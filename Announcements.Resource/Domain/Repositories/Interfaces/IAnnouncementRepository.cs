using Announcements.Resource.Database;
using Announcements.Resource.Domain.Entities.Implementations;
using System;
using System.Collections.Generic;

namespace Announcements.Resource.Domain.Repositories.Interfaces
{
    public interface IAnnouncementRepository
    {
        public AppDbContext Context { get; set; }


        // Create
        public AnnouncementEntity Add(string title, string description);
        public AnnouncementEntity Add(AnnouncementEntity entity);
        
        // Read
        public AnnouncementEntity Get(Guid id);
        public IEnumerable<AnnouncementEntity> GetAll();

        // Update
        public AnnouncementEntity Update(AnnouncementEntity entity);

        // Delete
        public bool Delete(Guid id);
    }
}
