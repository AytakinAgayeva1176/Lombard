using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lombard.Helpers
{
    public interface IBaseURL
    {
       
        public string IdentityUrl { get; set; }
      
    }
    public  class BaseURL: IBaseURL
    {
      
        public string IdentityUrl { get; set; }
      

        public BaseURL( string identityUrl)
        {
           
            IdentityUrl = identityUrl;
            
        }
    }
   
}
