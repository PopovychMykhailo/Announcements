using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Announcements.Resource.Domain.Repositories.Interfaces;
using Announcements.Resource.Domain.Repositories.Implementations;
using Announcements.Resource.Domain.Entities.Implementations;
using Announcements.Resource.Domain.Exceptions;
using Announcements.Resource.Models;
using Announcements.Resource.Models.Requests;
using Announcements.Resource.Services.Interfaces;
using Announcements.Resource.Services.Implementations;

namespace Announcements.Resource.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementsController : ControllerBase
    {
        protected AnnouncementsRepository Announcements;
        protected AnnouncementsAnalyzer Analyzer;


        public AnnouncementsController(IAnnouncementsRepository announcements, IAnnouncementsAnalyzer analyzer)
        {
            Announcements = (AnnouncementsRepository)announcements;
            Analyzer = (AnnouncementsAnalyzer)analyzer;
        }


        #region CRUD announcements

        // Create
        [HttpPost(nameof(Create))]
        public IActionResult Create([FromBody] CreateAnnouncementRequest request)
        {
            var entity = Announcements.Add(request.Title, request.Description);
            return Ok(entity);
        }


        // Read
        [HttpGet(nameof(GetDetails) + "/{id}")]
        public IActionResult GetDetails(Guid id)
        {
            return Ok((AnnouncementModel) Announcements.Get(id));
        }

        [HttpGet(nameof(GetMany))]
        public IActionResult GetMany([FromBody] GetAllRequest options)
        {
            if (options == null)        options = new GetAllRequest();  // Defaults: WithDetails = false, MaxSize = 0
            if (options.MaxSize == 0)   options.MaxSize = Announcements.Count;


            if (options.WithDetails == true)
                return Ok(Announcements.GetAll().Select(a => (AnnouncementModel)a).Take(options.MaxSize));
            else 
                return Ok(Announcements.GetAll().Select(a => (ShortAnnouncementModel)a).Take(options.MaxSize));
        }


        // Update
        [HttpPut(nameof(Update))]
        public IActionResult Update([FromBody] AnnouncementModel model)
        {
            if (model == null)
                return BadRequest("AnnouncementModel is null!");

            try
            {
                return Ok(Announcements.Update((AnnouncementEntity)model));
            }
            catch (ArgumentNullException)
            {
                return BadRequest("AnnouncementModel is null!");
            }
            catch (NotFoundException)
            {
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // HttpStatusCode.InternalServerError
            }
        }


        // Delete
        [HttpDelete(nameof(Delete) + "/{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                Announcements.Delete(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // HttpStatusCode.InternalServerError
            }
        }

        #endregion


        #region Analize announcements

        [HttpGet(nameof(GetSimilar))]
        public IActionResult GetSimilar([FromBody] GetSimilarRequest request)
        {
            if (request == null)
                return BadRequest("Request is empty!");

            return Ok(Analyzer.GetSimilar(request.PrimaryId, request.MaxSimilarAnnouncements));
        }

        #endregion
    }
}
