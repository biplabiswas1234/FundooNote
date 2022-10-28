using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        public NoteEntity AddNote(NoteModel notes, long userid);
        public NoteEntity DeleteNote(long NoteId);
        public NoteEntity UpdateNote(NoteModel noteModel, long NoteId);
        public List<NoteEntity> GetNote(long NoteId);
        public List<NoteEntity> GetNotebyUserId(long userId);
        public bool Pinned(long NoteID, long userId);
        public bool Trashed(long NoteID, long userId);
        public bool Archieved(long NoteID, long userId);
        public NoteEntity ColorNote(long NoteId, string color);
        public List<NoteEntity> GetAllNote();
        public string Imaged(long NoteID, long userId, IFormFile image);
    }
}
