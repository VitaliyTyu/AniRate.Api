using AniRate.Application.AnimeCollections.Commands.DeleteCollections;
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
    public class DeleteCollectionsCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteCollectionsCommandHandler_DeleteOne_Success()
        {
            // Arrange
            var handler = new DeleteCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();
            collectionsIds.Add(ContextFactory.FirstCollectionId);

            // Act
            await handler.Handle(
                new DeleteCollectionsCommand
                {
                    UserId = ContextFactory.UserAId,
                    AnimeCollectionsIds = collectionsIds,
                },
                CancellationToken.None);


            //Assert

            foreach (var collectionId in collectionsIds)
            {
                Assert.Null(
                    await Context.AnimeCollections.SingleOrDefaultAsync(collection =>
                        collection.Id == collectionId));
            }
        }

        [Fact]
        public async Task DeleteCollectionsCommandHandler_DeleteMany_Success()
        {
            // Arrange
            var handler = new DeleteCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();
            collectionsIds.Add(ContextFactory.FirstCollectionId);
            collectionsIds.Add(ContextFactory.SecondCollectionId);


            // Act
            await handler.Handle(
                new DeleteCollectionsCommand
                {
                    UserId = ContextFactory.UserAId,
                    AnimeCollectionsIds = collectionsIds,
                },
                CancellationToken.None);


            //Assert

            foreach (var collectionId in collectionsIds)
            {
                Assert.Null(
                    await Context.AnimeCollections.SingleOrDefaultAsync(collection =>
                        collection.Id == collectionId));
            }
        }

        [Fact]
        public async Task DeleteCollectionsCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var handler = new DeleteCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();
            collectionsIds.Add(ContextFactory.FirstCollectionId);
            collectionsIds.Add(ContextFactory.SecondCollectionId);


            // Act


            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteCollectionsCommand
                    {
                        UserId = ContextFactory.UserBId,
                        AnimeCollectionsIds = collectionsIds,
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteCollectionsCommandHandler_FailOnWrongCollectionId()
        {
            // Arrange
            var handler = new DeleteCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();
            collectionsIds.Add(Guid.NewGuid());
            collectionsIds.Add(ContextFactory.SecondCollectionId);


            // Act


            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteCollectionsCommand
                    {
                        UserId = ContextFactory.UserBId,
                        AnimeCollectionsIds = collectionsIds,
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteCollectionsCommandHandler_FailOnWrongEmptyCollectionsIds()
        {
            // Arrange
            var handler = new DeleteCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();


            // Act


            //Assert
            await Assert.ThrowsAsync<EmptyStateException>(async () =>
                await handler.Handle(
                    new DeleteCollectionsCommand
                    {
                        UserId = ContextFactory.UserBId,
                        AnimeCollectionsIds = collectionsIds,
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteCollectionsCommandHandler_NoOneDeleted_FailOnWrongCollectionId()
        {
            // Arrange
            var handler = new DeleteCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();
            collectionsIds.Add(ContextFactory.SecondCollectionId);
            collectionsIds.Add(Guid.NewGuid());
            collectionsIds.Add(ContextFactory.FirstCollectionId);


            // Act


            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteCollectionsCommand
                    {
                        UserId = ContextFactory.UserAId,
                        AnimeCollectionsIds = collectionsIds,
                    },
                    CancellationToken.None));

            Assert.NotNull(await Context.AnimeCollections.SingleOrDefaultAsync(c => c.Id == ContextFactory.SecondCollectionId, CancellationToken.None));
            Assert.NotNull(await Context.AnimeCollections.SingleOrDefaultAsync(c => c.Id == ContextFactory.FirstCollectionId, CancellationToken.None));

        }

        [Fact]
        public async Task DeleteCollectionsCommandHandler_NoOneDeleted_FailOnWrongUserId()
        {
            // Arrange
            var handler = new DeleteCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();
            collectionsIds.Add(ContextFactory.SecondCollectionId);
            collectionsIds.Add(ContextFactory.FirstCollectionId);


            // Act


            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteCollectionsCommand
                    {
                        UserId = ContextFactory.UserBId,
                        AnimeCollectionsIds = collectionsIds,
                    },
                    CancellationToken.None));

            Assert.NotNull(await Context.AnimeCollections.SingleOrDefaultAsync(c => c.Id == ContextFactory.SecondCollectionId, CancellationToken.None));
            Assert.NotNull(await Context.AnimeCollections.SingleOrDefaultAsync(c => c.Id == ContextFactory.FirstCollectionId, CancellationToken.None));
        }
    }
}
