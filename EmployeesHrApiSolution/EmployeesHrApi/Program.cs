
using EmployeesHrApi.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeesHrApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var employeesConnectionString = builder.Configuration.GetConnectionString("employees") ?? throw new Exception("Need a Connection String");
            
            builder.Services.AddDbContext<EmployeeDataContext>(options =>
            {
                options.UseSqlServer(employeesConnectionString);
            });
            //above this is config for th4e "behind the scenes" thing in your API
            var app = builder.Build();
            //after this is setting up "middleware" - thats the code that recieves the HTTP request and makes the response
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(); //the code that will let you GET the documentation
                app.UseSwaggerUI();  //this is the middleware that provides the UI for viewing that documentation
            }

            app.UseAuthorization(); //web guard

            app.MapControllers(); //when doing "controller based" apis, this is where it creates the lookup table / route table

            app.Run(); // this is when our API is up and running. And it "blocks" here.
        }
    }
}