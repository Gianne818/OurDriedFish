using OurDriedFish.Models;

namespace OurDriedFish.Services;

public interface IReviewService
{
    Task<IReadOnlyList<ProductReview>> GetAllAsync();
    Task<double> GetAverageRatingAsync(int? productId = null);
    Task<int> GetReviewCountAsync(int? productId = null);
    Task<ProductReview> AddAsync(ProductReview review);
}
