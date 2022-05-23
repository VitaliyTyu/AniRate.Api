using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeTitles.Queries.SerchAnimes
{
    public class SerchAnimesQueryHandlerValidator : AbstractValidator<SerchAnimesQuery>
    {
        public SerchAnimesQueryHandlerValidator()
        {
            RuleFor(serchAnimesQuery =>
                serchAnimesQuery.SearchString).NotEmpty().MaximumLength(100);
        }
    }
}
