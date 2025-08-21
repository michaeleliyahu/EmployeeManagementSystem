var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// --------------------- Repositories ---------------------
builder.Services.AddScoped<EmployeeManagementSystem.Repositories.IEmployeeRepository, EmployeeManagementSystem.Repositories.EmployeeJsonRepository>();
builder.Services.AddScoped<EmployeeManagementSystem.Repositories.IDepartmentRepository, EmployeeManagementSystem.Repositories.DepartmentJsonRepository>();

// --------------------- Services ---------------------
builder.Services.AddScoped<EmployeeManagementSystem.Services.EmployeeService>();
builder.Services.AddScoped<EmployeeManagementSystem.Services.DepartmentService>();
builder.Services.AddScoped<EmployeeManagementSystem.Services.DashboardService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseMiddleware<EmployeeManagementSystem.Middleware.ExceptionLoggingMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// --------------------- Default Route ---------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
