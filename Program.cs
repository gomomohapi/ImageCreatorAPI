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

app.UseHttpsRedirection();

app.MapGet("/GenerateImage/{prompt}", (string prompt) =>
{
    return ImageCreatorAPI.ImageCreator.GenerateImage(prompt);
})
    .WithName("GetGeneratedImage")
    .WithSummary("Generates Image")
    .WithDescription("Generates an image with the Azure OpenAI Service based on a provied prompt")
    .WithOpenApi();

app.Run();