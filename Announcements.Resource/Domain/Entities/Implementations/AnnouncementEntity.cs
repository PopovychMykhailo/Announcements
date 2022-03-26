using Announcements.Resource.Domain.Entities.Interfaces;
using Announcements.Resource.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcements.Resource.Domain.Entities.Implementations
{
    public class AnnouncementEntity : IEntity
    {
        private DateTime _dateAdded;

        public Guid Id { get; init; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded
        {
            get => _dateAdded;

            init
            {
                if (value > DateTime.UtcNow)
                    throw new WrongDateException("DateAdded cann't be in the future!");

                _dateAdded = value;
            }
        }



        public AnnouncementEntity()
        {
            Id = Guid.NewGuid();
            Title = "(empty)";
            Description = "(description)";
            DateAdded = DateTime.UtcNow;
        }
        public AnnouncementEntity(string title, string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            DateAdded = DateTime.UtcNow;
        }


        public AnnouncementEntity ApplyChanges(AnnouncementEntity source)
        {
            Title = source.Title;
            Description = source.Description;

            return this;
        }
    }
}
