using ASassignment.Model;
using ASassignment.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace WebApp_Core_Identity.Model
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _configuration;
        //public AuthDbContext(DbContextOptions<AuthDbContext> options):base(options){ }
        public AuthDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }   
        /*public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ReCAPTCHASettings>(_configuration.GetSection("GooglereCAPTCHA"));
            services.AddTransient<GooglereCaptchaService>();
        }*/
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("AuthConnectionString"); optionsBuilder.UseSqlServer(connectionString);
        }
    }
}