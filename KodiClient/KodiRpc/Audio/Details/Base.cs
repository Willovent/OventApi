using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
#pragma warning disable CS0108

namespace KodiRpc.Audio.Details
{
   public class Base : KodiRpc.Media.Details.Base
   {
       public string dateadded { get; set; }
       public global::System.Collections.Generic.List<string> genre { get; set; }
    }
}
