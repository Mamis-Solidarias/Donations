using MamisSolidarias.WebAPI.Donations.StartUp;

var builder = WebApplication.CreateBuilder(args);
ServiceRegistrar.Register(builder);

var app = builder.Build();
MiddlewareRegistrar.Register(app);

app.Run();