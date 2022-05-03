using FluentValidation;


namespace AniRate.Application.AnimeCollections.Queries.GetCollectionDetails
{
    public class GetCollectionDetailsQueryValidator : AbstractValidator<GetCollectionDetailsQuery>
    {
        public GetCollectionDetailsQueryValidator()
        {
            RuleFor(getCollectionDetailsQuery =>
                getCollectionDetailsQuery.UserId).NotEqual(Guid.Empty);
            RuleFor(getCollectionDetailsQuery =>
                getCollectionDetailsQuery.Id).NotEqual(Guid.Empty);
        }
    }
}
