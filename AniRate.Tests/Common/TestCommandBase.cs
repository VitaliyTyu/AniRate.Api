using AniRate.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace AniRate.Tests.Common
{
    public class TestCommandBase : IDisposable
    {
        protected readonly ApplicationDbContext Context;

        public TestCommandBase()
        {
            Context = ContextFactory.Create();
        }

        public void Dispose()
        {
            ContextFactory.Destroy(Context);
        }
    }
}
