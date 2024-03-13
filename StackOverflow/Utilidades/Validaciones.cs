using StackOverflow.Models;

namespace StackOverflow.Utilidades
{
    public class Validaciones
    {

        public Predicate<Post>[] PostValidations =
        {
            b => b.PostTitle != null,
            b => b.PostDescription != null
        };

        public bool ValidationsVote(Post post)
        {
            return post.DownVote < 0;
        }


    }
}
