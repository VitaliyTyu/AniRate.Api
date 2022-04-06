using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Commands.UpdateCollectionDetails
{
    public class UpdateCollectionDetailsCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Comment { get; set; }
        public double? AverageRating { get; set; }
    }
}
