namespace OurDriedFish.Services;

using OurDriedFish.Models;

public class CartService
{
    private readonly List<CartItem> _items = new();

    public IReadOnlyList<CartItem> Items => _items;

    public bool IsOpen { get; private set; }

    public int TotalWeightKg => _items.Sum(item => item.QuantityKg);

    public int TotalUniqueItems => _items.Count;

    public decimal Subtotal => _items.Sum(item => item.TotalPrice);

    public event Action? OnChange;

    public void OpenCart()
    {
        IsOpen = true;
        NotifyStateChanged();
    }

    public void CloseCart()
    {
        IsOpen = false;
        NotifyStateChanged();
    }

    public void ToggleCart()
    {
        IsOpen = !IsOpen;
        NotifyStateChanged();
    }

    public void AddItem(DriedFishItem product, int quantityKg = 1, bool openDrawer = true)
    {
        if (quantityKg <= 0) return;

        var existingItem = _items.FirstOrDefault(i => i.Product.Id == product.Id);
        if (existingItem != null)
        {
            existingItem.QuantityKg += quantityKg;
        }
        else
        {
            _items.Add(new CartItem
            {
                Product = product,
                QuantityKg = quantityKg
            });
        }

        if (openDrawer)
        {
            IsOpen = true;
        }

        NotifyStateChanged();
    }

    public void UpdateQuantity(int productId, int quantityKg)
    {
        var item = _items.FirstOrDefault(i => i.Product.Id == productId);
        if (item != null)
        {
            if (quantityKg <= 0)
            {
                _items.Remove(item);
            }
            else
            {
                item.QuantityKg = quantityKg;
            }
            NotifyStateChanged();
        }
    }

    public void Increment(int productId, int amount = 1)
    {
        var item = _items.FirstOrDefault(i => i.Product.Id == productId);
        if (item != null)
        {
            item.QuantityKg += amount;
            NotifyStateChanged();
        }
    }

    public void Decrement(int productId, int amount = 1)
    {
        var item = _items.FirstOrDefault(i => i.Product.Id == productId);
        if (item != null)
        {
            item.QuantityKg -= amount;
            if (item.QuantityKg <= 0)
            {
                _items.Remove(item);
            }
            NotifyStateChanged();
        }
    }

    public void RemoveItem(int productId)
    {
        var item = _items.FirstOrDefault(i => i.Product.Id == productId);
        if (item != null)
        {
            _items.Remove(item);
            NotifyStateChanged();
        }
    }

    public void Clear()
    {
        _items.Clear();
        NotifyStateChanged();
    }

    public int GetQuantity(int productId)
    {
        return _items.FirstOrDefault(i => i.Product.Id == productId)?.QuantityKg ?? 0;
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
