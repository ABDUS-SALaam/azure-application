var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Configuration.AddAzureAppConfiguration("Endpoint=https://ac-learning.azconfig.io;Id=8ntB;Secret=3mfIRMT2ZWa8cHOyzXmC6ffc9zl9XBJniQ0cemokobOl40SU5TLmJQQJ99BDACGhslB7YLY4AAACAZAC4ElZ");
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
