using MyBAL;
using MyDAL;

namespace MyProject
{
    public static class ServiceExtension
    {
        public static void RegisterRepos(this IServiceCollection collection, ConfigurationManager configuration)
        {
            var connectionString = configuration["ConnectionStrings:ConnectionString"];
            collection.AddTransient<IStudentService, StudentService>();
            collection.AddTransient<IStudentRepo>(s => new StudentRepo(connectionString));
        }
    }
}
