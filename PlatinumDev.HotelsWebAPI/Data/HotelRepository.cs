public class HotelRepository : IHotelRepository
{
    private HotelDB _dbContext { get; }

    public HotelRepository(HotelDB hdb)
    {
        _dbContext = hdb;
    }
    public async Task<Hotel> GetHotelAsync(int id) => await _dbContext.Hotels.FirstAsync(h => h.Id == id);
    public async Task<IList<Hotel>> GetHotelsAsync() => await _dbContext.Hotels.ToListAsync();
    public async Task InsertHotelAsync(Hotel newHotel)
    {
        await _dbContext.AddAsync(newHotel);
        await SaveAsync();
    }

    public async Task UpdateHotelAsync(Hotel newHotel)
    {
        var foundHotel = await _dbContext.Hotels.FindAsync(new object[] { newHotel.Id });
        if (foundHotel == null)
            return;

        foundHotel.Latitude = newHotel.Latitude;
        foundHotel.Longitude = newHotel.Longitude;
        foundHotel.Name = newHotel.Name;

        _dbContext.Update(foundHotel);
        await SaveAsync();
    }

    public async Task DeleteHotelAsync(int id)
    {
        var foundHotel = await _dbContext.Hotels.FindAsync(new object[] { id });
        if (foundHotel == null)
            return;
        _dbContext.Remove(foundHotel);
        await SaveAsync();
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async void Dispose()
    {
        await _dbContext.DisposeAsync();
    }
}