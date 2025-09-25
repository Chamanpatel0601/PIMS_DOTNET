using Microsoft.EntityFrameworkCore;
using PIMS_DOTNET.Repository;
using PIMS_DOTNET.Services;

var builder = WebApplication.CreateBuilder(args);

// -------------------- Add services --------------------


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register custom services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();

// (Optional) Add authentication / JWT here if needed
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

// (Optional) Add authentication middleware
// app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
