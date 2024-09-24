using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Comment;

public class UpdateCommentRequestDto
{
    [Required]
    [MinLength(5, ErrorMessage = "Comment title must be at least 5 characters long.")]
    [MaxLength(280, ErrorMessage = "Comment title must be between 5 and 280 characters long.")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MinLength(5, ErrorMessage = "Comment content must be at least 5 characters long.")]
    [MaxLength(300, ErrorMessage = "Comment content must be between 5 and 300 characters long.")]
    public string Content { get; set; } = string.Empty;
}