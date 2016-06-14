using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Telligent.Evolution.Extensibility.Api.Entities.Version1;

namespace te.extension.kharta.PublicApi
{
    public class Hostings
    {
        public static Hosting GetHostingApplication(Guid applicationId)
        {
              
            //KhartaSource khartaSource = new KhartaSource();
            try
            {
                InternalApi.KhartaHosting hosting = InternalApi.HostingDataService.GetHostingApplication(applicationId);
                if (hosting == null)
                    return null;

                return new Hosting(hosting);

            }
            catch (Exception ex)
            {

                return new Hosting(new AdditionalInfo(new Error(ex.GetType().FullName, ex.Message)));
            }
         
    }
    }
}
