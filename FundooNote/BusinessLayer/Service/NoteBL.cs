using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NoteBL:INoteBL
    {
        INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }

        public NoteEntity AddNote(NoteModel notes, long userid)
        {
            try
            {


                return this.noteRL.AddNote(notes,userid);
            }
            catch (Exception)
            {

                throw;
            }
        }

    
        
        public NoteEntity DeleteNote(long NoteId)
        {
            try
            {
                return noteRL.DeleteNote(NoteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NoteEntity UpdateNote(NoteModel noteModel, long NoteId)
        {
            try
            {
                return noteRL.UpdateNote(noteModel, NoteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<NoteEntity> GetNote(long NotesId)
        {
            try
            {
                return noteRL.GetNote(NotesId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
