var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Configuration.AddAzureAppConfiguration("Endpoint=https://ac-learning.azconfig.io;Id=8ntB;Secret=3mfIRMT2ZWa8cHOyzXmC6ffc9zl9XBJniQ0cemokobOl40SU5TLmJQQJ99BDACGhslB7YLY4AAACAZAC4ElZ");

builder.Configuration
    //.SetBasePath(Directory.GetCurrentDirectory())
    //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    //.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables(); // <-- Picks up Azure App Service environment variables


// Step 2: Load Azure App Configuration if connection string is available
var appConfigConnection = builder.Configuration["AppConfigConnectionString"];

if (!string.IsNullOrEmpty(appConfigConnection))
{
    builder.Configuration.AddAzureAppConfiguration(options =>
    {
        options.Connect(appConfigConnection)
               .UseFeatureFlags()
               .ConfigureRefresh(refresh =>
               {
                   refresh.Register("SentinelKey", refreshAll: true)
                          .SetRefreshInterval(TimeSpan.FromSeconds(30));
               });
    });
}

// Step 3: Register Azure App Configuration services if used
builder.Services.AddAzureAppConfiguration();
//builder.Services.AddFeatureManagement();      // For feature flags

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
