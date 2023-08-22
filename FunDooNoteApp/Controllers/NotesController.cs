using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Context;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.CompilerServices;

namespace FunDooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : Controller
    {
        private readonly INotesBusiness notesBusiness;

        public NotesController(INotesBusiness notesBusiness)
        {
            this.notesBusiness = notesBusiness;
        }

        [Authorize]
        [Route("CreatingNote")]
        [HttpPost]
        public IActionResult Notes(NoteTakingModel model)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            if (userId != null)
            {
                var result = notesBusiness.NoteTaking(model, userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "User Notes Created Successfully", Data = result });
                }
                else
                {
                    return this.NotFound(new { success = false, Message = "Notes Not Created", Data = result });

                }
            }
            return null;

        }

        [Authorize]
        [Route("GetAllNotes")]
        [HttpGet]
        public IActionResult GetAllNotes()
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            if (userId != null)
            {

                var result = notesBusiness.GetAllNotes(userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Get All Notes Successful", Data = result });
                }
                else
                {
                    return this.NotFound(new { success = false, Message = "UnSuccessful", Data = result });

                }
            }
            return null;

        }

        [Authorize]
        [Route("GetNotesById")]
        [HttpGet]
        public IActionResult GetNotesById(long NotesId)
        {
            var result = notesBusiness.GetNotesById(NotesId);
            if (result != null)
            {
                return this.Ok(new { success = true, Message = "Get Notes By ID Successful", Data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, Message = "UnSuccessful", Data = result });

            }
        }
        [Authorize]
        [Route("UpdateNotes")]
        [HttpPatch]
        public IActionResult UpdateNotes(long NotesId, string Notes)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            if (userId != null)
            {
                var result = notesBusiness.UpdateNote(NotesId, Notes, userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Updated Notes Successful" });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "UnSuccessful", Data = result });

                }
            }
            return null;
        }
        [Authorize]
        [Route("DeleteNote")]
        [HttpDelete]
        public IActionResult DeleteNote(long NotesId)

        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            if (userId != null)
            {
                var result = notesBusiness.DeleteNote(NotesId, userId);
                if (result == true)
                {
                    return this.Ok(new { success = true, Message = "Deleted Notes Successful" });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "UnSuccessful", Data = result });

                }
            }
            return null;
        }
        [Authorize]
        [Route("SetColour")]
        [HttpPatch]
        public IActionResult AddColour(long NotesId, string Colour)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            if (userId != null)
            {
                var result = notesBusiness.Colour(NotesId, userId, Colour);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Colour Set Successful" });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "UnSuccessful", Data = result });

                }
            }
            return null;
        }
        [Authorize]
        // [Route("AddImage")]
        [HttpPost("AddImage")]
        public async Task<IActionResult> AddImage(long notedId, string item, IFormFile imageFile)
        {
            try
            {
                long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                if (userId != null)
                {
                    // Call the business layer to handle the image upload
                    var result = await notesBusiness.AddImage(notedId, userId,imageFile);

                    if (result.Item1 == 1)
                    {
                        return Ok(new { success = true, message = "Image Uploaded successfully" });
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = result.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred: " + ex.Message });
            }
            return null;
        }

        [Authorize]
        [Route("Archive")]
        [HttpPost]
        public IActionResult Archive(long NotesId)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            if (userId != null)
            {
                var result = notesBusiness.Archive(NotesId, userId);
                if (result == true)
                {
                    return this.Ok(new { success = true, Message = "Archive Notes Successful" });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "UnSuccessful", Data = result });

                }
            }
            return null;
        }
        [Authorize]
        [Route("Pin")]
        [HttpPost]
        public IActionResult Pin(long NotesId)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            if (userId != null)
            {
                var result = notesBusiness.Pin(NotesId, userId);
                if (result == true)
                {
                    return this.Ok(new { success = true, Message = "Pin Notes Successful" });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "UnSuccessful", Data = result });

                }
            }
            return null;
        }
        [Authorize]
        [Route("Trash")]
        [HttpPost]
        public IActionResult MoveToTash(long NotesId)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            if (userId != null)
            {
                var result = notesBusiness.MoveToTrash(NotesId, userId);
                if (result == true)
                {
                    return this.Ok(new { success = true, Message = "Moved To Trash Successful" });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "UnSuccessful", Data = result });

                }
            }
            return null;
        }
    }
}