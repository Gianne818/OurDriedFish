namespace OurDriedFish.Models;

public class CartItem
{
    public DriedFishItem Product { get; set; } = null!;
    public int QuantityKg { get; set; } = 1;
    public decimal TotalPrice => Product.PricePerKg * QuantityKg;
}
