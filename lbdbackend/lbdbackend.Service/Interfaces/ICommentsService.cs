using lbdbackend.Core.Entities;
using lbdbackend.Service.DTOs.CommentDTOs;
using lbdbackend.Service.DTOs.GenreDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lbdbackend.Service.Interfaces {
    public interface ICommentService {
        Task CreateComment(CommentCreateDTO commentCreateDTO);
        Task<List<CommentGetDTO>> GetReviewComments(int? reviewID);
        Task Delete(int? id);

        }
    }