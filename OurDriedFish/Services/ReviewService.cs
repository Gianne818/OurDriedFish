using OurDriedFish.Models;
using Supabase.Postgrest.Interfaces;
using static Supabase.Postgrest.Constants;

namespace OurDriedFish.Services;

public class ReviewService : IReviewService
{
    private readonly Supabase.Client _db;

    public ReviewService(Supabase.Client db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<ProductReview>> GetAllAsync(int? productId = null, bool generalOnly = false, string sortBy = "newest")
    {
        IPostgrestTable<ProductReview> table = _db.From<ProductReview>();

        if (generalOnly)
        {
            table = table.Where(r => r.ProductId == null);
        }
        else if (productId.HasValue)
        {
            var pid = productId.Value;
            table = table.Where(r => r.ProductId == pid);
        }

        table = sortBy switch
        {
            "highest" => table.Order(r => r.Rating, Ordering.Descending).Order(r => r.CreatedAt, Ordering.Descending),
            "lowest" => table.Order(r => r.Rating, Ordering.Ascending).Order(r => r.CreatedAt, Ordering.Descending),
            _ => table.Order(r => r.CreatedAt, Ordering.Descending)
        };

        var res = await table.Range(0, 49).Get();
        return res.Models;
    }

    public async Task<ProductReview> AddAsync(ProductReview review)
    {
        var toInsert = new ProductReview
        {
            ProductId = review.ProductId,
            AuthorName = review.AuthorName.Trim(),
            Rating = review.Rating,
            Title = review.Title.Trim(),
            Comment = review.Comment.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        var res = await _db.From<ProductReview>().Insert(toInsert);
        return res.Model ?? toInsert;
    }

    public async Task UpdateAsync(ProductReview review)
    {
        await _db.From<ProductReview>()
            .Where(r => r.Id == review.Id)
            .Set(r => r.ProductId!, review.ProductId)
            .Set(r => r.AuthorName, review.AuthorName.Trim())
            .Set(r => r.Rating, review.Rating)
            .Set(r => r.Title, review.Title.Trim())
            .Set(r => r.Comment, review.Comment.Trim())
            .Update();
    }

    public async Task DeleteAsync(int id)
    {
        await _db.From<ProductReview>()
            .Where(r => r.Id == id)
            .Delete();
    }
}
