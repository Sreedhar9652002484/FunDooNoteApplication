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
    }
}