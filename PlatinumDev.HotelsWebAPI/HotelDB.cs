


//add data context

//while development ensure db scheme created while app started

//Get all hotels

//Get specific hotel by id

//Add hotel

//Delete all hotels

//Delete specific hotel

//Update specific hotel, id shall be same


//add HTTPS
//db context
public class HotelDB : DbContext
{
    public HotelDB(DbContextOptions<HotelDB> options) : base(options) { }
    public DbSet<Hotel> Hotels => Set<Hotel>();
}