namespace BakeryOps.API.StartUp;

public static class SwaggerConfig
{
    public static WebApplication ConfigureSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.yaml", "Orders API V1");
            });
        }
        
        return app;
    }
}
