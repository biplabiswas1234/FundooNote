using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        public readonly FundooContext fundooContext;
        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public LabelEntity CreateLabel(LabelModel labelModel)
        {
            try
            {
                var result = fundooContext.Note.Where(r => r.NoteID == labelModel.NoteID).FirstOrDefault();
                if (result != null)
                {
                    LabelEntity label = new LabelEntity();
                    label.LabelName = labelModel.LabelName;
                    label.NoteID = result.NoteID;
                    label.UserId = result.userid;

                    fundooContext.LabelTable.Add(label);
                    fundooContext.SaveChanges();
                    return label;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public LabelEntity UpdateLabel(long LabelId, string Labelname)
        {
            try
            {
                var result = fundooContext.LabelTable.Where(x => x.LabelID == LabelId).FirstOrDefault();
                if (result != null)
                {
                    result.LabelName = Labelname;
                    fundooContext.SaveChanges();
                    return result;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public LabelEntity DeleteLabel(long labelID, long userId)
        {
            try
            {
                var deleteLabel = fundooContext.LabelTable.Where(r => r.LabelID == labelID).FirstOrDefault();
                if (deleteLabel != null)
                {
                    fundooContext.LabelTable.Remove(deleteLabel);
                    fundooContext.SaveChanges();
                    return deleteLabel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<LabelEntity> GetLabels(long userId)
        {
            try
            {
                var result = fundooContext.LabelTable.ToList().Where(x => x.UserId == userId);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
    

