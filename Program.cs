using Orders.StartUp;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);   
        builder.Services.ConfigureServices(builder);
        
        var app = builder.Build();
        app.ConfigureSwagger();
        app.MapProductEndpoints();
        app.Run();
    }
}