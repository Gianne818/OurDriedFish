using OurDriedFish.Models;
using static Supabase.Postgrest.Constants;

namespace OurDriedFish.Services;

public class DriedFishRepository : IDriedFishRepository
{
    private readonly Supabase.Client _db;

    public DriedFishRepository(Supabase.Client db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<DriedFishItem>> GetAllAsync()
    {
        var res = await _db.From<DriedFishItem>()
            .Where(p => p.DeletedAt == null)
            .Order(p => p.Id, Ordering.Ascending)
            .Range(0, 49)
            .Get();

        return res.Models;
    }

    public async Task<DriedFishItem?> GetByIdAsync(int id)
    {
        return await _db.From<DriedFishItem>()
            .Where(p => p.Id == id && p.DeletedAt == null)
            .Single();
    }

    public async Task<IReadOnlyList<DriedFishItem>> GetSeasonalAsync()
    {
        var res = await _db.From<DriedFishItem>()
            .Where(p => p.DeletedAt == null && p.IsSeasonal == true)
            .Order(p => p.Id, Ordering.Ascending)
            .Range(0, 49)
            .Get();

        return res.Models;
    }

    public async Task<IReadOnlyList<DriedFishItem>> SearchAsync(string? query, string? category = null)
    {
        var table = _db.From<DriedFishItem>()
            .Where(p => p.DeletedAt == null);

        if (!string.IsNullOrWhiteSpace(category) && !category.Equals("All", StringComparison.OrdinalIgnoreCase))
        {
            var selectedCategory = category.Trim();
            table = table.Where(p => p.Category == selectedCategory);
        }

        if (!string.IsNullOrWhiteSpace(query))
        {
            var term = query.Trim();
            table = table.Filter("name", Operator.ILike, $"%{term}%");
        }

        var res = await table
            .Order(p => p.Id, Ordering.Ascending)
            .Range(0, 49)
            .Get();

        return res.Models;
    }

    public async Task<IReadOnlyList<string>> GetCategoriesAsync()
    {
        var items = await GetAllAsync();
        var categories = items
            .Select(i => i.Category)
            .Where(c => !string.IsNullOrWhiteSpace(c))
            .Distinct()
            .OrderBy(c => c)
            .ToList();
        categories.Insert(0, "All");
        return categories;
    }

    public async Task<DriedFishItem> AddAsync(DriedFishItem item)
    {
        var toInsert = new DriedFishItem
        {
            Name = item.Name.Trim().ToUpperInvariant(),
            LocalName = item.LocalName.Trim(),
            ScientificName = item.ScientificName.Trim(),
            Description = item.Description.Trim(),
            WeightGrams = item.WeightGrams > 0 ? item.WeightGrams : 1000,
            PricePerKg = item.PricePerKg,
            StockQuantity = item.StockQuantity,
            Category = item.Category.Trim(),
            ImageUrl = string.IsNullOrWhiteSpace(item.ImageUrl)
                ? "images/products/danggit.png"
                : item.ImageUrl.Trim(),
            IsSeasonal = item.IsSeasonal,
            Origin = string.IsNullOrWhiteSpace(item.Origin)
                ? "Cebu, Philippines"
                : item.Origin.Trim(),
            TasteProfile = item.TasteProfile.Trim(),
            PreparationTips = item.PreparationTips.Trim(),
            AvailableWeightsGrams = item.AvailableWeightsGrams.Count > 0
                ? item.AvailableWeightsGrams
                : new List<int> { 250, 500, 1000 },
            DeletedAt = null,
            CreatedAt = DateTime.UtcNow
        };

        var res = await _db.From<DriedFishItem>().Insert(toInsert);
        return res.Model ?? toInsert;
    }

    public async Task UpdateAsync(DriedFishItem item)
    {
        await _db.From<DriedFishItem>()
            .Where(p => p.Id == item.Id)
            .Set(p => p.Name, item.Name.Trim().ToUpperInvariant())
            .Set(p => p.LocalName, item.LocalName.Trim())
            .Set(p => p.Description, item.Description.Trim())
            .Set(p => p.PricePerKg, item.PricePerKg)
            .Set(p => p.StockQuantity, item.StockQuantity)
            .Set(p => p.Category, item.Category.Trim())
            .Set(p => p.Origin, string.IsNullOrWhiteSpace(item.Origin) ? "Cebu, Philippines" : item.Origin.Trim())
            .Set(p => p.ImageUrl, string.IsNullOrWhiteSpace(item.ImageUrl) ? "images/products/danggit.png" : item.ImageUrl.Trim())
            .Set(p => p.IsSeasonal, item.IsSeasonal)
            .Update();
    }

    public async Task SoftDeleteAsync(int id)
    {
        await _db.From<DriedFishItem>()
            .Where(p => p.Id == id)
            .Set(p => p.DeletedAt!, DateTime.UtcNow)
            .Update();
    }
}
