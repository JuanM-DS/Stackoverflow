namespace StackOverflow.Models
{
    public interface IPostAction
    {
        public string[] VoteUp();
        public string[] VoteDown();
    }
}
