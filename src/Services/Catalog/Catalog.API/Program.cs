
var builder = WebApplication.CreateBuilder(args);

try
{
    // Add Services to the Container here
    builder.Services.AddCarter();
    builder.Services.AddMediatR(config => { config.RegisterServicesFromAssemblies(typeof(Program).Assembly); });
    builder.Services.AddMarten(options =>
    {
        options.Connection(builder.Configuration.GetConnectionString("DefaultConnection")!);
    }).UseLightweightSessions();

    var app = builder.Build();

    //app.MapGet("/", () => "Hello World!");
    //Configure the Http Request Pipeline
    app.MapCarter();
    app.Run();
}
catch (Exception ex)
{
    throw;
}
