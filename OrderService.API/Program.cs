var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapGet("/ready", () =>
{
    Console.WriteLine("Order service is ready");
    return false;
});

app.MapGet("/commit", () =>
{
    Console.WriteLine("Order service is Committed"); 
    return true;
});

app.MapGet("/rollback", () =>
{
    Console.WriteLine("Order service is roll-backed");
});

app.Run();

