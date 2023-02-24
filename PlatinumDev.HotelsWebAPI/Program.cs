var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var _hotels = new List<Hotel>();

app.MapGet("/hotels", () => _hotels);
app.MapGet("/hotels/{id}", (int id) => _hotels.FirstOrDefault(h => h.Id == id));
app.MapPost("/hotels", (Hotel h) => _hotels.Add(h));
app.MapDelete("/hotels", () => _hotels.Clear());
app.MapPut("/hotels", (Hotel h) =>
{
    var index = _hotels.FindIndex(ht => ht.Id == h.Id);
    if (index < 0)
        throw new Exception("Hotel not found");
    _hotels[index] = h;
});
app.Run();