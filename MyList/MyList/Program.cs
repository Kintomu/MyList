using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyList.Components;
using MyList.Data_Access;
using MyList.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyList",
    "Logs");
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(Path.Combine(logDirectory, $"log-{DateTime.Now:yyyy-MM-dd}.log"))
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddDbContext<GiftListContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("GiftListContext") ?? "Data Source=GiftList.db"));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.EnableAnnotations();
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "API Key Authentication",
        Name = "Authorization", // Header name
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKey"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Gift List API V1"); });

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<GiftListContext>();
    context.Database.EnsureCreated();

    if (!context.GiftLists.Any())
    {
        context.GiftLists.AddRange(
            new GiftList { ListName = "List 1", GiftName = "Item 1", GiftDescription = "Description 1" },
            new GiftList { ListName = "List 2", GiftName = "Item 2", GiftDescription = "Description 2" }
        );
        context.SaveChanges();
    }
}

app.Run();