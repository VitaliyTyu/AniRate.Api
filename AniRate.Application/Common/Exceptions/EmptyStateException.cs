using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.Common.Exceptions
{
    public class EmptyStateException : Exception
    {
        public EmptyStateException(string name) : base($"Entity \"{name}\" must be not empty")
        {
        }
    }
}
