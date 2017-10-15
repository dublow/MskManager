using FluentValidation;

namespace MskManager.Frontoffice.Models
{
    public class DeezerUserModel
    {
        public string AccessToken { get; set; }
        public int Id { get; set; }
    }

    public class DeezerUserModelValidator : AbstractValidator<DeezerUserModel>
    {
        public DeezerUserModelValidator()
        {
            RuleFor(request => request.Id).NotEmpty();
            RuleFor(request => request.AccessToken).NotEmpty();
        }
    }
}
