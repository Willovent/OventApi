using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
#pragma warning disable CS0108

namespace KodiRpc.PVR
{
   public class GetTimerDetailsResponse
   {
       public KodiRpc.PVR.Details.Timer timerdetails { get; set; }
    }
}
