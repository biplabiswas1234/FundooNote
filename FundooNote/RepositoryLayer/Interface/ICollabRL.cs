﻿using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollabRL
    {
        public CollabEntity AddCollab(CollabModel collabModel);
        public string RemoveCollab(long collabID, long userId);
        public IEnumerable<CollabEntity> GetCollab(long noteId, long userId);
    }
}
