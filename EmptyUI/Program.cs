using EmptyDAL;
using EmptyDAL.Abstract;
using EmptyDAL.Interfaces;
using EmptyDAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var spaSrcPath = "ClientApp";
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSpaStaticFiles(opt => opt.RootPath = $"{spaSrcPath}/dist");
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IStrangeItemManager, StrangeItemManager>();
builder.Services.AddTransient<IStrangeItemRepository, StrangeItemRepository>();

builder.Services.AddDbContext<StrangeItemDbContext>(
    options =>
    {
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("StrangeDatabase"));
    });


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(static options => options.SwaggerEndpoint("../swagger/v1/swagger.json", "History service api"));
}
else
{
    app.UseResponseCompression();
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.UseSpa(spa =>
{
    spa.Options.SourcePath = spaSrcPath;

    if (app.Environment.IsDevelopment())
        spa.UseReactDevelopmentServer(npmScript: "start");
});

app.Run();