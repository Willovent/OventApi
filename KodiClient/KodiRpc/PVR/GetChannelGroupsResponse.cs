using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
#pragma warning disable CS0108

namespace KodiRpc.PVR
{
   public class GetChannelGroupsResponse
   {
       public global::System.Collections.Generic.List<KodiRpc.PVR.Details.ChannelGroup> channelgroups { get; set; }
       public KodiRpc.List.LimitsReturned limits { get; set; }
    }
}
