using FluentValidation;

namespace AniRate.Application.AnimeCollections.Commands.DeleteManyTitlesFromCollection
{
    public class DeleteManyTitlesFromCollectionCommandValidator : AbstractValidator<DeleteManyTitlesFromCollectionCommand>
    {
        public DeleteManyTitlesFromCollectionCommandValidator()
        {
            RuleFor(deleteManyTitlesFromCollectionCommand =>
                deleteManyTitlesFromCollectionCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(deleteManyTitlesFromCollectionCommand =>
                deleteManyTitlesFromCollectionCommand.Id).NotEqual(Guid.Empty);
            RuleFor(deleteManyTitlesFromCollectionCommand =>
                deleteManyTitlesFromCollectionCommand.AnimeTitlesIds).NotEmpty();
            RuleForEach(deleteManyTitlesFromCollectionCommand =>
                deleteManyTitlesFromCollectionCommand.AnimeTitlesIds).NotEqual(Guid.Empty);
        }
    }
}
