using BlogsApp.Api.Models.Post;

using FluentValidation;

using System;

namespace BlogsApp.Api.Validators
{
    /// <summary>
    /// Update post validator
    /// </summary>
    public class UpdatePostValidator : AbstractValidator<UpdatePost>
    {
        /// <summary>
        /// Update post validation rules set
        /// </summary>
        public UpdatePostValidator()
        {
            RuleFor(x => x.Title)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Title is required.")
                .DependentRules(rules =>
                {
                    rules.RuleFor(x => x.Title)
                        .Must(x => x.Length >= 3 && x.Length <= 20).WithMessage("Title must be between 3 and 20 characters.")
                        .Must(x => x.Contains("a")).WithMessage("Title must contain 'a' symbol.")
                        .Must(x => x.Contains("b")).WithMessage("Title must contain 'b' symbol.");
                });

            RuleFor(x => x.Content).Must(x => !string.IsNullOrEmpty(x)).WithMessage("Content is required.");
        }
    }

    /// <summary>
    /// New post validator
    /// </summary>
    public class NewPostValidator : AbstractValidator<NewPost>
    {
        /// <summary>
        /// New post validation rules set
        /// </summary>
        public NewPostValidator()
        {
            Include(new UpdatePostValidator());
            RuleFor(x => x.UserId).Must(x => x > 0).WithMessage("User id is required.");
        }
    }

    /// <summary>
    /// Post validator
    /// </summary>
    public class PostValidator : AbstractValidator<Post>
    {
        /// <summary>
        /// Post validation rules set
        /// </summary>
        public PostValidator()
        {
            Include(new NewPostValidator());
            RuleFor(x => x.PostId).Must(x => x > 0).WithMessage("Post id is required.");
            RuleFor(x => x.AddedOn).Must(x => x < DateTime.Now).WithMessage("Date added cannot be in the future.");
        }
    }
}
