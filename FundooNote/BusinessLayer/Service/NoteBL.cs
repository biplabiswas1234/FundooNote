using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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
        public List<NoteEntity> GetNotebyUserId(long userId)
        {
            try
            {
                return noteRL.GetNotebyUserId(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<NoteEntity> GetAllNote()
        {
            try
            {
                return noteRL.GetAllNote();
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

        public bool Pinned(long NoteID, long userId)
        {
            try
            {
                return noteRL.Pinned(NoteID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Trashed(long NoteID, long userId)
        {
            try
            {
                return noteRL.Trashed(NoteID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Archieved(long NoteID, long userId)
        {
            try
            {
                return noteRL.Archieved(NoteID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NoteEntity ColorNote(long NoteId, string color)
        {
            try
            {
                return noteRL.ColorNote(NoteId, color);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Imaged(long NoteID, long userId, IFormFile image)
        {
            try
            {
                return noteRL.Imaged(NoteID, userId, image);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
