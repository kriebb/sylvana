using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections;


namespace PAS.BLL
{
public abstract class Helpers
{
     public static void PurgeCache(string prefix)
        {
             prefix = prefix.ToLower();
            
             foreach (DictionaryEntry item in HttpContext.Current.Cache)
             {
                 if (item.Key.ToString().ToLower().StartsWith(prefix))
                    HttpContext.Current.Cache.Remove(item.Key.ToString());
             }
        }
}
}
