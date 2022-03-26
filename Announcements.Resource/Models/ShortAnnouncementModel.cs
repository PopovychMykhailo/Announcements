using Announcements.Resource.Domain.Entities.Implementations;
using System;

namespace Announcements.Resource.Models
{
    public class ShortAnnouncementModel
    {
        public Guid Id { get; init; }
        public string Title { get; init; }



        public static implicit operator ShortAnnouncementModel(AnnouncementEntity model)
        {
            if (model == null)
                return null;

            return new ShortAnnouncementModel()
            {
                Id = model.Id,
                Title = model.Title
            };
        }
    }
}
