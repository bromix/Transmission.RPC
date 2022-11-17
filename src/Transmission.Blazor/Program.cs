using Bromix.Transmission.Blazor.Data;

Transmission.RPC.Environment.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<Transmission.RPC.Client>(serviceProvider =>
{
    var transmissionUrl = System.Environment.GetEnvironmentVariable("TRANSMISSION_URL") ?? throw new InvalidOperationException("TRANSMISSION_URL is missing in Environment Variables.");
    var transmissionUserName = System.Environment.GetEnvironmentVariable("TRANSMISSION_USERNAME") ?? throw new InvalidOperationException("TRANSMISSION_USERNAME is missing in Environment Variables.");
    var transmissionPassword = System.Environment.GetEnvironmentVariable("TRANSMISSION_PASSWORD") ?? throw new InvalidOperationException("TRANSMISSION_PASSWORD is missing in Environment Variables.");
    Transmission.RPC.Client client = new(
        transmissionUrl,
        transmissionUserName,
        transmissionPassword
    );
    return client;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
