using SDK.API;

try
{
    var builder = WebApplication.CreateBuilder(args);
    // Add services to the container.
    var cors = "mycors";
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: cors,
                          policy =>
                          {
                              policy.WithOrigins("https://api.hoilai.com",
                                                  "http://127.0.0.1:8848/Weblogin/index.html");
                          });
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline. 
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseCors(cors);

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    API.Print(ex.Message);
}
