using PZ3_Project.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<AnimeXmlService>(provider =>
{
    var xmlFilePath = builder.Environment.IsDevelopment()
        ? Path.Combine(builder.Environment.ContentRootPath, "animeData.xml")
        : "/path/to/production/xml/file"; // Replace with the actual production path

    return new AnimeXmlService(xmlFilePath);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Anime}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "animeDetails",
        pattern: "Anime/AnimeDetails/{id?}",
        defaults: new { controller = "Anime", action = "AnimeDetails" });
    endpoints.MapControllerRoute(
        name: "animeEdit",
        pattern: "Anime/Edit/{id?}",
        defaults: new { controller = "Anime", action = "Edit" });
});


app.Run();
