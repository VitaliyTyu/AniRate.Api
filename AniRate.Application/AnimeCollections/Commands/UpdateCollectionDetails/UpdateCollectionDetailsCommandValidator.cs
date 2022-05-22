using FluentValidation;

namespace AniRate.Application.AnimeCollections.Commands.UpdateCollectionDetails
{
    public class UpdateCollectionDetailsCommandValidator : AbstractValidator<UpdateCollectionDetailsCommand>
    {
        public UpdateCollectionDetailsCommandValidator()
        {
            RuleFor(updateCollectionDetailsCommand =>
                updateCollectionDetailsCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(updateCollectionDetailsCommand =>
                updateCollectionDetailsCommand.Id).NotEqual(Guid.Empty);
            RuleFor(updateCollectionDetailsCommand =>
                updateCollectionDetailsCommand.Name).NotEmpty().MaximumLength(100);
            RuleFor(updateCollectionDetailsCommand =>
                updateCollectionDetailsCommand.UserComment).MaximumLength(2000);
        }
    }
}
