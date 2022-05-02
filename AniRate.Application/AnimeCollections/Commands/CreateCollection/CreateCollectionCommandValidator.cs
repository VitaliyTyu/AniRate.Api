using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Commands.CreateCollection
{
    public class CreateCollectionCommandValidator : AbstractValidator<CreateCollectionCommand>
    {
        public CreateCollectionCommandValidator()
        {
            RuleFor(createCollectionCommand =>
                createCollectionCommand.Name).NotEmpty().MaximumLength(100);
            RuleFor(createCollectionCommand =>
                createCollectionCommand.UserId).NotEqual(Guid.Empty);
            RuleForEach(createCollectionCommand =>
                createCollectionCommand.AnimeTitlesIds).NotEqual(Guid.Empty);
        }
    }
}
