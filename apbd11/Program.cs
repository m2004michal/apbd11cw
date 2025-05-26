using apbd11.Data;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;
using Tutorial5.Services;

namespace apbd11;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddDbContext<DatabaseContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
        );
        
        builder.Services.AddScoped<IDbService, DbService>();

        var app = builder.Build();

        app.UseSwagger();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        
        app.UseHttpsRedirection();
        
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}