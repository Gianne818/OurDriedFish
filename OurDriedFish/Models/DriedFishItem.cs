namespace OurDriedFish.Models;

public class DriedFishItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LocalName { get; set; } = string.Empty;
    public string ScientificName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int WeightGrams { get; set; } = 1000;
    public decimal Price { get; set; }
    public decimal PricePerKg { get; set; }
    public int StockQuantity { get; set; } = 25;
    public string Category { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsSeasonal { get; set; }
    public bool InStock => StockQuantity > 0;
    public string Origin { get; set; } = "Cebu, Philippines";
    public string TasteProfile { get; set; } = string.Empty;
    public string PreparationTips { get; set; } = string.Empty;
    public List<int> AvailableWeightsGrams { get; set; } = new() { 250, 500, 1000 };

    public decimal GetPriceForWeight(int grams)
    {
        return Math.Round((PricePerKg * grams) / 1000m, 2);
    }
}
