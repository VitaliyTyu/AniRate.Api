using AniRate.Application.AnimeCollections.Commands.UpdateCollectionDetails;
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
    public class UpdateCollectionDetailsCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateCollectionDetailsCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateCollectionDetailsCommandHandler(Context);


            // Act
            await handler.Handle(
                new UpdateCollectionDetailsCommand
                {
                    Id = ContextFactory.FirstCollectionId,
                    Name = "changed collection 1",
                    UserId = ContextFactory.UserAId,
                },
                CancellationToken.None);


            //Assert
                Assert.NotNull(
                    await Context.AnimeCollections.SingleOrDefaultAsync(collection =>
                        collection.Id == ContextFactory.FirstCollectionId &&
                        collection.Name == "changed collection 1"));
        }

        [Fact]
        public async Task UpdateCollectionDetailsCommandHandler_FailOnWrongCollectionId()
        {
            // Arrange
            var handler = new UpdateCollectionDetailsCommandHandler(Context);


            // Act


            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateCollectionDetailsCommand
                    {
                        Id = Guid.NewGuid(),
                        Name = "changed collection 1",
                        UserId = ContextFactory.UserAId,
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdateCollectionDetailsCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var handler = new UpdateCollectionDetailsCommandHandler(Context);


            // Act


            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateCollectionDetailsCommand
                    {
                        Id = ContextFactory.FirstCollectionId,
                        Name = "changed collection 1",
                        UserId = ContextFactory.UserBId,
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdateCollectionDetailsCommandHandler_FailOnEmptyName()
        {
            // Arrange
            var handler = new UpdateCollectionDetailsCommandHandler(Context);


            // Act


            //Assert
            await Assert.ThrowsAsync<EmptyStateException>(async () =>
                await handler.Handle(
                    new UpdateCollectionDetailsCommand
                    {
                        Id = ContextFactory.FirstCollectionId,
                        Name = "",
                        UserId = ContextFactory.UserAId,
                    },
                    CancellationToken.None));
        }
    }
}
