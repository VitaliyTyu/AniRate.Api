using FluentValidation;

namespace AniRate.Application.AnimeTitles.Queries.GetTitlesFromCollection
{
    public class GetTitlesFromCollectionQueryValidator : AbstractValidator<GetTitlesFromCollectionQuery>
    {
        public GetTitlesFromCollectionQueryValidator()
        {
            RuleFor(getTitlesFromCollectionQuery =>
                getTitlesFromCollectionQuery.UserId).NotEqual(Guid.Empty);

            RuleFor(getTitlesFromCollectionQuery =>
                getTitlesFromCollectionQuery.CollectionId).NotEqual(Guid.Empty);
        }
    }
}
