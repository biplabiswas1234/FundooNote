using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using System;
using System.Linq;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        private readonly FundooContext fundooContext;

        public LabelController(ILabelBL labelBL, FundooContext fundooContext)
        {
            this.labelBL = labelBL;
            this.fundooContext = fundooContext;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult CreateLabel(LabelModel labelModel)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var labelNote = fundooContext.Note.Where(r => r.NoteID == labelModel.NoteID).FirstOrDefault();
                if (labelNote.userid == userid)
                {
                    var result = labelBL.CreateLabel(labelModel);
                    if (result != null)
                    {
                        return Ok(new { Success = true, Message = "Label created successfully", data = result });
                    }
                    else
                    {
                        return BadRequest(new { Success = false, Message = "Label not created" });
                    }
                }
                else
                {
                    return Unauthorized(new { Success = false, Message = "Unauthorized User!" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Rename")]
        public IActionResult UpdateLabel(long LabelId, string Labelname)
        {
            try
            {
                // long Labelid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "LabelId").Value);
                var result = labelBL.UpdateLabel(LabelId, Labelname);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Updated", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed" });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete]
        [Route("Remove")]
        public IActionResult DeleteLabel(long labelID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var delete = labelBL.DeleteLabel(labelID, userId);
                if (delete != null)
                {
                    return this.Ok(new { Success = true, message = "Label Deleted Successfully" });
                }
                else
                {
                    return this.NotFound(new { Success = false, message = "Label not Deleted" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("Retrieve")]
        public IActionResult GetAllLabels()
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var labels = labelBL.GetLabels(userid);
                if (labels != null)
                {
                    return this.Ok(new { Success = true, Message = " All labels found Successfully", data = labels });
                }
                else
                {
                    return this.NotFound(new { Success = false, Message = "No label found" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
