
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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
        private readonly Cloudinary _cloudinary;
        private readonly FileService _fileService;

        public NotesRepo(FunDoContext funDoContext, IConfiguration configuration, Cloudinary cloudinary, FileService fileService)
        {
            this.funDoContext = funDoContext;
            this.configuration = configuration;
            this._cloudinary = cloudinary;
            this._fileService = fileService;

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
        public string Colour(long NotesId, string Colour, long userId)
        {
            try
            {
                var note = funDoContext.Note.FirstOrDefault(data => data.NoteId == NotesId && data.UserId == userId);
                if (note != null)
                {
                    Colour = note.Colour;
                    funDoContext.Note.Update(note);
                    funDoContext.SaveChanges();
                    return Colour;
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Tuple<int, string>> AddImage(long NotesId, long userId, IFormFile image)
        {
            try
            {
                var result = funDoContext.Note.FirstOrDefault(u => u.NoteId == NotesId);
                if (result != null)
                {
                    var fileServiceResult = await _fileService.SaveImage(image);
                    if (fileServiceResult.Item1 == 0)
                    {
                        return new Tuple<int, string>(0, fileServiceResult.Item2);
                    }
                    var uploadImage = new ImageUploadParams
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream()),
                    };
                    ImageUploadResult uploadResult = await _cloudinary.UploadAsync(uploadImage);
                    string imgUrl = uploadResult.SecureUrl.AbsoluteUri;
                    result.Image = imgUrl;
                    funDoContext.Note.Update(result);
                    funDoContext.SaveChanges();

                    return new Tuple<int, string>(1, "Image Uploaded Succesfully");

                }
                return null;
            }
            catch (Exception ex)
            {

                return new Tuple<int, string>(0, "An error occurred: " + ex.Message);
            }

        }
        public bool Archive(long NotesId, long userId)

        {
            try
            {
                var notes = funDoContext.Note.FirstOrDefault(u => u.NoteId == NotesId && u.UserId == userId);
                if (notes != null)
                {
                    notes.isArchive = true;

                    funDoContext.Note.Update(notes);
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
        public bool Pin(long NotesId, long userId)
        {
            try
            {
                var notes = funDoContext.Note.FirstOrDefault(u => u.NoteId == NotesId && u.UserId == userId);
                if (notes != null)
                {
                    notes.isPin = true;
                    funDoContext.Note.Update(notes);
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
        public bool MoveToTrash(long NotesId, long userId)
        {
            try
            {
                var notes = funDoContext.Note.FirstOrDefault(u => u.NoteId == NotesId && u.UserId == userId);
                if (notes != null)
                {
                    notes.isTrash = true;
                    funDoContext.Note.Update(notes);
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
