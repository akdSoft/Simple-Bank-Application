using Simple_Bank_Application.Data;
using Microsoft.EntityFrameworkCore;
using Simple_Bank_Application.Repositories;
using Simple_Bank_Application.Services;
using Microsoft.AspNetCore.Authorization;
using Simple_Bank_Application.Repositories.Interfaces;
using Simple_Bank_Application.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseMySQL(connectionString!), ServiceLifetime.Transient);

//builder.Services.AddTransient<AppDbContext>();

builder.Services.AddScoped<ValidatorService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
builder.Services.AddScoped<IBankAccountService, BankAccountService>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
        builder.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());
});

builder.Services.AddRazorPages();

builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache(); //inmemory cache
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();


app.UseRouting();
app.UseCors("AllowFrontend");
app.UseStaticFiles();
app.UseSession();


app.UseMiddleware<AuthorizationMiddleware>();
app.UseAuthorization();



app.UseSwagger();
app.UseSwaggerUI();


app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers(); // API ile Razor Pages birlikte kullanýlacak
});

app.Run();
