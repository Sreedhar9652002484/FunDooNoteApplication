﻿
using CommonLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static RepoLayer.Services.NotesRepo;

namespace BusinessLayer.Interface
{
    public interface INotesBusiness
    {
        public NotesEntity NoteTaking(NoteTakingModel model, long userId);
        public List<NotesEntity> GetAllNotes(long userId);

        public List<NotesEntity> GetNotesById(long NotesId);
        public string UpdateNote(long NotesId, string Notes, long userId);

        public bool DeleteNote(long NotesId, long userId);
        public string Colour(long NotesId,  long userId, string colour);
        public Task<Tuple<int, string>> AddImage(long NotesId, long userId, IFormFile imageFile);

        public bool Archive(long NotesId, long userId);

        public bool Pin(long NotesId, long userId);
        public bool MoveToTrash(long NotesId, long userId);
    }
}
