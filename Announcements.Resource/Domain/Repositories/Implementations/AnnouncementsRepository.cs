using Announcements.Resource.Database;
using Announcements.Resource.Domain.Entities.Implementations;
using Announcements.Resource.Domain.Exceptions;
using Announcements.Resource.Domain.Repositories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcements.Resource.Domain.Repositories.Implementations
{
    public class AnnouncementsRepository : IAnnouncementsRepository, IEnumerable<AnnouncementEntity>
    {
        protected AppDbContext Context { get; set; }
        public int Count { get => Context.Announcements.Count(); }


        public AnnouncementsRepository(AppDbContext context)
        {
            Context = context;
        }


        #region CRUD for AnnouncementEntity
        
        /// <summary>
        /// Add new AnnouncementEntity in database
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <returns>AnnouncementEntity</returns>
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

        /// <summary>
        /// Add new AnnouncementEntity in database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>AnnouncementEntity</returns>
        /// <exception cref="ArgumentNullException">If entity is null</exception>
        public AnnouncementEntity Add(AnnouncementEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Context.Announcements.Add(entity);
            Context.SaveChanges();

            return Context.Announcements.FirstOrDefault(x => x.Id == entity.Id);
        }

        /// <summary>
        /// Get AnnouncementEntity from database by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AnnouncementEntity, can be null</returns>
        public AnnouncementEntity Get(Guid id)
        {
            var toGet = Context.Announcements.FirstOrDefault(x => x.Id == id);
            return toGet;
        }

        /// <summary>
        /// Get all AnnouncementEntity from database
        /// </summary>
        /// <returns>IEnumerable<AnnouncementEntity></returns>
        public IEnumerable<AnnouncementEntity> GetAll()
        {
            var array = Context.Announcements.ToArray();
            return array;
        }

        /// <summary>
        /// Update AnnouncementEntity in the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Updated AnnouncementEntity</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotFoundException"></exception>
        public AnnouncementEntity Update(AnnouncementEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            var toUpdate = Context.Announcements.FirstOrDefault(x => x.Id == entity.Id);
            if (toUpdate == null)
                throw new NotFoundException($"The {nameof(AnnouncementEntity.Id)} doesn't exist in database!");

            toUpdate.ApplyChanges(entity);

            Context.Announcements.Update(toUpdate);
            Context.SaveChanges();

            return Context.Announcements.FirstOrDefault(x => x.Id == entity.Id);
        }

        /// <summary>
        /// Delete AnnouncementEntity in database
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotFoundException"></exception>
        public void Delete(Guid id)
        {
            var toDelete = Context.Announcements.FirstOrDefault(x => x.Id == id);
            if (toDelete == null)
                throw new NotFoundException($"The {nameof(AnnouncementEntity.Id)} doesn't exist in database!");

            Context.Announcements.Remove(toDelete);
            Context.SaveChanges();
        }

        #endregion

        #region IEnumerable
        public IEnumerator<AnnouncementEntity> GetEnumerator()
        {
            return Context.Announcements.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
