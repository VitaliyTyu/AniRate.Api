using FluentValidation;


namespace AniRate.Application.AnimeCollections.Queries.SearchCollections
{
    public class SearchCollectionsQueryHandlerValidator : AbstractValidator<SearchCollectionsQuery>
    {
        public SearchCollectionsQueryHandlerValidator()
        {
            RuleFor(serchAnimesQuery =>
                serchAnimesQuery.SearchString).NotEmpty().MaximumLength(100);
        }
    }
}