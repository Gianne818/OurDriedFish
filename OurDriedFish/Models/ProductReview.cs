using System.ComponentModel.DataAnnotations;

namespace OurDriedFish.Models;

public class ProductReview
{
    public int Id { get; set; }

    /// <summary>
    /// Null = general site feedback. Otherwise matches DriedFishItem.Id.
    /// </summary>
    public int? ProductId { get; set; }

    [Required(ErrorMessage = "Please provide your name.")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 80 characters.")]
    public string AuthorName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a star rating.")]
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5 stars.")]
    public int Rating { get; set; }

    [StringLength(100, ErrorMessage = "Title must be 100 characters or fewer.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please write a comment.")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "Comment must be at least 10 characters.")]
    public string Comment { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
