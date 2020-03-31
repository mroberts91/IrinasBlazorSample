using IrinasBlazorSample.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Radzen.Blazor;

namespace IrinasBlazorSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: "IrinasDatabase");
            });

            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddHttpClient<IPictureService, PictureService>();

            services.AddRazorPages();
            services.AddServerSideBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            AddTestData(dataContext);
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private void AddTestData(DataContext context)
        {
            var testUser1 = new Data.Models.User
            {
                Id = "abc123",
                FirstName = "Luke",
                LastName = "Skywalker"
            };


            var testUser2 = new Data.Models.User
            {
                Id = "bbb123",
                FirstName = "Irina",
                LastName = "Timochenko"
            };


            var testPost1 = new Data.Models.Post
            {
                Id = "def234",
                UserId = testUser1.Id,
                Content = "What a piece of junk!"
            };

            var testPost2 = new Data.Models.Post
            {
                Id = "hfd234",
                UserId = testUser2.Id,
                Content = "Merica..."
            };

            context.Users.Add(testUser1);
            context.Users.Add(testUser2); 
            context.Posts.Add(testPost1);
            context.Posts.Add(testPost2);

            context.SaveChanges();
        }
    }
}
