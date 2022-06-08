using Microsoft.AspNetCore.Mvc;
using backend.Models.DB;
using System.Security.Cryptography;

namespace backend.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase
{   

    private readonly ILogger<AuthController> _logger;
    private FSEXContext FSDB = new FSEXContext();
    const int TokenEXPHour = 24;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult UserAuth(string Username, string PWD)
    {
        try{
            //--Check User--
            string _PWD = HasMD5(PWD,"F@","$");
            var User = FSDB.SysUsers.Where(f => f.UsrUsername == Username && f.UsrPwd == _PWD).FirstOrDefault();
            if(User != null){
                //--Create Token--
                string _TOKEN = HasMD5(DateTime.Now.ToString("yyyyMMddHHmmss"),"F","@");
                
                //--Check Old Token--
                var oldTK = FSDB.SysTokens.Where(f => f.TknUid == User.UsrUid).FirstOrDefault();
                if(oldTK != null)
                    FSDB.SysTokens.Remove(oldTK);

                //--Save Token--
                DateTime TKCreate = DateTime.Now;
                DateTime TKExpire = TKCreate.AddHours(TokenEXPHour);
                SysToken newTK = new SysToken(){
                    TknUid = User.UsrUid,
                    TknToken = _TOKEN,
                    TknCreatedate = TKCreate,
                    TknExpire = TKExpire
                };
                FSDB.SysTokens.Add(newTK);
                FSDB.SaveChanges();

                //--Return Token--
                var UA = new {
                    UID = User.UsrUid,
                    TOKEN = _TOKEN
                };   
                return new JsonResult(UA);
            }
            else{
                return BadRequest("Username or Password Invalid!!!");
            }
        }
        catch(Exception ex){
            return StatusCode(500, ex.Message);
        }        
    }

    private string HasMD5(string hasValue, string PPKey = "", string APKey = "")
    {
        //Add special Key
        hasValue = PPKey+hasValue+APKey;

        // Use input string to calculate MD5 hash
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(hasValue);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes); // .NET 5 +
        }
    }
}
