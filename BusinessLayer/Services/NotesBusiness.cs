using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entity;
using RepoLayer.Interface;
using RepoLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NotesBusiness : INotesBusiness
    {
        private readonly INotesRepo _notesRepo;


        public NotesBusiness(INotesRepo notesRepo)
        {
            _notesRepo = notesRepo;
        }



        public NotesEntity NoteTaking(NoteTakingModel model, long userId)
        {
            try
            {
                return _notesRepo.NoteTaking(model, userId);
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
                return _notesRepo.GetAllNotes(userId);
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
                return _notesRepo.GetNotesById(NotesId);
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
                return _notesRepo.UpdateNote(NotesId, Notes, userId);
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
                return _notesRepo.DeleteNote(NotesId, userId);
            }
            catch (Exception)
            {

                throw;
            }

        }
       
    }
}
