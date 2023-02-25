

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

//Get all hotels
app.MapGet("/hotels", async (HotelDB hdb) => await hdb.Hotels.ToListAsync());

//Get specific hotel by id
app.MapGet("/hotels/{id}", async (int id, HotelDB hdb) =>
    await hdb.Hotels.FirstOrDefaultAsync(h => h.Id == id) is Hotel hotel ?
    Results.Ok(hotel) :
    Results.NotFound()
);

//Add hotel
app.MapPost("/hotels", async ([FromBody] Hotel h, [FromServices] HotelDB hdb) =>
{
    hdb.Add(h);
    await hdb.SaveChangesAsync();
    return Results.Created($"/hotels{h.Id}", h);
});

//Delete all hotels
app.MapDelete("/hotels", async (HotelDB hdb) =>
{
    await hdb.Hotels.ExecuteDeleteAsync();
    await hdb.SaveChangesAsync();
    return Results.Ok();
});

//Update specific hotel, id shall be same
/*
app.MapPut("/hotels", async (HotelDB h) =>
{
    var index = h.Hotels.FindAsync(ht => ht.Id == h.Id);
    if (index < 0)
        throw new Exception("Hotel not found");
    _hotels[index] = h;
});
*/

app.Run();

//db context
public class HotelDB : DbContext
{
    public HotelDB(DbContextOptions<HotelDB> options) : base(options) { }
    public DbSet<Hotel> Hotels => Set<Hotel>();
}