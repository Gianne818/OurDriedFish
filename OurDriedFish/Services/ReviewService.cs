using OurDriedFish.Models;

namespace OurDriedFish.Services;

public class ReviewService : IReviewService
{
    private readonly List<ProductReview> _reviews;
    private int _nextId;
    private readonly object _lock = new();

    public ReviewService()
    {
        _reviews = new List<ProductReview>
        {
            new() { Id = 1, ProductId = 1, AuthorName = "Maria Santos", Rating = 5, Title = "Best danggit outside Bantayan", Comment = "Crispy fins, clean salt, and arrived vacuum-sealed fresh. Flash-fried for a minute and served with sukang maanghang.", CreatedAt = DateTime.UtcNow.AddDays(-2) },
            new() { Id = 2, ProductId = 1, AuthorName = "Jose Ramirez", Rating = 4, Title = "Great, a touch salty", Comment = "Excellent texture and crunch. I rinse it briefly before frying to tame the saltiness. Will order again.", CreatedAt = DateTime.UtcNow.AddDays(-9) },
            new() { Id = 3, ProductId = 2, AuthorName = "Aileen Cruz", Rating = 5, Title = "Sweet and chewy pusit", Comment = "Tastes like Bogo market sun-dried squid. Pan-fried 30 seconds per side and it stayed tender, not rubbery.", CreatedAt = DateTime.UtcNow.AddDays(-5) },
            new() { Id = 4, ProductId = 4, AuthorName = "Mark Villanueva", Rating = 5, Title = "Crispy dilis for breakfast", Comment = "Whole intact dilis, perfect for sweet-spicy glaze with peanuts. Kids finished the whole batch.", CreatedAt = DateTime.UtcNow.AddDays(-12) },
            new() { Id = 5, ProductId = 9, AuthorName = "Lola Nena", Rating = 4, Title = "Tuyo like home", Comment = "Authentic robust tuyo flavor. Fry with the lid on as advised. Perfect with champorado.", CreatedAt = DateTime.UtcNow.AddDays(-20) },
            new() { Id = 6, ProductId = 6, AuthorName = "Kevin Tan", Rating = 5, Title = "Flounder chips", Comment = "Turned glass-crisp in coconut oil. Light salinity, great with tomato and shallot salad.", CreatedAt = DateTime.UtcNow.AddDays(-7) },
            new() { Id = 7, ProductId = null, AuthorName = "Teresa Beth", Rating = 5, Title = "Fast shipping to Miami", Comment = "Ordered mixed 3kg for our restaurant. FDA-compliant packs, well-labeled, arrived in 4 days. Excellent support.", CreatedAt = DateTime.UtcNow.AddDays(-3) },
            new() { Id = 8, ProductId = null, AuthorName = "Dan Meyer", Rating = 4, Title = "Great quality, wish for more", Comment = "Overall quality is top-notch and packaging is excellent. Would love larger bulk discounts for wholesale.", CreatedAt = DateTime.UtcNow.AddDays(-15) },
        };
        _nextId = _reviews.Max(r => r.Id) + 1;
    }

    public Task<IReadOnlyList<ProductReview>> GetAllAsync()
    {
        IReadOnlyList<ProductReview> snapshot;
        lock (_lock)
        {
            snapshot = _reviews.OrderByDescending(r => r.CreatedAt).ToList();
        }
        return Task.FromResult(snapshot);
    }

    public Task<double> GetAverageRatingAsync(int? productId = null)
    {
        lock (_lock)
        {
            var query = _reviews.AsEnumerable();
            if (productId.HasValue)
            {
                query = query.Where(r => r.ProductId == productId.Value);
            }
            var list = query.ToList();
            var avg = list.Count == 0 ? 0 : list.Average(r => r.Rating);
            return Task.FromResult(Math.Round(avg, 1));
        }
    }

    public Task<int> GetReviewCountAsync(int? productId = null)
    {
        lock (_lock)
        {
            var count = productId.HasValue
                ? _reviews.Count(r => r.ProductId == productId.Value)
                : _reviews.Count;
            return Task.FromResult(count);
        }
    }

    public Task<ProductReview> AddAsync(ProductReview review)
    {
        lock (_lock)
        {
            review.Id = _nextId++;
            review.CreatedAt = DateTime.UtcNow;
            _reviews.Add(review);
            return Task.FromResult(review);
        }
    }
}
