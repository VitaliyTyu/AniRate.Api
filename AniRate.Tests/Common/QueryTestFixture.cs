using AniRate.Application.Common.Mappings;
using AniRate.Application.Interfaces;
using AniRate.Infrastructure.Persistence;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AniRate.Tests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public ApplicationDbContext Context;
        public IMapper Mapper;

        public QueryTestFixture()
        {
            Context = ContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(IApplicationDbContext).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            ContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
