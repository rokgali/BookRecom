using backend.persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using backend.models.database;
using backend.services.gemini;
using backend.services;
using backend.services.book;
using backend.services.jobs;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Google;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("myserverconnection");
var apiKey = builder.Configuration["Gemini:ApiKey"];

builder.Services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<BookRecomDbContext>()
    .AddApiEndpoints();

builder.Services.AddDbContext<BookRecomDbContext>(options => {
    options.UseSqlServer(connectionString);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Semantic kernel for Gemini AI

var kernelBuiler = Kernel.CreateBuilder();

#pragma warning disable SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
kernelBuiler.AddGoogleAIGeminiChatCompletion(
    modelId:"gemini-1.5-flash",
    apiKey:apiKey,
    serviceId:"description"
);
#pragma warning restore SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

#pragma warning disable SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
kernelBuiler.AddGoogleAIGeminiChatCompletion(
    modelId:"tunedModels/main-book-takeaways-au2dj9bfx11d:generateContent",
    apiKey:apiKey,
    serviceId:"takeaways"
);
#pragma warning restore SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

var kernel = kernelBuiler.Build();

builder.Services.AddSingleton<Kernel>(kernel);

// ----------------------------

builder.Services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddAuthorizationBuilder();

builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});

builder.Services.AddTransient<IGeminiClient, GeminiClient>();
builder.Services.AddHttpClient<GeminiClient>();

builder.Services.AddTransient<IGeminiRequestFactory, GeminiRequestFactory>();

builder.Services.AddTransient<IBookService, BookService>();

builder.Services.AddTransient<IBackgroundTaskQueue, BackgroundTaskQueue>();
builder.Services.AddHostedService<BackgroundJobService>();

builder.Services.AddLogging(options => {
    options.AddDebug();
    options.AddConsole();
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.MapIdentityApi<User>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
