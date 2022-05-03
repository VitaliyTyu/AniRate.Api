using FluentValidation;

namespace AniRate.Application.AnimeCollections.Commands.DeleteCollections
{
    public class DeleteCollectionsCommandValidator : AbstractValidator<DeleteCollectionsCommand>
    {
        public DeleteCollectionsCommandValidator()
        {
            RuleFor(deleteCollectionsCommand =>
                deleteCollectionsCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(deleteCollectionsCommand =>
                deleteCollectionsCommand.AnimeCollectionsIds).NotEmpty();
            RuleForEach(deleteCollectionsCommand =>
                deleteCollectionsCommand.AnimeCollectionsIds).NotEqual(Guid.Empty);
        }
    }
}

