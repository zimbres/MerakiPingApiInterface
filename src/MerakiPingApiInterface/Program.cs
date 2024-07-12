var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("Default");
builder.Services.AddHttpClient("Proxy", client => { })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var configs = builder.Configuration.GetSection(nameof(ProxyConfiguration)).Get<ProxyConfiguration>();
        return new HttpClientHandler
        {
            Proxy = new WebProxy(configs?.Address + ":" + configs?.Port),
            UseProxy = true
        };
    });
builder.Services.AddSingleton<RequestPingService>();
builder.Services.AddSingleton<RequestResultService>();
builder.Services.AddSingleton<PingService>();
builder.Services.AddSingleton<StartupLoggingService>();

var app = builder.Build();
app.Services.GetRequiredService<StartupLoggingService>().Log();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();
app.Run();
