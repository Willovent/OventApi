using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
#pragma warning disable CS0108

namespace KodiRpc.AudioLibrary
{
   public class GetRolesResponse
   {
       public KodiRpc.List.LimitsReturned limits { get; set; }
       public global::System.Collections.Generic.List<KodiRpc.Audio.Details.Role> roles { get; set; }
    }
}
