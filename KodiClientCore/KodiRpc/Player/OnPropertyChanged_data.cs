using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
#pragma warning disable CS0108

namespace KodiRpc.Player
{
   public class OnPropertyChanged_data
   {
       public KodiRpc.Player.Notifications.Player player { get; set; }
       public KodiRpc.Player.Property.Value property { get; set; }
    }
}
