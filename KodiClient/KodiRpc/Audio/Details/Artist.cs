using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
#pragma warning disable CS0108

namespace KodiRpc.Audio.Details
{
   public class Artist : KodiRpc.Audio.Details.Base
   {
       public string artist { get; set; }
       public int artistid { get; set; }
       public string born { get; set; }
       public bool compilationartist { get; set; }
       public string description { get; set; }
       public string died { get; set; }
       public string disbanded { get; set; }
       public string formed { get; set; }
       public global::System.Collections.Generic.List<string> instrument { get; set; }
       public bool isalbumartist { get; set; }
       public global::System.Collections.Generic.List<string> mood { get; set; }
       public string musicbrainzartistid { get; set; }
       public global::System.Collections.Generic.List<KodiRpc.Audio.Artist.RolesItem> roles { get; set; }
       public global::System.Collections.Generic.List<KodiRpc.Audio.Details.GenresItem> songgenres { get; set; }
       public global::System.Collections.Generic.List<string> style { get; set; }
       public global::System.Collections.Generic.List<string> yearsactive { get; set; }
    }
}
