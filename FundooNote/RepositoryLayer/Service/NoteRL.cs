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
    }
}
