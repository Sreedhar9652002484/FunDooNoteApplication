
using CommonLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static RepoLayer.Services.NotesRepo;

namespace RepoLayer.Interface
{
    public interface INotesRepo
    {
        public NotesEntity NoteTaking(NoteTakingModel model, long userId);
        public List<NotesEntity> GetAllNotes(long userId);

        public List<NotesEntity> GetNotesById(long NotesId);
        public string UpdateNote(long NotesId, string Notes, long userId);

        public bool DeleteNote(long NotesId, long userId);
        public string Colour(long NotesId, string colour, long userId);
        public  Task<Tuple<int, string>> AddImage(long NotesId, long userId, IFormFile image);

        public bool Archive(long NotesId, long userId);

        public bool Pin(long NotesId, long userId);
        public bool MoveToTrash(long NotesId, long userId);
    }
}
