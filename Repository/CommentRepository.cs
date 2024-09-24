using api.Data;
using api.DTOs.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _context;
    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public  async Task<List<Comment>> GetAllCommentsAsync()
    {
        return await _context.Comment.ToListAsync();
    }
    
    //18 Eylül Çarşamba günü yazılan GetCommentByIdAsync fonksiyonu
    public  async Task<Comment?> GetCommentByIdAsync(int id)
    {
        return await _context.Comment.FindAsync(id);
    }
    

    public  async Task<Comment> AddCommentAsync(Comment commentModel)
    { 
        DateTime localdateTime = DateTime.Now;
        DateTime utcdateTime = localdateTime.ToUniversalTime();
        commentModel.CreatedOn= utcdateTime;
            
        await _context.Comment.AddAsync(commentModel);
        await _context.SaveChangesAsync();
        return commentModel;
    }
    

    public  async Task<Comment?> UpdateCommentAsync(int id, UpdateCommentRequestDto commentDto)
    {
        var existingComment = await _context.Comment.FirstOrDefaultAsync(x=>x.Id == id);
        if (existingComment == null)
        {
            return null;
        }
        
        existingComment.Title = commentDto.Title;
        existingComment.Content = commentDto.Content;
        existingComment.CreatedOn = commentDto.CreatedOn;
        existingComment.StockId = commentDto.StockId;
        
        await _context.SaveChangesAsync();
        return existingComment;
        
    }

    public  async Task<Comment?> DeleteCommentAsync(int id)
    {
        var commentModel = await _context.Comment.FirstOrDefaultAsync(x => x.Id == id);
        if (commentModel == null)
        {
            return null;
        }
        _context.Comment.Remove(commentModel);
        await _context.SaveChangesAsync();
        return commentModel;
    }
}