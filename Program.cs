using Microsoft.EntityFrameworkCore;
using PIMS_DOTNET.Repository;
using PIMS_DOTNET.Services;
using AutoMapper;
using PIMS_DOTNET.Mapping;

var builder = WebApplication.CreateBuilder(args);

// -------------------- Add services --------------------

// Add DbContext with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add controllers
builder.Services.AddControllers();

// Add Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -------------------- Register custom services --------------------
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IInventoryTransactionService, InventoryTransactionService>();

// -------------------- Register AutoMapper --------------------
// This will scan all assemblies for Profile classes automatically
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// -------------------- Optional: JWT Authentication --------------------
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//        .AddJwtBearer(options => { /* Configure JWT here */ });

// -------------------- Build app --------------------
var app = builder.Build();

// -------------------- Configure middleware --------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Uncomment if JWT authentication is configured
// app.UseAuthentication();

app.UseAuthorization();

// Map controllers
app.MapControllers();

// Run the application
app.Run();
