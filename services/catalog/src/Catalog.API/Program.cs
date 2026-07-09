using Catalog.Infrastructure;
//using Catalog.API.Middlewares; // Exception middleware klasörünüze göre namespace'i gerekirse güncelleyin

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Catalog.Application.Commands.CreateProductCommand).Assembly));


builder.Services.AddInfrastructureServices(builder.Configuration);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


//app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/health", () => Results.Ok(new
{
    status = "healthy",
    service = "catalog"
}));

app.Run();