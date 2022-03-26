using Announcements.Resource.Domain.Entities.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcements.Resource.Models
{
    public class AnnouncementModel
    {
        [Required]
        public Guid Id { get; init; }

        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; }



        public static explicit operator AnnouncementEntity(AnnouncementModel model)
        {
            if (model == null)
                return null;

            return new AnnouncementEntity()
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                DateAdded = model.DateAdded
            };
        }

        public static implicit operator AnnouncementModel(AnnouncementEntity entity)
        {
            if (entity == null)
                return null;

            return new AnnouncementModel()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                DateAdded = entity.DateAdded
            };
        }
    }
}
