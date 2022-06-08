using Microsoft.AspNetCore.Mvc;
using backend.Models.DB;
using backend.Models;
using System.Web;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{   

    private readonly ILogger<TaskController> _logger;
    private FSEXContext FSDB = new FSEXContext();

    public TaskController(ILogger<TaskController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get([FromHeader] string UID, [FromHeader] string TOKEN)
    {
        AuthHeaderModel AHM = new AuthHeaderModel(Request.Headers);
        string ATH_MSG = AHM.CheckAuth();
        if(!string.IsNullOrEmpty(ATH_MSG))
            return BadRequest(ATH_MSG);

        var result = FSDB.DatTasks.ToList();
        return new JsonResult(result);
    }

    [HttpPut]
    public IActionResult Post([FromHeader]string UID, [FromHeader]string TOKEN, [FromBody]DatTask NTK)
    {
        try{
            AuthHeaderModel AHM = new AuthHeaderModel(Request.Headers);
            string ATH_MSG = AHM.CheckAuth();
            if(!string.IsNullOrEmpty(ATH_MSG))
                return BadRequest(ATH_MSG);
            
            var oldTK = FSDB.DatTasks.Where(f => f.DtkId == NTK.DtkId).FirstOrDefault();
            if(oldTK != null)
            {
                oldTK.DtkTaskName = NTK.DtkTaskName;
                oldTK.DtkTktId = NTK.DtkTktId;
                oldTK.DtkPriority = NTK.DtkPriority;
                oldTK.DtkDuration = NTK.DtkDuration;
                oldTK.DtkDurationType = NTK.DtkDurationType;
                oldTK.DtkDescription = NTK.DtkDescription;
                oldTK.DtkCreateByUid = NTK.DtkCreateByUid;
                oldTK.DtkAssignToUid = NTK.DtkAssignToUid;
                oldTK.DtkStatus = NTK.DtkStatus;
                FSDB.SaveChanges();                
            }
            else{
                NTK.DtkCreatedate = DateTime.Now;
                FSDB.DatTasks.Add(NTK);
                FSDB.SaveChanges();
            }
            return Ok(string.Format("Task ID:{0} save complete.", NTK.DtkId));
        }
        catch(Exception ex){
            return StatusCode(500, ex.Message+(ex.InnerException != null? ex.InnerException.Message:""));
        }
    }

    [HttpPatch("{TKID}/{STATUS}")]
    public IActionResult Patch([FromHeader] string UID, [FromHeader] string TOKEN, int TKID, string STATUS)
    {
        try{
            AuthHeaderModel AHM = new AuthHeaderModel(Request.Headers);
            string ATH_MSG = AHM.CheckAuth();
            if(!string.IsNullOrEmpty(ATH_MSG))
                return BadRequest(ATH_MSG);
            
            var oldTK = FSDB.DatTasks.Where(f => f.DtkId == TKID).FirstOrDefault();
            if(oldTK != null)
            {
                oldTK.DtkStatus = STATUS;
                oldTK.DtkComplete = (ulong)(STATUS == "COMPLETE"? 1 : 0);
                FSDB.SaveChanges();                
            }
            else{
                return BadRequest("Task ID invalid");
            }
            return Ok(string.Format("Task ID:{0} update status complete.", TKID));
        }
        catch(Exception ex){
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{TKID}")]
    public IActionResult Delete([FromHeader] string UID, [FromHeader] string TOKEN, int TKID)
    {
        try{
            AuthHeaderModel AHM = new AuthHeaderModel(Request.Headers);
            string ATH_MSG = AHM.CheckAuth();
            if(!string.IsNullOrEmpty(ATH_MSG))
                return BadRequest(ATH_MSG);
            
            var oldTK = FSDB.DatTasks.Where(f => f.DtkId == TKID).FirstOrDefault();
            if(oldTK != null)
            {
                FSDB.DatTasks.Remove(oldTK);
                FSDB.SaveChanges();
                return Ok(string.Format("Task ID:{0} delete complete.",TKID));
            }
            else{
                return BadRequest("Task ID invalid");
            }
        }
        catch(Exception ex){
            return StatusCode(500, ex.Message);
        }        
    }
}
