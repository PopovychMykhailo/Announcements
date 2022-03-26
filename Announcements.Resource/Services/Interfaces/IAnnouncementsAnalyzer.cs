using Announcements.Resource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcements.Resource.Services.Interfaces
{
    public interface IAnnouncementsAnalyzer
    {
        public IEnumerable<ShortAnnouncementModel> GetSimilar(Guid primaryId, int maxSimilarItems);
    }
}
