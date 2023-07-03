var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAuthentication("MyCookieAuthType").AddCookie("MyCookieAuthType", options =>
{
    options.Cookie.Name = "MyCookieAuthType";
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/AccessDenied";
    options.ExpireTimeSpan= TimeSpan.FromMinutes(2);
});

//authorization
builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("MustBeFromHr",policy => policy.RequireClaim("Department", "Hr"));
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
    options.AddPolicy("HRManagerOnly", policy => policy.RequireClaim("Department", "Hr").RequireClaim("Manager"));
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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
