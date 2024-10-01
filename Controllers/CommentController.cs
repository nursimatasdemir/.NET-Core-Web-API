using api.Data;
using api.DTOs.Comment;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public async Task<IActionResult> GetAllComments([FromQuery] CommentQueryObject queryObject)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var comments = await _commentRepo.GetAllCommentsAsync(queryObject);
        var commentDto = comments.Select(s => s.ToCommentDto());
        return Ok(commentDto);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCommentById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var comment = await _commentRepo.GetCommentByIdAsync(id);
    
        if (comment == null)
        {
            return NotFound();
        }
        
        return Ok(comment.ToCommentDto());
    }
    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> AddComment([FromRoute] int stockId, CreateCommentRequestDto commentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (!await _stockRepo.StockExists(stockId))
        {
            return BadRequest("Stock does not exist");
        }
        var commentModel = commentDto.ToCommentFromCreate(stockId);
        await _commentRepo.AddCommentAsync(commentModel);
        return CreatedAtAction(nameof(GetCommentById), new { id = commentModel.Id }, commentModel);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateCommentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var commentModel = await _commentRepo.UpdateCommentAsync(
            id, updateCommentDto.ToCommentFromUpdate());
        
        if (commentModel == null)
        {
            return NotFound("Comment not found");
        }
        
        return Ok(commentModel.ToCommentDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var commentModel = await _commentRepo.DeleteCommentAsync(id);
        if (commentModel == null)
        {
            return NotFound("Comment does not exist");
        }

        return Ok(commentModel);
    }


}


