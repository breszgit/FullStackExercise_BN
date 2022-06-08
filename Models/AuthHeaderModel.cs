using System;
using System.Collections.Generic;
using backend.Models.DB;

namespace backend.Models
{
    public partial class AuthHeaderModel
    {
        public string UID { get; set; }
        public string TOKEN { get;set; }

        public AuthHeaderModel(IHeaderDictionary HD){
            HD.TryGetValue("UID",out var _UID);
            HD.TryGetValue("TOKEN",out var _TOKEN);
            UID = _UID.ToString();
            TOKEN = _TOKEN.ToString();
        }

        public string CheckAuth(){
            string result = "You don't have authorize";

            FSEXContext FSDB = new FSEXContext();

            var TK = FSDB.SysTokens.Where(f => f.TknUid == UID && f.TknToken == TOKEN).FirstOrDefault();
            if(TK != null){
                if(TK.TknExpire > DateTime.Now)
                    result = "";
                else
                    result = "Your token was expire please renew it";
            }
            
            return result;
        }
    }
}