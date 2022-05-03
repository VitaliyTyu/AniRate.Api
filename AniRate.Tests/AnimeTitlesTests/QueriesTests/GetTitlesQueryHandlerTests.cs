using AniRate.Application.AnimeTitles.Queries;
using AniRate.Application.AnimeTitles.Queries.GetTitles;
using AniRate.Application.Common.Models;
using AniRate.Infrastructure.Persistence;
using AniRate.Tests.Common;
using AutoMapper;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AniRate.Tests.AnimeTitlesTests.QueriesTests
{
    [Collection("QueryCollection")]
    public class GetTitlesQueryHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;

        public GetTitlesQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetTitlesQueryHandler_Success()
        {
            // Arrange
            var handler = new GetTitlesQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetTitlesQuery
                {
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<PaginatedList<BriefTitleVM>>();
            result.TotalCount.ShouldBe(4);
        }
    }
}
