namespace StackOverflow.Models
{
    public class Post : IPostAction
    {
        public int IdPost { get; set; }
        public string PostTitle { get; set; }
        public string PostDescription { get; set; }
        public DateTime CreationDate { get; set; }
        public int UpVote { get; set; }
        public int DownVote { get; set; }
        public Category? Category { get; set; }

        public Post(int idPost, string postTitle, string postDescription, int upVote, int downVote, Category category)
        {
            IdPost = idPost;
            PostTitle = postTitle;
            PostDescription = postDescription;
            CreationDate = DateTime.Now;
            UpVote = upVote;
            DownVote = downVote;
            Category = category;
        }
        public Post() => CreationDate = DateTime.Now;

        public string[] VoteUp()
        {
            return ["sp_VoteUp", $"{UpVote++}"];
        }

        public string[] VoteDown()
        {
            return ["sp_VoteDown", $"{DownVote++}"];
        }

    }
}
