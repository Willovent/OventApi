using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
#pragma warning disable CS0108

namespace KodiRpc.Playlist
{
   public class Item1
   {
       public string directory { get; set; }
       public KodiRpc.Files.Media media { get; set; }
       public bool recursive { get; set; }
    }
}
