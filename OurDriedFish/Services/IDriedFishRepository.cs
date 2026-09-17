using OurDriedFish.Models;

namespace OurDriedFish.Services;

public interface IDriedFishRepository
{
    Task<IReadOnlyList<DriedFishItem>> GetAllAsync();
    Task<DriedFishItem?> GetByIdAsync(int id);
    Task<IReadOnlyList<DriedFishItem>> GetSeasonalAsync();
    Task<IReadOnlyList<DriedFishItem>> SearchAsync(string? query, string? category = null);
    Task<IReadOnlyList<string>> GetCategoriesAsync();
}
