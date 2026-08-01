using GameStore.Api.Endpoints;
using GameStore.Api.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
//Console.WriteLine(builder.Configuration["ConnectionStrings:DefaultConnection"]); //Kiểm tra xem chuỗi kết nối có đúng ko
builder.Services.AddSqlServer<GameStoreContext>(connString);

var app = builder.Build();

app.MapGamesEndpoints();

app.MigrateDb();

app.Run();
