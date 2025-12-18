var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAll");
app.MapControllers();
app.Run();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowVercel", policy => {
        policy.WithOrigins("https://global-trade-hub-51.vercel.app") // Your Vercel URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
// ...
app.UseCors("AllowVercel");
