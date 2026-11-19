using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace OurDriedFish.Models;

[Table("dried_fish_items")]
public class DriedFishItem : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }

    [Column("name")]
    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Column("local_name")]
    public string LocalName { get; set; } = string.Empty;

    [Column("scientific_name")]
    public string ScientificName { get; set; } = string.Empty;

    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Column("weight_grams")]
    public int WeightGrams { get; set; } = 1000;

    [Column("price_per_kg")]
    [Range(1, 100000, ErrorMessage = "Price per kg must be greater than 0.")]
    public decimal PricePerKg { get; set; }

    [Column("stock_quantity")]
    [Range(0, 100000, ErrorMessage = "Stock quantity cannot be negative.")]
    public int StockQuantity { get; set; } = 25;

    [Column("category")]
    [Required(ErrorMessage = "Category is required.")]
    public string Category { get; set; } = string.Empty;

    [Column("image_url")]
    public string ImageUrl { get; set; } = "images/products/danggit.png";

    [Column("is_seasonal")]
    public bool IsSeasonal { get; set; }

    [Column("origin")]
    public string Origin { get; set; } = "Cebu, Philippines";

    [Column("taste_profile")]
    public string TasteProfile { get; set; } = string.Empty;

    [Column("preparation_tips")]
    public string PreparationTips { get; set; } = string.Empty;

    [Column("available_weights_grams")]
    public List<int> AvailableWeightsGrams { get; set; } = new() { 250, 500, 1000 };

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public bool InStock => StockQuantity > 0;

    public decimal GetPriceForWeight(int grams)
    {
        return Math.Round((PricePerKg * grams) / 1000m, 2);
    }
}
