public interface IHotelRepository : IDisposable
{
    Task<IList<Hotel>> GetHotelsAsync();
    Task<Hotel> GetHotelAsync(int id);
    Task InsertHotelAsync(Hotel newHotel);
    Task UpdateHotelAsync(Hotel newHotel);
    Task DeleteHotelAsync(int id);
    Task SaveAsync();
}