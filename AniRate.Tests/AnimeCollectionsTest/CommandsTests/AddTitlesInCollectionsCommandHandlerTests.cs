using AniRate.Application.AnimeCollections.Commands.AddTitlesInCollections;
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
    public class AddTitlesInCollectionsCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task AddTitlesInCollectionsCommandHandler_ManyTitlesToManyCollections_Success()
        {
            // Arrange
            var handler = new AddTitlesInCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();
            collectionsIds.Add(ContextFactory.ThirdCollectionId);
            collectionsIds.Add(ContextFactory.FourthCollectionId);

            var animesIds = new List<Guid>();
            animesIds.Add(ContextFactory.FirstAnimeId);
            animesIds.Add(ContextFactory.SecondAnimeId);
            animesIds.Add(ContextFactory.FourthAnimeId);


            // Act
            await handler.Handle(
                new AddTitlesInCollectionsCommand
                {
                    UserId = ContextFactory.UserBId,
                    AnimeTitlesIds = animesIds,
                    CollectionsIds = collectionsIds,
                },
                CancellationToken.None);


            //Assert
            foreach (var collectionId in collectionsIds)
            {
                foreach (var animeId in animesIds)
                {
                    Assert.NotNull(
                        await Context.AnimeCollections.SingleOrDefaultAsync(collection =>
                            collection.Id == collectionId &&
                            collection.AnimeTitles.Any(a => a.Id == animeId)));
                }
            }
        }


        [Fact]
        public async Task AddTitlesInCollectionsCommandHandler_ManyTitlesToOneCollection_Success()
        {
            // Arrange
            var handler = new AddTitlesInCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();
            collectionsIds.Add(ContextFactory.FourthCollectionId);

            var animesIds = new List<Guid>();
            animesIds.Add(ContextFactory.FirstAnimeId);
            animesIds.Add(ContextFactory.SecondAnimeId);
            animesIds.Add(ContextFactory.FourthAnimeId);

            // Act
            await handler.Handle(
                new AddTitlesInCollectionsCommand
                {
                    UserId = ContextFactory.UserBId,
                    AnimeTitlesIds = animesIds,
                    CollectionsIds = collectionsIds,
                },
                CancellationToken.None);


            //Assert
            foreach (var collectionId in collectionsIds)
            {
                foreach (var animeId in animesIds)
                {
                    Assert.NotNull(
                        await Context.AnimeCollections.SingleOrDefaultAsync(collection =>
                            collection.Id == collectionId &&
                            collection.AnimeTitles.Any(a => a.Id == animeId)));
                }
            }
        }


        [Fact]
        public async Task AddTitlesInCollectionsCommandHandler_OneTitleToManyCollections_Success()
        {
            // Arrange
            var handler = new AddTitlesInCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();
            collectionsIds.Add(ContextFactory.FourthCollectionId);
            collectionsIds.Add(ContextFactory.ThirdCollectionId);

            var animesIds = new List<Guid>();
            animesIds.Add(ContextFactory.FirstAnimeId);

            // Act
            await handler.Handle(
                new AddTitlesInCollectionsCommand
                {
                    UserId = ContextFactory.UserBId,
                    AnimeTitlesIds = animesIds,
                    CollectionsIds = collectionsIds,
                },
                CancellationToken.None);


            //Assert
            foreach (var collectionId in collectionsIds)
            {
                foreach (var animeId in animesIds)
                {
                    Assert.NotNull(
                        await Context.AnimeCollections.SingleOrDefaultAsync(collection =>
                            collection.Id == collectionId &&
                            collection.AnimeTitles.Any(a => a.Id == animeId)));
                }
            }
        }


        [Fact]
        public async Task AddTitlesInCollectionsCommandHandler_AddingTitlesToEmptyCollection_Succes()
        {
            // Arrange
            var handler = new AddTitlesInCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();
            collectionsIds.Add(ContextFactory.SecondCollectionId);

            var animesIds = new List<Guid>();
            animesIds.Add(ContextFactory.FirstAnimeId);
            animesIds.Add(ContextFactory.SecondAnimeId);


            // Act
            await handler.Handle(
                new AddTitlesInCollectionsCommand
                {
                    UserId = ContextFactory.UserAId,
                    AnimeTitlesIds = animesIds,
                    CollectionsIds = collectionsIds,
                },
                CancellationToken.None);


            //Assert
            var firstAnime = await Context.AnimeTitles.SingleOrDefaultAsync(a => a.Id == ContextFactory.FirstAnimeId);

            foreach (var collectionId in collectionsIds)
            {
                foreach (var animeId in animesIds)
                {
                    Assert.NotNull(
                        await Context.AnimeCollections.SingleOrDefaultAsync(collection =>
                            collection.Id == collectionId &&
                            collection.AnimeTitles.Any(a => a.Id == animeId) &&
                            collection.Image == firstAnime.Image));
                }
            }
        }


        [Fact]
        public async Task AddTitlesInCollectionsCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var handler = new AddTitlesInCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();
            collectionsIds.Add(ContextFactory.ThirdCollectionId);
            collectionsIds.Add(ContextFactory.SecondCollectionId);

            var animesIds = new List<Guid>();
            animesIds.Add(ContextFactory.FirstAnimeId);
            animesIds.Add(ContextFactory.SecondAnimeId);
            animesIds.Add(ContextFactory.FourthAnimeId);


            // Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                new AddTitlesInCollectionsCommand
                {
                    UserId = ContextFactory.UserAId,
                    AnimeTitlesIds = animesIds,
                    CollectionsIds = collectionsIds,
                },
                CancellationToken.None));
        }


        [Fact]
        public async Task AddTitlesInCollectionsCommandHandler_FailOnWrongCollectionId()
        {
            // Arrange
            var handler = new AddTitlesInCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();
            collectionsIds.Add(ContextFactory.ThirdCollectionId);
            collectionsIds.Add(Guid.NewGuid());

            var animesIds = new List<Guid>();
            animesIds.Add(ContextFactory.FirstAnimeId);
            animesIds.Add(ContextFactory.SecondAnimeId);
            animesIds.Add(ContextFactory.FourthAnimeId);


            // Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                new AddTitlesInCollectionsCommand
                {
                    UserId = ContextFactory.UserBId,
                    AnimeTitlesIds = animesIds,
                    CollectionsIds = collectionsIds,
                },
                CancellationToken.None));
        }


        [Fact]
        public async Task AddTitlesInCollectionsCommandHandler_FailOnWrongAnimeId()
        {
            // Arrange
            var handler = new AddTitlesInCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();
            collectionsIds.Add(ContextFactory.ThirdCollectionId);
            collectionsIds.Add(ContextFactory.FourthCollectionId);

            var animesIds = new List<Guid>();
            animesIds.Add(ContextFactory.FirstAnimeId);
            animesIds.Add(Guid.NewGuid());
            animesIds.Add(ContextFactory.FourthAnimeId);


            // Act


            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                new AddTitlesInCollectionsCommand
                {
                    UserId = ContextFactory.UserBId,
                    AnimeTitlesIds = animesIds,
                    CollectionsIds = collectionsIds,
                },
                CancellationToken.None));
        }


        [Fact]
        public async Task AddTitlesInCollectionsCommandHandler_FailOnEmptyCollectionsIds()
        {
            // Arrange
            var handler = new AddTitlesInCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();

            var animesIds = new List<Guid>();
            animesIds.Add(ContextFactory.FirstAnimeId);
            animesIds.Add(ContextFactory.FourthAnimeId);


            // Act


            //Assert
            await Assert.ThrowsAsync<EmptyStateException>(async () =>
            await handler.Handle(
                new AddTitlesInCollectionsCommand
                {
                    UserId = ContextFactory.UserBId,
                    AnimeTitlesIds = animesIds,
                    CollectionsIds = collectionsIds,
                },
                CancellationToken.None));
        }


        [Fact]
        public async Task AddTitlesInCollectionsCommandHandler_FailOnEmptyAnimesIds()
        {
            // Arrange
            var handler = new AddTitlesInCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();
            collectionsIds.Add(ContextFactory.ThirdCollectionId);
            collectionsIds.Add(ContextFactory.FourthCollectionId);

            var animesIds = new List<Guid>();


            // Act


            //Assert
            await Assert.ThrowsAsync<EmptyStateException>(async () =>
            await handler.Handle(
                new AddTitlesInCollectionsCommand
                {
                    UserId = ContextFactory.UserBId,
                    AnimeTitlesIds = animesIds,
                    CollectionsIds = collectionsIds,
                },
                CancellationToken.None));
        }

        public async Task AddTitlesInCollectionsCommandHandler_NoOneAdded_FailOnWrongCollectionId()
        {
            // Arrange
            var handler = new AddTitlesInCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();
            collectionsIds.Add(ContextFactory.ThirdCollectionId);
            collectionsIds.Add(Guid.NewGuid());
            collectionsIds.Add(ContextFactory.FourthCollectionId);


            var animesIds = new List<Guid>();
            animesIds.Add(ContextFactory.FirstAnimeId);
            animesIds.Add(ContextFactory.SecondAnimeId);


            // Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                new AddTitlesInCollectionsCommand
                {
                    UserId = ContextFactory.UserBId,
                    AnimeTitlesIds = animesIds,
                    CollectionsIds = collectionsIds,
                },
                CancellationToken.None));

            //Assert
            foreach (var collectionId in collectionsIds)
            {
                foreach (var animeId in animesIds)
                {
                    Assert.Null(
                        await Context.AnimeCollections.SingleOrDefaultAsync(collection =>
                            collection.Id == collectionId &&
                            collection.AnimeTitles.Any(a => a.Id == animeId)));
                }
            }
        }


        [Fact]
        public async Task AddTitlesInCollectionsCommandHandler_NoOneAdded_FailOnWrongAnimeId()
        {
            // Arrange
            var handler = new AddTitlesInCollectionsCommandHandler(Context);

            var collectionsIds = new List<Guid>();
            collectionsIds.Add(ContextFactory.ThirdCollectionId);
            collectionsIds.Add(ContextFactory.FourthCollectionId);


            var animesIds = new List<Guid>();
            animesIds.Add(ContextFactory.FirstAnimeId);
            animesIds.Add(Guid.NewGuid());
            animesIds.Add(ContextFactory.SecondAnimeId);


            // Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                new AddTitlesInCollectionsCommand
                {
                    UserId = ContextFactory.UserBId,
                    AnimeTitlesIds = animesIds,
                    CollectionsIds = collectionsIds,
                },
                CancellationToken.None));

            //Assert
            foreach (var collectionId in collectionsIds)
            {
                foreach (var animeId in animesIds)
                {
                    Assert.Null(
                        await Context.AnimeCollections.SingleOrDefaultAsync(collection =>
                            collection.Id == collectionId &&
                            collection.AnimeTitles.Any(a => a.Id == animeId)));
                }
            }
        }
    }
}
