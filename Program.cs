using Microsoft.EntityFrameworkCore;
using PIMS_DOTNET.Repository;
using PIMS_DOTNET.Services;
using AutoMapper;
using PIMS_DOTNET.Mapping;

var builder = WebApplication.CreateBuilder(args);

// -------------------- Add services --------------------

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add controllers
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -------------------- Register custom services --------------------
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();

// -------------------- Register AutoMapper --------------------
builder.Services.AddAutoMapper(typeof(MappingProfile));

// -------------------- Optional: Authentication / JWT --------------------
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//        .AddJwtBearer(options => { ... });

// -------------------- Build app --------------------
var app = builder.Build();

// -------------------- Configure middleware --------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthentication(); // Optional JWT middleware
app.UseAuthorization();

app.MapControllers();

app.Run();
