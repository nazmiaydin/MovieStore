using FluentValidation;

namespace MovieStore.WebApi.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
    {
        public UpdateActorCommandValidator()
        {
            RuleFor(command => command.ActorId).GreaterThan(0);
            RuleFor(command => command.Model.Name).MinimumLength(3).When(x => x.Model.Name != string.Empty);
            RuleFor(command => command.Model.Surname).MinimumLength(3).When(x => x.Model.Surname != string.Empty);
        }
    }
}