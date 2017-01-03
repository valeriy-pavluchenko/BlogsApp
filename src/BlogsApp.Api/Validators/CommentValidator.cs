using BlogsApp.Api.Models.Comment;

using FluentValidation;

using System;

namespace BlogsApp.Api.Validators
{
    /// <summary>
    /// Update comment validator
    /// </summary>
    public class UpdateCommentValidator : AbstractValidator<UpdateComment>
    {
        /// <summary>
        /// Update comment validation rules set
        /// </summary>
        public UpdateCommentValidator()
        {
            RuleFor(x => x.Message).Must(x => !string.IsNullOrEmpty(x)).WithMessage("Message is required.");
        }
    }

    /// <summary>
    /// New comment validator
    /// </summary>
    public class NewCommentValidator : AbstractValidator<NewComment>
    {
        /// <summary>
        /// New comment validation rules set
        /// </summary>
        public NewCommentValidator()
        {
            Include(new UpdateCommentValidator());
            RuleFor(x => x.PostId).Must(x => x > 0).WithMessage("Post id is required.");
            RuleFor(x => x.UserId).Must(x => x > 0).WithMessage("User id is required.");
        }
    }

    /// <summary>
    /// Comment validator
    /// </summary>
    public class CommentValidator : AbstractValidator<Comment>
    {
        /// <summary>
        /// Comment validation rules set
        /// </summary>
        public CommentValidator()
        {
            Include(new NewCommentValidator());
            RuleFor(x => x.CommentId).Must(x => x > 0).WithMessage("Comment id is required.");
            RuleFor(x => x.AddedOn).Must(x => x < DateTime.Now).WithMessage("Date added cannot be in the future.");
        }
    }
}
