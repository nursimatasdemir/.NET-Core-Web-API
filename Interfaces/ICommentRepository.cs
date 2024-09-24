using api.DTOs.Comment;
using api.Models;

namespace api.Interfaces;

public interface ICommentRepository
{ 
    Task<List<Comment>> GetAllCommentsAsync();
    Task<Comment?> GetCommentByIdAsync(int id);
    Task<Comment> AddCommentAsync(Comment commentModel);
    Task<Comment?> UpdateCommentAsync(int id, Comment commentModel);//Update null olabilir
    Task<Comment?> DeleteCommentAsync(int id);
}