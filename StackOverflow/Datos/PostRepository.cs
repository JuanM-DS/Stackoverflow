using StackOverflow.Models;
using System.Collections.Immutable;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace StackOverflow.Datos
{
    public class PostRepository : IRepository<Post>
    {
        private readonly CategoryRepository categoryRepository = new CategoryRepository();

        public override List<Post> GetModels()
        {
            var posts = new List<Post>();
            var categories = categoryRepository.GetModels();
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("sp_List", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            var post = new Post();
                            post.IdPost = Convert.ToInt32(rd["IdPost"]);
                            post.PostTitle = rd["Title"].ToString();
                            post.PostDescription = rd["Description"].ToString();
                            post.CreationDate = DateTime.Parse(rd["Creation_Date"].ToString());
                            post.UpVote = Convert.ToInt32(rd["UpVote"]);
                            post.DownVote = Convert.ToInt32(rd["DownVote"]);
                            post.Category = categories.FirstOrDefault(b => b.IdCategory == Convert.ToInt32(rd["IdCategory"]));
                            posts.Add(post);
                        }
                    }
                }
            }
            return posts;
        }

        public override bool InsertModel(Post model)
        {
            bool res;
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("sp_Insert", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Title", model.PostTitle);
                        cmd.Parameters.AddWithValue("@Description", model.PostDescription);
                        cmd.Parameters.Add("@Creation_Date", SqlDbType.Date).Value = model.CreationDate.Date;
                        cmd.Parameters.AddWithValue("@IdCategory", model.Category.IdCategory);
                        cmd.ExecuteNonQuery();
                        res = true;
                    }
                }
            }
            catch (Exception ex)
            {
                res = false;
                throw new ArgumentException(ex.Message); 
            }
            return res;
        }

        public bool UpdatePost(Post model)
        {
            bool res;
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("sp_Update", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdPost", model.IdPost);
                        cmd.Parameters.AddWithValue("@Title", model.PostTitle);
                        cmd.Parameters.AddWithValue("@Description", model.PostDescription);
                        cmd.Parameters.Add("@Creation_Date", SqlDbType.Date).Value = model.CreationDate.Date;
                        cmd.Parameters.AddWithValue("@IdCategory", model.Category.IdCategory);
                        cmd.ExecuteNonQuery();
                        res = true;
                    }
                }
            }
            catch (Exception ex)
            {
                res = false;
                throw new ArgumentException(ex.Message);
            }
            return res;
        }

        public override bool DeleteModel(Post model)
        {
            bool res;
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("sp_Delete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", model.IdPost);
                        cmd.ExecuteNonQuery();
                        res = true;
                    }
                }
            }
            catch (Exception ex)
            {
                res = false;
                throw new ArgumentException(ex.Message);
            }
            return res;
        }

        public bool SetVote(Post model, Func<string[]> metod)
        {
            bool res;
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(metod()[0], conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdPost", model.IdPost);
                        cmd.Parameters.AddWithValue("@VotingStatus", Convert.ToInt32(metod()[1]));
                        cmd.ExecuteNonQuery();
                        res = true;
                    }
                }
            }
            catch (Exception ex)
            {
                res = false;
                throw new ArgumentException(ex.Message);
            }
            return res;
        }

        public List<Post> GetByCategory(Category model) =>
            GetModels().Where(b => b.Category.IdCategory == model.IdCategory).ToList();
        
        public Post GetPost(int IdPost)
        {
            var categories = categoryRepository.GetModels();
            var post = new Post();
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("sp_Get", conn))
                {
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", IdPost);
                    using (var rd = cmd.ExecuteReader())
                    {
                        while(rd.Read())
                        {
                            post.IdPost = Convert.ToInt32(rd["IdPost"]);
                            post.PostTitle = rd["Title"].ToString();
                            post.PostDescription = rd["Description"].ToString();
                            post.CreationDate = DateTime.Parse(rd["Creation_Date"].ToString());
                            post.UpVote = Convert.ToInt32(rd["UpVote"]);
                            post.DownVote = Convert.ToInt32(rd["DownVote"]);
                            post.Category = categories.FirstOrDefault(b => b.IdCategory == Convert.ToInt32(rd["IdCategory"]));
                        }
                    }
                }
            }
            return post;
        }
    }
}
