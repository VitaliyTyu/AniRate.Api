using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Commands.AddTitlesInCollections
{
    public class AddTitlesInCollectionsCommandValidator : AbstractValidator<AddTitlesInCollectionsCommand>
    {
        public AddTitlesInCollectionsCommandValidator()
        {
            RuleFor(addTitlesInCollectionsCommand =>
                addTitlesInCollectionsCommand.UserId).NotEqual(Guid.Empty);

            RuleFor(addTitlesInCollectionsCommand =>
                addTitlesInCollectionsCommand.CollectionsIds).NotEmpty();
            RuleForEach(addTitlesInCollectionsCommand =>
                addTitlesInCollectionsCommand.CollectionsIds).NotEqual(Guid.Empty);

            RuleFor(addTitlesInCollectionsCommand =>
                addTitlesInCollectionsCommand.AnimeTitlesIds).NotEmpty();
            RuleForEach(addTitlesInCollectionsCommand =>
                addTitlesInCollectionsCommand.AnimeTitlesIds).NotEqual(Guid.Empty);
        }
    }
}
