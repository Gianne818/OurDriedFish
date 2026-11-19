using System.ComponentModel.DataAnnotations;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace OurDriedFish.Models;

[Table("product_reviews")]
public class ProductReview : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }

    /// <summary>
    /// Null = general site feedback. Otherwise matches DriedFishItem.Id.
    /// </summary>
    [Column("product_id")]
    public int? ProductId { get; set; }

    [Column("author_name")]
    [Required(ErrorMessage = "Please provide your name.")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 80 characters.")]
    public string AuthorName { get; set; } = string.Empty;

    [Column("rating")]
    [Required(ErrorMessage = "Please select a star rating.")]
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5 stars.")]
    public int Rating { get; set; }

    [Column("title")]
    [StringLength(100, ErrorMessage = "Title must be 100 characters or fewer.")]
    public string Title { get; set; } = string.Empty;

    [Column("comment")]
    [Required(ErrorMessage = "Please write a comment.")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "Comment must be at least 10 characters.")]
    public string Comment { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
