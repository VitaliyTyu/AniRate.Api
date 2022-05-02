using FluentValidation;


namespace AniRate.Application.AnimeCollections.Queries.GetCollections
{
    public class GetCollectionsQueryValidator : AbstractValidator<GetCollectionsQuery>
    {
        public GetCollectionsQueryValidator()
        {
            RuleFor(getCollectionsQuery =>
                getCollectionsQuery.UserId).NotEqual(Guid.Empty);
        }
    }
}
