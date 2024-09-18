using api.DTOs.Comment;
using api.Models;

namespace api.Interfaces;

public abstract class ICommentRepository
{
    public abstract Task<List<Comment>> GetAllCommentsAsync();
    public abstract Task<Comment?> GetCommentByIdAsync(int id);
    public abstract Task<Comment> AddCommentAsync(Comment commentModel);
    public abstract Task<Comment?> UpdateCommentAsync(int id, UpdateCommentRequestDto commentModel);
    public abstract Task<Comment?> DeleteCommentAsync(int id);
}