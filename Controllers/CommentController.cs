using api.Data;
using api.DTOs.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;
[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ICommentRepository _commentRepo;
    public CommentController(ApplicationDbContext context, ICommentRepository commentRepo)
    {
        _context = context;
        _commentRepo = commentRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllComments()
    {
        var comments = await _commentRepo.GetAllCommentsAsync();
        var commentDto = comments.Select(s => s.ToCommentDto());
        return Ok(commentDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCommentById([FromRoute] int id)
    {
        var comment = await _commentRepo.GetCommentByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        return Ok(comment.ToCommentDto());
    }

    [HttpPost]
    public async Task<IActionResult> AddComment([FromBody] CreateCommentRequestDto commentDto)
    {
        var commentModel = commentDto.ToCommentFromCreate();
        await _commentRepo.AddCommentAsync(commentModel);
        return CreatedAtAction(nameof(GetCommentById), new {commentId = commentModel.Id}, 
            commentModel.ToCommentDto());
            
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateCommentDto)
    {
        var commentModel = await _commentRepo.UpdateCommentAsync(id, updateCommentDto);
        if (commentModel == null)
        {
            return NotFound();
        }
        return Ok(commentModel.ToCommentDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int id)
    {
        var commentModel = await _commentRepo.DeleteCommentAsync(id);
        if (commentModel == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}


