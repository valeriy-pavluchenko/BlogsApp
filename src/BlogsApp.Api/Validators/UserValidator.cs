using BlogsApp.Api.Models.User;

using FluentValidation;
using FluentValidation.Results;

namespace BlogsApp.Api.Validators
{
    /// <summary>
    /// User validator
    /// </summary>
    public class UserValidator : AbstractValidator<User>
    {
        /// <summary>
        /// User validation rules set
        /// </summary>
        public UserValidator()
        {
        }
    }
}
