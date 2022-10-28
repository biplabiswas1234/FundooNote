using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity CreateLabel(LabelModel labelModel);
        public LabelEntity UpdateLabel(long LabelId, string Labelname);
        public LabelEntity DeleteLabel(long labelID, long userId);
        public IEnumerable<LabelEntity> GetLabels(long userId);
    }
}
