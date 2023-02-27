

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

//Delete specific hotel
app.MapDelete("/hotels{id}", async (int id, HotelDB hdb) =>
{
    var foundHotel = await hdb.Hotels.FindAsync(new object[] { id });
    if (foundHotel == null)
        return Results.NotFound();
    hdb.Remove(foundHotel);
    await hdb.SaveChangesAsync();
    return Results.Ok();
});

//Update specific hotel, id shall be same
app.MapPut("/hotels", async ([FromBody] Hotel h, [FromServices] HotelDB hdb) =>
{
    var foundHotel = await hdb.Hotels.FindAsync(new object[] { h.Id });
    if (foundHotel == null)
        return Results.NotFound();

    foundHotel.Latitude = h.Latitude;
    foundHotel.Longitude = h.Longitude;
    foundHotel.Name = h.Name;

    hdb.Update(foundHotel);
    await hdb.SaveChangesAsync();
    return Results.Ok();
});

app.Run();

//add HTTPS
app.UseHttpsRedirection();
