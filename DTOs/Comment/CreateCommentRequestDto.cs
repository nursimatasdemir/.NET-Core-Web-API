using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Comment;

public class CreateCommentRequestDto
{
    [Required]
    [MinLength(5,ErrorMessage ="Comment title must be 5 characters!")]
    [MaxLength(280, ErrorMessage ="Comment title can not be over 280 characters!")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MinLength(5,ErrorMessage ="Comment content must be 5 characters!")]
    [MaxLength(300, ErrorMessage ="Comment content can not be over 300 characters!")]
    public string Content { get; set; } = string.Empty;

}