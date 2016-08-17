using System;
using Telligent.Evolution.Extensibility.Version1;

namespace te.extension.coria.InternalApi.Utility
{
    public class CoriaException :  Exception, ILoggableException
    {
        string _category = "Unknown";
        
        public CoriaException() : base(){ }
        public CoriaException( string category, string internalMessage) : base(internalMessage) { _category = category; }
        public CoriaException(string category, string internalMessage, Exception innerException) : base(internalMessage, innerException){ _category = category; }
        public CoriaException(string category, System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info,context){ _category = category; }
        public string Category
        { get { return _category; } }
    }
}
