using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCD2.Context;
using MVCD2.Middleware;
using MVCD2.Models;
using MVCD2.Repo.Auth;
using MVCD2.Repo.Crs;
using MVCD2.Repo.Dept;
using MVCD2.Repo.Ins;
using MVCD2.Repo.Std;
using MVCD2.Repo.Unit;
using MVCD2.Repo.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CompanyContext>(op =>
    op.UseSqlServer(builder.Configuration.GetConnectionString("cs")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<CompanyContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["App:GoogleClientId"];
        googleOptions.ClientSecret = builder.Configuration["App:GoogleClientSecret"];
    })
    .AddFacebook(facebookOptions =>
    {
        facebookOptions.AppId = "FACEBOOK_APP_ID";
        facebookOptions.AppSecret = "FACEBOOK_APP_SECRET";
    });


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";  
    options.AccessDeniedPath = "/Account/AccessDenied"; 
});

builder.Services.AddControllersWithViews();
builder.Services.AddLogging();
builder.Services.AddSession();
builder.Services.AddScoped<IInstructorRepo, InstructorRepo>();
builder.Services.AddScoped<ICourseRepo,CourseRepo>();
builder.Services.AddScoped<IStudentRepo, StudentRepo>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedRoles(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseSession();
app.UseRouting();
//app.UseMiddleware<LoggingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

static async Task SeedRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { "Admin", "HR", "Instructor", "Student" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
