using AniRate.Application.AnimeCollections.Queries;
using AniRate.Application.AnimeCollections.Queries.GetCollections;
using AniRate.Application.Common.Models;
using AniRate.Infrastructure.Persistence;
using AniRate.Tests.Common;
using AutoMapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AniRate.Tests.AnimeCollectionsTest.QueriesTests
{
    [Collection("QueryCollection")]
    public class GetCollectionsQueryHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;

        public GetCollectionsQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetCollectionsQueryHandler_Success()
        {
            // Arrange
            var handler = new GetCollectionsQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetCollectionsQuery
                {
                    UserId = ContextFactory.UserAId
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<PaginatedList<BriefCollectionVM>>();
            result.TotalCount.ShouldBe(2);
        }
    }
}
