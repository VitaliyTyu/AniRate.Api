﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeTitles.Queries.GetAnimeTitles
{
    public class AnimeTitlesListVM
    {
        public IList<AnimeTitleBriefDto> AnimeTitles { get; set; }
    }
}