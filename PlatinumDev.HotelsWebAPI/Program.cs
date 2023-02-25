var builder = WebApplication.CreateBuilder(args);

//add data context
builder.Services.AddDbContext<HotelDB>(options =>
{
    //add configuration from appsettings.json
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
});
var app = builder.Build();

//while development ensure db scheme created while app started
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbService = scope.ServiceProvider.GetRequiredService<HotelDB>();
    dbService.Database.EnsureCreated();
}

var _hotels = new List<Hotel>();

//Get all hotels
app.MapGet("/hotels", () => _hotels);

//Get specific hotel by id
app.MapGet("/hotels/{id}", (int id) => _hotels.FirstOrDefault(h => h.Id == id));

//Add hotel
app.MapPost("/hotels", (Hotel h) => _hotels.Add(h));

//Delete all hotels
app.MapDelete("/hotels", () => _hotels.Clear());

//Update specific hotel, id shall be same
app.MapPut("/hotels", (Hotel h) =>
{
    var index = _hotels.FindIndex(ht => ht.Id == h.Id);
    if (index < 0)
        throw new Exception("Hotel not found");
    _hotels[index] = h;
});

app.Run();

//db context
public class HotelDB : DbContext
{
    public HotelDB(DbContextOptions<HotelDB> options) : base(options) { }
    public DbSet<Hotel> Hotels => Set<Hotel>();
}