var builder = WebApplication.CreateBuilder(args);

//add data context
builder.Services.AddDbContext<HotelDB>(options =>
{
    //add configuration from appsettings.json
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
});

//add repository service
builder.Services.AddScoped<IHotelRepository, HotelRepository>();

//add swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//while development ensure db scheme created while app started
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using var scope = app.Services.CreateScope();
    var dbService = scope.ServiceProvider.GetRequiredService<HotelDB>();
    dbService.Database.EnsureCreated();
}

//Get all hotels
app.MapGet("/hotels", async (IHotelRepository repo) => await repo.GetHotelsAsync())
.Produces<List<Hotel>>(StatusCodes.Status200OK)
.WithName("GetAllHotels")
.WithTags("GET");

//Get specific hotel by id
app.MapGet("/hotels/{id}", async (int id, IHotelRepository repo) =>
    await repo.GetHotelAsync(id) is Hotel hotel ?
    Results.Ok(hotel) :
    Results.NotFound()
).WithTags("GET");

//Add hotel
app.MapPost("/hotels", async ([FromBody] Hotel h, IHotelRepository repo) =>
{
    await repo.InsertHotelAsync(h);
    await repo.SaveAsync();
    return Results.Created($"/hotels{h.Id}", h);
});

//Delete specific hotel
app.MapDelete("/hotels{id}", async (int id, IHotelRepository repo) =>
{
    await repo.DeleteHotelAsync(id);
    await repo.SaveAsync();
    return Results.NoContent();
})
.ExcludeFromDescription();

//Update specific hotel, id shall be same
app.MapPut("/hotels", async ([FromBody] Hotel h, IHotelRepository repo) =>
{
    await repo.UpdateHotelAsync(h);
    await repo.SaveAsync();
    return Results.NoContent();
});

app.Run();

//add HTTPS
app.UseHttpsRedirection();
