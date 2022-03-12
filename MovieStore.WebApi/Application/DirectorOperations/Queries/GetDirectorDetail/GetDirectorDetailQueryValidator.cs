using FluentValidation;
using MovieStore.WebApi.Application.DirectorOperations.Queries.GetDirector;

namespace MovieStore.WebApi.Application.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorDetailQueryValidator : AbstractValidator<GetDirectorDetailQuery>
    {
        public GetDirectorDetailQueryValidator()
        {
            RuleFor(command => command.DirectorId).GreaterThan(0);
        }
    }
}