using Microsoft.EntityFrameworkCore;



namespace Twitter.Models
{
    public class TwitterContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Tweet> Tweets { get; set; }

        public TwitterContext(DbContextOptions<TwitterContext> options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=twitter;" +
                "user=root;password=password");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Handle).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.Email).IsRequired();


            });

            modelBuilder.Entity<Tweet>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Text).IsRequired();
                entity.HasOne(d => d.User);
            });
        }

    }
}
