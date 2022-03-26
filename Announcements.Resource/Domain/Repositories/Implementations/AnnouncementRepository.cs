using Announcements.Resource.Database;
using Announcements.Resource.Domain.Entities.Implementations;
using Announcements.Resource.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcements.Resource.Domain.Repositories.Implementations
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        public AppDbContext Context { get; set; }



        public AnnouncementRepository(AppDbContext context)
        {
            Context = context;
        }


        public AnnouncementEntity Add(string title, string description)
        {
            var toAdd = new AnnouncementEntity()
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                DateAdded = DateTime.UtcNow
            };

            Context.Announcements.Add(toAdd);
            Context.SaveChanges();

            // Make sure it's added
            return Context.Announcements.FirstOrDefault(x => x.Id == toAdd.Id);
        }

        public AnnouncementEntity Add(AnnouncementEntity entity)
        {
            throw new NotImplementedException();
        }

        public AnnouncementEntity Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AnnouncementEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public AnnouncementEntity Update(AnnouncementEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
