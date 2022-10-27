using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NoteRL : INoteRL
    {
        public readonly FundooContext context;
        private readonly IConfiguration Config;

        public NoteRL(FundooContext context, IConfiguration Config)
        {
            this.context = context;
            this.Config = Config;
        }


        public NoteEntity AddNote(NoteModel notes, long userid)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.Title = notes.Title;
                noteEntity.Note = notes.Note;
                noteEntity.Color = notes.Color;
                noteEntity.Image = notes.Image;
                noteEntity.IsArchive = notes.IsArchive;
                noteEntity.IsPin = notes.IsPin;
                noteEntity.IsTrash = notes.IsTrash;
                noteEntity.userid = userid;
                // noteEntity.Created = notes.Created;
                this.context.Note.Add(noteEntity);
                int result = this.context.SaveChanges();

                if (result > 0)
                {
                    return noteEntity;
                }
                return null;

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
                var deleteNote = context.Note.Where(x => x.NoteID == NoteId).FirstOrDefault();
                if (deleteNote != null)
                {
                    context.Note.Remove(deleteNote);
                    context.SaveChanges();
                    return deleteNote;
                }

                return null;


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
                var Note = context.Note.Where(x => x.userid == userId).FirstOrDefault();
                if (Note != null)
                {
                    return context.Note.Where(list => list.userid == userId).ToList();
                }

                return null;

            }
            catch (Exception)
            {
                throw;
            }
        }
        

        public List<NoteEntity> GetNote(long NoteId)
        {
            try
            {
                var Note = context.Note.Where(x => x.NoteID == NoteId).FirstOrDefault();

                if (Note != null)
                {
                    return context.Note.Where(list => list.NoteID == NoteId).ToList();
                }

                return null;

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
                var Note = context.Note.FirstOrDefault();

                if (Note != null)
                {
                    return context.Note.ToList();
                }

                return null;

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
                var update = context.Note.Where(x => x.NoteID == NoteId).FirstOrDefault();
                if (update != null)
                {
                    update.Title = noteModel.Title;
                    update.Note = noteModel.Note;
                    update.IsArchive = noteModel.IsArchive;
                    update.Color = noteModel.Color;
                    update.Image = noteModel.Image;
                    update.IsPin = noteModel.IsPin;
                    update.IsTrash = noteModel.IsTrash;
                    context.Note.Update(update);
                    context.SaveChanges();
                    return update;

                }


                return null;

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
                var result = context.Note.Where(r => r.userid == userId && r.NoteID == NoteID).FirstOrDefault();

                result.IsPin = !result.IsPin;
                context.SaveChanges();
                return result.IsPin;

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
                var result = context.Note.Where(r => r.userid == userId && r.NoteID == NoteID).FirstOrDefault();

                result.IsTrash = !result.IsTrash;
                context.SaveChanges();
                return result.IsTrash;

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
                var result = context.Note.Where(r => r.userid == userId && r.NoteID == NoteID).FirstOrDefault();
                result.IsArchive = !result.IsArchive;
                context.SaveChanges();
                return result.IsArchive;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public NoteEntity ColorNote(long NoteId, string color)
        {
            var result = context.Note.Where(r => r.NoteID == NoteId).FirstOrDefault();
            if (result != null)
            {
               
                    result.Color = color;
                    context.Note.Update(result);
                    context.SaveChanges();
                    return result;
               
            }
            else
            {
                return null;
            }
        }

       

    }
}
