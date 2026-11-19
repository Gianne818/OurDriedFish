using OurDriedFish.Models;

namespace OurDriedFish.Services;

public interface IReviewService
{
    Task<IReadOnlyList<ProductReview>> GetAllAsync(int? productId = null, bool generalOnly = false, string sortBy = "newest");
    Task<ProductReview> AddAsync(ProductReview review);
    Task UpdateAsync(ProductReview review);
    Task DeleteAsync(int id);
}
