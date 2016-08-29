using System;
using Telligent.Evolution.Extensibility.Version1;
using Telligent.Evolution.Components;
namespace te.extension.coria.InternalApi.Utility
{
    public class CoriaException :  CSException//, ILoggableException//, ITranslatablePlugin
    {
         

        
        public CoriaException( CSExceptionType exType, string internalMessage, Func<CSException,string> trans) : base(exType, internalMessage,trans) {}
      
    }
}
