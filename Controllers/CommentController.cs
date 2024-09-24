using api.Data;
using api.DTOs.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;
[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepo;
    private readonly IStockRepository _stockRepo;
    public CommentController(ApplicationDbContext context, ICommentRepository commentRepo, IStockRepository stockRepo)
    {
        _commentRepo = commentRepo;
        
        _stockRepo = stockRepo; 
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
    [HttpPost("{stockId}")]
    public async Task<IActionResult> AddComment([FromRoute] int stockId, CreateCommentRequestDto commentDto)
    {
        if (!await _stockRepo.StockExists(stockId))
        {
            return BadRequest("Stock does not exist");
        }
        var commentModel = commentDto.ToCommentFromCreate(stockId);
        await _commentRepo.AddCommentAsync(commentModel);
        return CreatedAtAction(nameof(GetCommentById), new { id = commentModel.Id }, commentModel);
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


