using OurDriedFish.Models;

namespace OurDriedFish.Services;

public class CartService
{
    private readonly List<CartItem> _items = new();

    public event Action? OnChange;

    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

    public bool IsDrawerOpen { get; private set; }
    public string? LastActionMessage { get; private set; }

    public int TotalItemCount => _items.Sum(i => i.Quantity);

    public decimal Subtotal => _items.Sum(i => i.TotalPrice);

    public void OpenDrawer()
    {
        IsDrawerOpen = true;
        NotifyStateChanged();
    }

    public void CloseDrawer()
    {
        IsDrawerOpen = false;
        NotifyStateChanged();
    }

    public void ToggleDrawer()
    {
        IsDrawerOpen = !IsDrawerOpen;
        NotifyStateChanged();
    }

    public void AddItem(DriedFishItem product, int weightGrams, int quantity = 1)
    {
        if (quantity <= 0) return;

        var existing = _items.FirstOrDefault(i => i.Product.Id == product.Id && i.SelectedWeightGrams == weightGrams);
        if (existing != null)
        {
            existing.Quantity += quantity;
        }
        else
        {
            _items.Add(new CartItem
            {
                Product = product,
                SelectedWeightGrams = weightGrams,
                Quantity = quantity
            });
        }

        LastActionMessage = $"Added {quantity}x {product.Name} ({weightGrams}g) to cart";
        IsDrawerOpen = true; // Automatically open cart drawer to confirm addition
        NotifyStateChanged();
    }

    public void UpdateQuantity(int productId, int weightGrams, int quantity)
    {
        var existing = _items.FirstOrDefault(i => i.Product.Id == productId && i.SelectedWeightGrams == weightGrams);
        if (existing != null)
        {
            if (quantity <= 0)
            {
                _items.Remove(existing);
            }
            else
            {
                existing.Quantity = quantity;
            }
            NotifyStateChanged();
        }
    }

    public void RemoveItem(int productId, int weightGrams)
    {
        var existing = _items.FirstOrDefault(i => i.Product.Id == productId && i.SelectedWeightGrams == weightGrams);
        if (existing != null)
        {
            _items.Remove(existing);
            NotifyStateChanged();
        }
    }

    public void Clear()
    {
        _items.Clear();
        NotifyStateChanged();
    }

    public void ClearNotification()
    {
        LastActionMessage = null;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
