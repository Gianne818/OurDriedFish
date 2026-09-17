using OurDriedFish.Models;

namespace OurDriedFish.Services;

public class DriedFishRepository : IDriedFishRepository
{
    private readonly List<DriedFishItem> _items = new()
    {
        new DriedFishItem
        {
            Id = 1,
            Name = "DANGGIT",
            LocalName = "Bulad Danggit (Rabbitfish)",
            ScientificName = "Siganus canaliculatus",
            Description = "Premium Bantayan Island sun-dried rabbitfish. Delicate, crispy when flash-fried, with a savory, distinct sea salt finish.",
            Price = 900.00m,
            PricePerKg = 900.00m,
            StockQuantity = 45,
            Category = "Rabbitfish & Spinefoot",
            ImageUrl = "images/products/danggit.png",
            IsSeasonal = true,
            Origin = "Bantayan Island, Cebu",
            TasteProfile = "Savory, crisp, light umami, delightfully crunchy fins",
            PreparationTips = "Deep fry in medium heat for 45 to 60 seconds until golden brown. Serve with spicy spiced coconut vinegar and garlic rice."
        },
        new DriedFishItem
        {
            Id = 2,
            Name = "DRIED SQUID",
            LocalName = "Pusit Sun-Dried",
            ScientificName = "Loligo edulis",
            Description = "Tender, hand-cured whole squids harvested from the Tañon Strait. Naturally sun-dried on bamboo mesh for a deep, rich caramelized sweetness.",
            Price = 1100.00m,
            PricePerKg = 1100.00m,
            StockQuantity = 30,
            Category = "Cephalopods",
            ImageUrl = "images/products/dried-squid.png",
            IsSeasonal = true,
            Origin = "Bogo City, Cebu",
            TasteProfile = "Sweet-savory, rich chew, robust umami aroma",
            PreparationTips = "Pan fry quickly in a touch of oil for 30 seconds per side. Do not overcook to preserve tender chewiness."
        },
        new DriedFishItem
        {
            Id = 3,
            Name = "FISH TOCINO",
            LocalName = "Sweet Cured Fish Fillet",
            ScientificName = "Oreochromis niloticus / Chanos chanos",
            Description = "Artisanal sweet-cured, marinated boneless fish fillets air-dried to lock in natural sugars and sea minerals. A vibrant breakfast favorite.",
            Price = 800.00m,
            PricePerKg = 800.00m,
            StockQuantity = 25,
            Category = "Cured & Marinated",
            ImageUrl = "images/products/fish-tocino.png",
            IsSeasonal = false,
            Origin = "Hagnaya, Cebu",
            TasteProfile = "Sweet glazed exterior, tender flaky meat, subtle caramelized garlic notes",
            PreparationTips = "Simmer in 2 tablespoons of water until dry, then lightly fry in its own natural caramel glaze."
        },
        new DriedFishItem
        {
            Id = 4,
            Name = "DRIED ANCHOVIES",
            LocalName = "Dilis Crispy Fry",
            ScientificName = "Encrasicholina punctifer",
            Description = "Grade-A sun-dried silver anchovies. Unsalted or lightly salted options with whole intact bodies, perfect for candied snacks or crispy breakfasts.",
            Price = 600.00m,
            PricePerKg = 600.00m,
            StockQuantity = 60,
            Category = "Anchovies & Dilis",
            ImageUrl = "images/products/dried-anchovies.png",
            IsSeasonal = false,
            Origin = "Medellin, Cebu",
            TasteProfile = "Crispy, mild salty bite, clean sea aroma",
            PreparationTips = "Toast in oil with peanuts and chili flakes, or glaze with brown sugar and sesame seeds for sweet-spicy dilis."
        },
        new DriedFishItem
        {
            Id = 5,
            Name = "DRIED KATAMBAK",
            LocalName = "Katambak / Bridled Spinecheek",
            ScientificName = "Lethrinus nebulosus",
            Description = "Split and butterfly-cut emperor bream sun-cured with unrefined rock salt. Thick, flavorful meat with pristine white flaky texture.",
            Price = 700.00m,
            PricePerKg = 700.00m,
            StockQuantity = 20,
            Category = "Whitefish & Snappers",
            ImageUrl = "images/products/dried-katambak.png",
            IsSeasonal = false,
            Origin = "San Remigio, Cebu",
            TasteProfile = "Hearty, clean salt profile, flaky and savory",
            PreparationTips = "Pan fry in moderate vegetable oil until outer skin is crispy while meat remains tender. Pair with sliced green mangoes."
        },
        new DriedFishItem
        {
            Id = 6,
            Name = "DRIED FLOUNDER",
            LocalName = "Dapa / Dried Flatfish",
            ScientificName = "Paralichthys olivaceus",
            Description = "Hand-selected flounder flatfish cured immediately upon harbor arrival. Delivers a uniquely delicate, thin meat that turns remarkably brittle and chips-like.",
            Price = 700.00m,
            PricePerKg = 700.00m,
            StockQuantity = 18,
            Category = "Flatfish",
            ImageUrl = "images/products/dried-flounder.png",
            IsSeasonal = true,
            Origin = "Bantayan Island, Cebu",
            TasteProfile = "Ultra-crisp, light salinity, delicate fish aroma",
            PreparationTips = "Flash fry in high-heat coconut oil for 1 minute until glass-crisp. Pairs wonderfully with tomato and shallot salad."
        },
        new DriedFishItem
        {
            Id = 7,
            Name = "BISUGO",
            LocalName = "Threadfin Bream Pinwheel",
            ScientificName = "Nemipterus japonicus",
            Description = "Sunburst circular presentation of threadfin breams. Cleaned, butterflied, and arranged in traditional wheel drying arrays under tropical sun.",
            Price = 550.00m,
            PricePerKg = 550.00m,
            StockQuantity = 35,
            Category = "Whitefish & Snappers",
            ImageUrl = "images/products/bisugo.png",
            IsSeasonal = false,
            Origin = "Daanbantayan, Cebu",
            TasteProfile = "Nutty, medium salt, firm golden meat",
            PreparationTips = "Fry each wheel intact for a stunning breakfast centerpiece. Dip in sinamak spiced vinegar."
        },
        new DriedFishItem
        {
            Id = 8,
            Name = "DALAGANG BUKID",
            LocalName = "Yellowtail Fusilier",
            ScientificName = "Caesio cuning",
            Description = "Whole split reef fusilier with natural golden-yellow hues preserved during the gentle solar drying cycle. Substantial meat ideal for sharing.",
            Price = 500.00m,
            PricePerKg = 500.00m,
            StockQuantity = 22,
            Category = "Whitefish & Snappers",
            ImageUrl = "images/products/dalagang-bukid.png",
            IsSeasonal = false,
            Origin = "Malapascua, Cebu",
            TasteProfile = "Mild sweetness, rich savory skin, dense flaky bite",
            PreparationTips = "Grill lightly over charcoal or shallow fry until edges turn bronze."
        },
        new DriedFishItem
        {
            Id = 9,
            Name = "DRIED HERRING",
            LocalName = "Tuyô / Tamban Smoked & Salted",
            ScientificName = "Sardinella fimbriata",
            Description = "The quintessential Philippine staple. Deeply salted, whole dried herrings with robust, unmistakable aroma and intense umami depth.",
            Price = 300.00m,
            PricePerKg = 300.00m,
            StockQuantity = 75,
            Category = "Herring & Sardines",
            ImageUrl = "images/products/dried-herring.png",
            IsSeasonal = false,
            Origin = "Estancia & Cebu Waters",
            TasteProfile = "Robust salt punch, smoky notes, authentic rustic flavor",
            PreparationTips = "Pan fry in enclosed pan to contain rich aromas. Sublime pairing with chocolate champorado or garlic sinangag."
        }
    };

    public Task<IReadOnlyList<DriedFishItem>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<DriedFishItem>>(_items);
    }

    public Task<DriedFishItem?> GetByIdAsync(int id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        return Task.FromResult(item);
    }

    public Task<IReadOnlyList<DriedFishItem>> GetSeasonalAsync()
    {
        var items = _items.Where(i => i.IsSeasonal).ToList();
        return Task.FromResult<IReadOnlyList<DriedFishItem>>(items);
    }

    public Task<IReadOnlyList<DriedFishItem>> SearchAsync(string? query, string? category = null)
    {
        var filtered = _items.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(category) && !category.Equals("All", StringComparison.OrdinalIgnoreCase))
        {
            filtered = filtered.Where(i => i.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(query))
        {
            var q = query.Trim();
            filtered = filtered.Where(i =>
                i.Name.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                i.LocalName.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                i.Description.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                i.Category.Contains(q, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult<IReadOnlyList<DriedFishItem>>(filtered.ToList());
    }

    public Task<IReadOnlyList<string>> GetCategoriesAsync()
    {
        var categories = _items.Select(i => i.Category).Distinct().OrderBy(c => c).ToList();
        categories.Insert(0, "All");
        return Task.FromResult<IReadOnlyList<string>>(categories);
    }
}
