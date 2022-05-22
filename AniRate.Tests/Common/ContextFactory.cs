using AniRate.Domain.Entities;
using AniRate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace AniRate.Tests.Common
{
    public class ContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();


        public static Guid FirstCollectionId;
        public static Guid SecondCollectionId;
        public static Guid ThirdCollectionId;
        public static Guid FourthCollectionId;

        public static Guid FirstAnimeId;
        public static Guid SecondAnimeId;
        public static Guid ThirdAnimeId;
        public static Guid FourthAnimeId;


        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var Anime1 = new AnimeTitle
            {
                Id = Guid.Parse("0F1FD77C-7A9D-4E74-86F0-B5597C95A262"),
                Name = "1 аниме",
                Russian = "1 аниме",
                Score = "10",
                Episodes = 64,
                AiredOn = "1",
                ReleasedOn = "1",
                Description = "1",
                DescriptionHtml = "1",
                Image = new Image()
                {
                    Id = Guid.Parse("3D03FBD1-766C-4C0E-ABCB-DC05D54CCF8E"),
                    AnimeId = Guid.Parse("0F1FD77C-7A9D-4E74-86F0-B5597C95A262"),
                    Original = "1",
                    Preview = "1",
                    X96 = "1",
                    X48 = "1",
                },
                Genres = new List<Genre>()
                    {
                        new Genre()
                        {
                            Id = Guid.Parse("592D9DC2-140A-439A-9D2B-600F74BD23EC"),
                            AnimeId = Guid.Parse("0F1FD77C-7A9D-4E74-86F0-B5597C95A262"),
                            Name = "1 жанр",
                            Russian = "1 жанр"
                        },

                        new Genre()
                        {
                            Id = Guid.Parse("AC5F1185-1D25-4D7C-B9BA-EBFE5DEE78A7"),
                            AnimeId = Guid.Parse("0F1FD77C-7A9D-4E74-86F0-B5597C95A262"),
                            Name = "2 жанр",
                            Russian = "2 жанр"
                        }
                    }
            };
            FirstAnimeId = Anime1.Id;

            var Anime2 = new AnimeTitle
            {
                Id = Guid.Parse("2499F50B-BE59-4F3C-9DD8-2A1C4F9CFF96"),
                Name = "2 аниме",
                Russian = "2 аниме",
                Score = "9",
                Episodes = 64,
                AiredOn = "2",
                ReleasedOn = "2",
                Description = "2",
                DescriptionHtml = "2",
                Image = new Image()
                {
                    Id = Guid.Parse("95EA153B-F6A5-4816-BE78-543C58CB59B0"),
                    AnimeId = Guid.Parse("2499F50B-BE59-4F3C-9DD8-2A1C4F9CFF96"),
                    Original = "2",
                    Preview = "2",
                    X96 = "2",
                    X48 = "2",
                },
                Genres = new List<Genre>()
                    {
                        new Genre()
                        {
                            Id = Guid.Parse("66477275-4952-4707-96D0-F107B2DF4CB9"),
                            AnimeId = Guid.Parse("2499F50B-BE59-4F3C-9DD8-2A1C4F9CFF96"),
                            Name = "2 жанр",
                            Russian = "2 жанр"
                        },

                        new Genre()
                        {
                            Id = Guid.Parse("E57AF38A-2A5B-4503-B939-AE6FA1F74460"),
                            AnimeId = Guid.Parse("2499F50B-BE59-4F3C-9DD8-2A1C4F9CFF96"),
                            Name = "3 жанр",
                            Russian = "3 жанр"
                        }
                    }
            };
            SecondAnimeId = Anime2.Id;

            var Anime3 = new AnimeTitle
            {
                Id = Guid.Parse("6D0DB8AC-46B6-4597-94E2-6FEC376D0470"),
                Name = "3 аниме",
                Russian = "3 аниме",
                Score = "8",
                Episodes = 64,
                AiredOn = "3",
                ReleasedOn = "3",
                Description = "3",
                DescriptionHtml = "3",
                Image = new Image()
                {
                    Id = Guid.Parse("25EA185B-F6A5-4816-BE78-543C58CB59C1"),
                    AnimeId = Guid.Parse("6D0DB8AC-46B6-4597-94E2-6FEC376D0470"),
                    Original = "3",
                    Preview = "3",
                    X96 = "3",
                    X48 = "3",
                },
                Genres = new List<Genre>()
                    {
                        new Genre()
                        {
                            Id = Guid.Parse("65477225-4952-D707-96D0-F917B3DF4CB9"),
                            AnimeId = Guid.Parse("6D0DB8AC-46B6-4597-94E2-6FEC376D0470"),
                            Name = "4 жанр",
                            Russian = "4 жанр"
                        },

                        new Genre()
                        {
                            Id = Guid.Parse("C68AF38A-2A5B-2903-B939-AE6FA1F74460"),
                            AnimeId = Guid.Parse("6D0DB8AC-46B6-4597-94E2-6FEC376D0470"),
                            Name = "5 жанр",
                            Russian = "5 жанр"
                        }
                    }
            };
            ThirdAnimeId = Anime3.Id;

            var Anime4 = new AnimeTitle
            {
                Id = Guid.Parse("C03A864C-E0D8-4AA7-9228-AC94E2222600"),
                Name = "4 аниме",
                Russian = "4 аниме",
                Score = "8",
                Episodes = 64,
                AiredOn = "4",
                ReleasedOn = "4",
                Description = "4",
                DescriptionHtml = "4",
                Image = new Image()
                {
                    Id = Guid.Parse("197DABB3-E469-4D60-A119-A78268B1031F"),
                    AnimeId = Guid.Parse("C03A864C-E0D8-4AA7-9228-AC94E2222600"),
                    Original = "4",
                    Preview = "4",
                    X96 = "4",
                    X48 = "4",
                },
                Genres = new List<Genre>()
                    {
                        new Genre()
        {
            Id = Guid.Parse("4928D5EA-35EE-40CC-8925-B7DB71E1AD29"),
                            AnimeId = Guid.Parse("C03A864C-E0D8-4AA7-9228-AC94E2222600"),
                            Name = "5 жанр",
                            Russian = "5 жанр"
                        },

                        new Genre()
        {
            Id = Guid.Parse("55EBC233-E87C-4FB4-8803-4D40B8C5EDC8"),
                            AnimeId = Guid.Parse("C03A864C-E0D8-4AA7-9228-AC94E2222600"),
                            Name = "6 жанр",
                            Russian = "6 жанр"
                        }
    }
            };
            FourthAnimeId = Anime4.Id;

            var collection1 = new AnimeCollection
            {
                Id = Guid.Parse("109B3614-47B8-41BD-92B6-62669EB2174B"),
                Name = "1 collection",
                UserComment = "1",
                UserId = UserAId,
            };
            collection1.AnimeTitles.Add(Anime1);
            collection1.AnimeTitles.Add(Anime2);
            collection1.AnimeTitles.Add(Anime3);
            collection1.AnimeTitles.Add(Anime4);
            FirstCollectionId = collection1.Id;


            var collection2 = new AnimeCollection
            {
                Id = Guid.Parse("90D5F70A-9CF1-46B7-98D3-9F4347F8971B"),
                Name = "2 collection",
                UserComment = "2",
                UserId = UserAId,
            };
            SecondCollectionId = collection2.Id;


            var collection3 = new AnimeCollection
            {
                Id = Guid.Parse("31430E0C-ACBB-416D-A2F2-3585E9A26B71"),
                Name = "3 collection",
                UserComment = "1",
                UserId = UserBId,
            };
            collection3.AnimeTitles.Add(Anime3);
            collection3.AnimeTitles.Add(Anime4);
            ThirdCollectionId = collection3.Id;


            var collection4 = new AnimeCollection
            {
                Id = Guid.Parse("33161C70-A14A-4F95-AD7A-69F0AA09AC41"),
                Name = "4 collection",
                UserComment = "2",
                UserId = UserBId,
            };
            collection4.AnimeTitles.Add(Anime4);
            FourthCollectionId = collection4.Id;


            context.AnimeCollections.AddRange(collection1, collection2, collection3, collection4);

            context.SaveChanges();

            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
