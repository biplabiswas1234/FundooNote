using CommonLayer.Model;
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
    }
}
