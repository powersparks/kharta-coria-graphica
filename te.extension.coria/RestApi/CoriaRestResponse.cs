using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.Rest.Version2;

namespace te.extension.coria.RestApi
{
    public class CoriaRestResponse : IRestResponse
    {
        public object Data { get; set; }
        public string[] Errors { get; set; }
        public string Name { get; set; }
    }
}
/****
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telligent.Evolution.Extensibility.Rest.Version2;

namespace Telligent.BigSocial.Polling.RestApi
{
	public class RestResponse : IRestResponse
	{
		public object Data { get; set; }
		public string[] Errors { get; set; }
		public string Name { get; set; }
	}
}
     */
