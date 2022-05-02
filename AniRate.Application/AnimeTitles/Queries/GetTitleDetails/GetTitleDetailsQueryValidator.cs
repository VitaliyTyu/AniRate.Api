using FluentValidation;


namespace AniRate.Application.AnimeTitles.Queries.GetTitleDetails
{
    public class GetTitleDetailsQueryValidator : AbstractValidator<GetTitleDetailsQuery>
    {
        public GetTitleDetailsQueryValidator()
        {
            RuleFor(getTitleDetailsQuery =>
                getTitleDetailsQuery.Id).NotEqual(Guid.Empty);
        }
    }
}
