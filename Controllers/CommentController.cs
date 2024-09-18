using api.Data;
using api.DTOs.Comment;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;
[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CommentController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllComments()
    {
        var comments = _context.Comment.ToList()
            .Select(s => s.ToCommentDto());
        return Ok(comments);
    }

    [HttpGet("{id}")]
    public IActionResult GetCommentById([FromRoute] int id)
    {
        var comment = _context.Comment.Find(id);
        if (comment == null)
        {
            return NotFound();
        }
        return Ok(comment.ToCommentDto());
    }

    [HttpPost]
    public IActionResult AddComment([FromBody] CreateCommentRequestDto commentDto)
    {
        var commentModel = commentDto.ToCommentFromCreate();
        _context.Comment.Add(commentModel);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetCommentById), new {commentId = commentModel.Id}, 
            commentModel.ToCommentDto());
            
    }
}