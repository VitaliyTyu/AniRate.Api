using AniRate.Application.Common.Models;
using MediatR;


namespace AniRate.Application.AnimeCollections.Queries.SearchCollections
{
    public class SearchCollectionsQuery : IRequest<PaginatedList<BriefCollectionVM>>
    {
        public Guid UserId { get; set; }
        public string SearchString { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}