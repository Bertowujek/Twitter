using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Twitter.Models;
using Twitter.Service;


namespace Twitter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITweetService serviceT;
        private readonly IUserService serviceU;
        private readonly IJWTService tokenService;
        private readonly IConfiguration config;
        private readonly IWebHostEnvironment hostEnvironment;//image


        public HomeController(ITweetService serviceT, IUserService serviceU, IJWTService tokenService, IConfiguration config, IWebHostEnvironment hostEnvironment)
        {
            this.serviceT = serviceT;
            this.serviceU = serviceU;
            this.tokenService = tokenService;
            this.config = config;
            this.hostEnvironment = hostEnvironment;
        }


        public IActionResult Index()
        {
            var tweets = serviceT.GetAll();
            return View(new TweetsViewModel { Tweets = tweets });
        }

        public IActionResult ByHandle(int Id)
        {
            var tweets = serviceT.GetByUser(Id);
            //return View(new TweetsHandleModel { TweetsByHandle = (IEnumerable<Tweet>)tweets });
            return View(new TweetsHandleModel { TweetsByHandle = tweets });
        }

     
        public IActionResult Create()
        {
            string token = HttpContext.Session.GetString("Token");

            if (token == null)
            {
                return (RedirectToAction("Login"));
            }
            else 
            {
                return View();
            }
           
        }

      
        [HttpPost]
        public async Task<IActionResult> Create(TweetViewModel tweet)
        {

            if (ModelState.IsValid)
            {

                var user = serviceU.GetById(tweet.UserId);

                var newTweet = serviceT.Create(new Tweet { Text = tweet.Text, Date = DateTime.Now, Likes = 0, Comments = "", User = user });
                if (newTweet is not null)
                    return RedirectToAction(nameof(Index));
                else
                    return RedirectToAction(nameof(Error));
            }
            else
            {
                return RedirectToAction(nameof(Error));
            }
        }

        
        public IActionResult Edit(int Id) //Retorna a view apenas
        {
            string token = HttpContext.Session.GetString("Token");

            if (token == null)
            {
                return (RedirectToAction("Login"));
            }

            else 
            {
                var tweetAEditar = serviceT.GetById(Id);

                //this.tokenService.GenerateToken
                //this.tokenService.GetJWTTokenClaim();
                //tweetAEditar.User.Id;

                //var tweetEditModel = new TweetEditModel();

                TweetEditModel tweetEditModel = new TweetEditModel();

                tweetEditModel.Id = tweetAEditar.Id;
                tweetEditModel.Text = tweetAEditar.Text;


                return View(tweetEditModel);
            }
            
        }

        /*crtl+k ctrl+c // ctrl+k crtl+u*/
        [HttpPost]
        public async Task<IActionResult> Edit(TweetEditModel tweet)
        {
            if (ModelState.IsValid)
            {
                var newTweet = serviceT.Update(tweet);
                
                if (newTweet is not null)
                    return RedirectToAction(nameof(Index)); //redirect para a view index
                else
                    return RedirectToAction(nameof(Error)); //redirect para a view de erro
            }
            else
            {
                return RedirectToAction(nameof(Error));
            }
        }

        //public IActionResult ByHandle()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                var userExists = serviceU.FindByName(user.Name);


                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "User already exists!");

                var newUser = serviceU.Create(user);
                if (newUser is not null)
                    return RedirectToAction(nameof(Index));
                else
                    return RedirectToAction(nameof(Error));
            }
            else
            {
                return RedirectToAction(nameof(Error));
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
      
        public IActionResult Delete(int Id)
        {
            string token = HttpContext.Session.GetString("Token");
           
            if (token == null)
            {
                return (RedirectToAction("Login"));
            }

            else 
            {

                var tweet = serviceT.GetById(Id);
                if (tweet is not null)
                {
                    serviceT.DeleteById(Id);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Error));
                }

            }
            

        }

        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(User userModel)
        {
            if (string.IsNullOrEmpty(userModel.Handle) || string.IsNullOrEmpty(userModel.Password))
            {
                return (RedirectToAction("Error"));
            }

            var user = serviceU.Get(userModel.Handle, userModel.Password);
            var validUser = new User{ Handle = user.Handle, Id = user.Id, Name = user.Name, Email = user.Email, Avatar = user.Avatar};

            if (validUser != null)
            {
                string generatedToken = tokenService.GenerateToken(
                    config["Jwt:Key"].ToString(),
                    config["Jwt:Issuer"].ToString(),
                    config["Jwt:Audience"].ToString(),
                validUser);

                if (generatedToken != null)
                {
                    HttpContext.Session.SetString("Token", generatedToken);
                    return RedirectToAction("Index", validUser);
                }
                else
                {
                    return (RedirectToAction("Error"));
                }
            }
            else
            {
                return (RedirectToAction("Error"));
            }
        }

        public IActionResult Logout()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]

        public IActionResult LogoutUser()
        {
            HttpContext.Session.Remove("Token");
            return (RedirectToAction("Index"));
        }




        //IMAGE 
        public IActionResult Image(ImageUploaded image)
        {
            return View(image);
        }

        public IActionResult Upload ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            string path = Path.Combine(this.hostEnvironment.WebRootPath, "images");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = Path.GetFileName(file.FileName);
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(stream);

                var token = HttpContext.Session.GetString("Token");
                var id = tokenService.GetJWTTokenClaim(token);

                serviceU.UpdateImage(int.Parse(id), fileName);

                return RedirectToAction("Image", new ImageUploaded { Path = file.FileName });
            }

            return RedirectToAction("Error");
        }
    }
}