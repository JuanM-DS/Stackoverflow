namespace StackOverflow.Datos
{
    public abstract class IRepository<T>
    {
        protected readonly string ConnectionString = "Data Source=Pc\\MSSQLSERVER01; Initial Catalog=DBStackOverflow; Integrated Security=true;";

        public abstract List<T> GetModels();

        public abstract bool DeleteModel(T model);

        public abstract bool InsertModel(T model);
    }
}
