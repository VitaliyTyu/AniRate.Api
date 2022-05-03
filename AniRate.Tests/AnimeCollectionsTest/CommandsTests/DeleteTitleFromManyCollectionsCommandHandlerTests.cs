using AniRate.Application.AnimeCollections.Commands.DeleteTitleFromManyCollections;
using AniRate.Application.Common.Exceptions;
using AniRate.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AniRate.Tests.AnimeCollectionsTest.CommandsTests
{
    public class DeleteTitleFromManyCollectionsCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteManyTitlesFromCollectionCommandHandler_Success()
        {
            // Arrange
            var handler = new DeleteTitleFromManyCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();
            collectionsIds.Add(ContextFactory.ThirdCollectionId);
            collectionsIds.Add(ContextFactory.FourthCollectionId);


            // Act
            await handler.Handle(
                new DeleteTitleFromManyCollectionsCommand
                {
                    TitleId = ContextFactory.FourthAnimeId,
                    CollectionsIds = collectionsIds,
                    UserId = ContextFactory.UserBId,
                },
                CancellationToken.None);


            //Assert
            foreach (var collectionId in collectionsIds)
            {
                Assert.Null(
                    await Context.AnimeCollections.SingleOrDefaultAsync(collection =>
                        collection.Id == collectionId &&
                        collection.AnimeTitles.Any(a => a.Id == ContextFactory.FourthAnimeId)));
            }
        }

        [Fact]
        public async Task DeleteManyTitlesFromCollectionCommandHandler_FailOnWrongAnimeId()
        {
            // Arrange
            var handler = new DeleteTitleFromManyCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();
            collectionsIds.Add(ContextFactory.ThirdCollectionId);
            collectionsIds.Add(ContextFactory.FourthCollectionId);


            // Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteTitleFromManyCollectionsCommand
                    {
                        TitleId = Guid.NewGuid(),
                        CollectionsIds = collectionsIds,
                        UserId = ContextFactory.UserBId,
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteManyTitlesFromCollectionCommandHandler_FailOnWrongCollectionId()
        {
            // Arrange
            var handler = new DeleteTitleFromManyCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();
            collectionsIds.Add(ContextFactory.ThirdCollectionId);
            collectionsIds.Add(Guid.NewGuid());


            // Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteTitleFromManyCollectionsCommand
                    {
                        TitleId = ContextFactory.FourthAnimeId,
                        CollectionsIds = collectionsIds,
                        UserId = ContextFactory.UserBId,
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteManyTitlesFromCollectionCommandHandler_FailOnEmptyCollectionsIds()
        {
            // Arrange
            var handler = new DeleteTitleFromManyCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();


            // Act

            //Assert
            await Assert.ThrowsAsync<EmptyStateException>(async () =>
                await handler.Handle(
                    new DeleteTitleFromManyCollectionsCommand
                    {
                        TitleId = ContextFactory.FourthAnimeId,
                        CollectionsIds = collectionsIds,
                        UserId = ContextFactory.UserBId,
                    },
                    CancellationToken.None));
        }
    }
}
