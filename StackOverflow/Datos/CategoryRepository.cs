using StackOverflow.Models;
using System.Data;
using System.Data.SqlClient;

namespace StackOverflow.Datos
{
    public class CategoryRepository : IRepository<Category>
    {
        public override List<Category> GetModels()
        {
            var categories = new List<Category>();

            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("sp_List_Categorys", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            var category = new Category();
                            category.IdCategory = Convert.ToInt32(rd["IdCategory"]);
                            category.Name = rd["Name"].ToString();
                            categories.Add(category);
                        }
                    }
                }
            }
            return categories;
        }
        public override bool DeleteModel(Category model)
        {
            bool res;
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("sp_Delete_Category", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", model.IdCategory);
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

        public override bool InsertModel(Category model)
        {
            bool res;
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("sp_Insert_Category", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Title", model.Name);
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
    }
}
