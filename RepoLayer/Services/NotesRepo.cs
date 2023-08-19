
using CommonLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Services
{
    public class NotesRepo : INotesRepo
    {

        private readonly FunDoContext funDoContext;
        private readonly IConfiguration configuration;
        
        public NotesRepo(FunDoContext funDoContext, IConfiguration configuration)
        {
            this.funDoContext = funDoContext;
            this.configuration = configuration;
           
        }


        public NotesEntity NoteTaking(NoteTakingModel model, long userId)
        {

            try
            {
               

                NotesEntity notesEntity = new NotesEntity();
                notesEntity.Title = model.Title;
                notesEntity.TakeNote = model.TakeaNote;
                notesEntity.UserId = userId;
               
                funDoContext.Note.Add(notesEntity);
                funDoContext.SaveChanges();

                if (notesEntity != null)
                {
                    return notesEntity;
                }
                return null;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
      

        public List<NotesEntity> GetAllNotes(long userId)
        {
            try
            {
                return funDoContext.Note.Where(data => data.UserId == userId).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<NotesEntity> GetNotesById(long NotesId)
        {
            try
            {

                var notesById = funDoContext.Note.FirstOrDefault(u => u.NoteId == NotesId);
                if (notesById != null)
                {
                    List<NotesEntity> newList = new List<NotesEntity>();
                    newList.Add(notesById);
                    return newList;
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public string UpdateNote(long NotesId, string Notes, long userId)
        {
            try
            {
                var userNotes = funDoContext.Note.FirstOrDefault(data => data.NoteId == NotesId && data.UserId == userId);
                //var userIdNote = funDoContext.User.FirstOrDefault(data => data.UserId == userId);
                if (userNotes != null)
                {


                    userNotes.TakeNote = userNotes.TakeNote + " " + Notes;
                    funDoContext.Note.Update(userNotes);
                    funDoContext.SaveChanges();

                    return userNotes.TakeNote;

                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteNote(long NotesId, long userId)
        {
            try
            {
                var note = funDoContext.Note.FirstOrDefault(data => data.NoteId == NotesId && data.UserId == userId);
                if (note != null)
                {

                    funDoContext.Note.Remove(note);
                    funDoContext.SaveChanges();
                    return true;

                }
                return false;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
