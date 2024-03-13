namespace StackOverflow.Models
{
    public class ModelViews
    {
        public static List<Category> categories { get; set; }
        public Post newPost { get; set; }

        public static bool Validation = true;
        public string? Category
        {
            get { return null; }
            set {
                var lista = new ModelViews();
                newPost.Category = categories.Where(c => c.Name == value).First();
            }
        }
    }
}
