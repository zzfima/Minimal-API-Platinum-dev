var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var _hotels = new List<Hotel>();

_hotels.Add(new Hotel()
{
    Id = 1,
    Latitude = 21,
    Longitude = 32,
    Name = "Hof argaman"
});

_hotels.Add(new Hotel()
{
    Id = 2,
    Latitude = 22,
    Longitude = 32,
    Name = "Nof hatsafon"
});

app.MapGet("/hotels", () => _hotels);

app.Run();



public class Hotel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Latitude { get; set; }
    public int Longitude { get; set; }
}