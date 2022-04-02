// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

likeButton.setOnClickListener{
    if (limit == 0) {
        limit = 1
        likeNumber.text = (likeNumber.toString().toInt() + 1).toString()
    } else if (limit == 1) {
        limit = 0
        likeNumber.text = (likeNumber.toString().toInt() - 1).toString()
    }
}

const FeedScreen = () => {
    const [posts, setPosts] = useState([]); // Here was the issue

    useEffect(() => {
        const boiler = async () => {
            const token = await AsyncStorage.getItem("token");
            fetch("http://192.168.1.5:3000/post", {
                headers: new Headers({
                    Authorization: "Bearer " + token
                })
            })
                .then(res => res.json())
                .then(res => {
                    setPosts(res.posts);
                })
                .catch(err => console.log(err));
        };

        boiler();
    }, []);

    return (
        <View style={stylesheet.container}>
            {posts.map(post => (
                <Text key={post.id}>{post.text}</Text>
            ))}
        </View>
    );
};

export default FeedScreen;