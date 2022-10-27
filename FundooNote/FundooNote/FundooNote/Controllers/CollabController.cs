using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using System;
using System.Linq;

namespace FundooNote.Controllers
{
    
        [Route("api/[controller]")]
        [ApiController]
        public class CollabController : ControllerBase
        {
            private readonly ICollabBL collabBL;
            private readonly FundooContext fundooContext;
            public CollabController(ICollabBL collabBL, FundooContext fundooContext)
            {
                this.collabBL = collabBL;
                this.fundooContext = fundooContext;
            }

            [HttpPost]
            [Route("Create")]
            public IActionResult AddCollab(CollabModel collabModel)
            {
                try
                {
                return null;
                   
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    
}
