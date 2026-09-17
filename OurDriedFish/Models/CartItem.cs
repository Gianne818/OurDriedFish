namespace OurDriedFish.Models;

public class CartItem
{
    public DriedFishItem Product { get; set; } = null!;
    public int SelectedWeightGrams { get; set; } = 1000;
    public int Quantity { get; set; } = 1;

    public decimal UnitPrice => Product.GetPriceForWeight(SelectedWeightGrams);
    public decimal TotalPrice => UnitPrice * Quantity;
}
