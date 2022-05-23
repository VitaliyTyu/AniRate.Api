using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
