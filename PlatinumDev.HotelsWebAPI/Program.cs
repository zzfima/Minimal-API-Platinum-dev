var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
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