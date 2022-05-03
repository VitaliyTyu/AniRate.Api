using FluentValidation;

namespace AniRate.Application.AnimeCollections.Commands.DeleteTitleFromManyCollections
{
    public class DeleteTitleFromManyCollectionsCommandValidator : AbstractValidator<DeleteTitleFromManyCollectionsCommand>
    {
        public DeleteTitleFromManyCollectionsCommandValidator()
        {
            RuleFor(deleteTitleFromManyCollectionsCommand =>
                deleteTitleFromManyCollectionsCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(deleteTitleFromManyCollectionsCommand =>
                deleteTitleFromManyCollectionsCommand.TitleId).NotEqual(Guid.Empty);
            RuleFor(deleteTitleFromManyCollectionsCommand =>
                deleteTitleFromManyCollectionsCommand.CollectionsIds).NotEmpty();
            RuleForEach(deleteTitleFromManyCollectionsCommand =>
                deleteTitleFromManyCollectionsCommand.CollectionsIds).NotEqual(Guid.Empty);
        }
    }
}