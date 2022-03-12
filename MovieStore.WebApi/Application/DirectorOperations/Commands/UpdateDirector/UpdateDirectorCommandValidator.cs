using FluentValidation;

namespace MovieStore.WebApi.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommandValidator : AbstractValidator<UpdateDirectorCommand>
    {
        public UpdateDirectorCommandValidator()
        {
            RuleFor(command => command.DirectorId).GreaterThan(0);
            RuleFor(command => command.Model.Name).MinimumLength(3).When(x => x.Model.Name != string.Empty);
            RuleFor(command => command.Model.Surname).MinimumLength(3).When(x => x.Model.Surname != string.Empty);
        }
    }
}