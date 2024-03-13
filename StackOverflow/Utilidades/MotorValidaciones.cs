using StackOverflow.Models;

namespace StackOverflow.Utilidades
{
    public class MotorValidaciones
    {
        public bool ValidacionesAdd(Post model, Predicate<Post>[] validaciones) =>
            validaciones.Where(d=> !d(model)).Count() == 0;

        public bool ValidacionesVote(Post model, Predicate<Post> validaciones) =>
            validaciones(model);
    }
}
