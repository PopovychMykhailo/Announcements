using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcements.Resource.Models.Requests
{
    public class GetSimilarRequest
    {
        public Guid PrimaryId { get; init; }
        public int MaxSimilarAnnouncements { get; init; } = 3;
    }
}
