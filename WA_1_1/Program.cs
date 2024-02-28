using WA_1_1.Payloads.Converters;
using WA_1_1.Services.Implements;
using WA_1_1.Services.Interfaces;
using WA_1_1.Payloads.Responses;
using WA_1_1.Payloads.DataResponses;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IHoaDonServices, HoaDonServices>();
builder.Services.AddSingleton<HoaDonConverter>();
builder.Services.AddSingleton<ResponsesObject<Responses_HoaDon>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
