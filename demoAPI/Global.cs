
namespace demoAPI
{
    public class Global
    {
        public static string getConnectString()
        {
            //var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder();
            //var builder = WebApplication.CreateBuilder();
            //string conStr = builder.Configuration.GetConnectionString("MyConnection");
            string conStr = @"Data Source=GV605\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True;Trust Server Certificate=True";
            return conStr;
        }
    }
}
