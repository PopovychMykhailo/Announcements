using Microsoft.AspNetCore.Mvc;
using Announcements.Resource.Domain.Repositories.Interfaces;
using Announcements.Resource.Models;

namespace Announcements.Resource.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementsController : ControllerBase
    {
        IAnnouncementRepository Announcements;



        public AnnouncementsController(IAnnouncementRepository announcements)
        {
            Announcements = announcements;
        }

        
        [HttpPost(nameof(Create))]
        public IActionResult Create(CreateAnnouncementModel model)
        {
            var entity = Announcements.Add(model.Title, model.Description);
            return Ok(entity);
        }
    }
}
