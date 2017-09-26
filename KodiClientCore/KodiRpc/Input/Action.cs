using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
#pragma warning disable CS0108

namespace KodiRpc.Input
{
   public enum Action
   {
       left,
       right,
       up,
       down,
       pageup,
       pagedown,
       select,
       highlight,
       parentdir,
       parentfolder,
       back,
       menu,
       previousmenu,
       info,
       pause,
       stop,
       skipnext,
       skipprevious,
       fullscreen,
       aspectratio,
       stepforward,
       stepback,
       bigstepforward,
       bigstepback,
       chapterorbigstepforward,
       chapterorbigstepback,
       osd,
       showsubtitles,
       nextsubtitle,
       cyclesubtitle,
       playerdebug,
       codecinfo,
       playerprocessinfo,
       nextpicture,
       previouspicture,
       zoomout,
       zoomin,
       playlist,
       queue,
       zoomnormal,
       zoomlevel1,
       zoomlevel2,
       zoomlevel3,
       zoomlevel4,
       zoomlevel5,
       zoomlevel6,
       zoomlevel7,
       zoomlevel8,
       zoomlevel9,
       nextcalibration,
       resetcalibration,
       analogmove,
       analogmovex,
       analogmovey,
       rotate,
       rotateccw,
       close,
       subtitledelayminus,
       subtitledelay,
       subtitledelayplus,
       audiodelayminus,
       audiodelay,
       audiodelayplus,
       subtitleshiftup,
       subtitleshiftdown,
       subtitlealign,
       audionextlanguage,
       verticalshiftup,
       verticalshiftdown,
       nextresolution,
       audiotoggledigital,
       number0,
       number1,
       number2,
       number3,
       number4,
       number5,
       number6,
       number7,
       number8,
       number9,
       smallstepback,
       fastforward,
       rewind,
       play,
       playpause,
       switchplayer,
       delete,
       copy,
       move,
       screenshot,
       rename,
       togglewatched,
       scanitem,
       reloadkeymaps,
       volumeup,
       volumedown,
       mute,
       backspace,
       scrollup,
       scrolldown,
       analogfastforward,
       analogrewind,
       moveitemup,
       moveitemdown,
       contextmenu,
       shift,
       symbols,
       cursorleft,
       cursorright,
       showtime,
       analogseekforward,
       analogseekback,
       showpreset,
       nextpreset,
       previouspreset,
       lockpreset,
       randompreset,
       increasevisrating,
       decreasevisrating,
       showvideomenu,
       enter,
       increaserating,
       decreaserating,
       setrating,
       togglefullscreen,
       nextscene,
       previousscene,
       nextletter,
       prevletter,
       jumpsms2,
       jumpsms3,
       jumpsms4,
       jumpsms5,
       jumpsms6,
       jumpsms7,
       jumpsms8,
       jumpsms9,
       filter,
       filterclear,
       filtersms2,
       filtersms3,
       filtersms4,
       filtersms5,
       filtersms6,
       filtersms7,
       filtersms8,
       filtersms9,
       firstpage,
       lastpage,
       guiprofile,
       red,
       green,
       yellow,
       blue,
       increasepar,
       decreasepar,
       volampup,
       volampdown,
       volumeamplification,
       createbookmark,
       createepisodebookmark,
       settingsreset,
       settingslevelchange,
       stereomode,
       nextstereomode,
       previousstereomode,
       togglestereomode,
       stereomodetomono,
       channelup,
       channeldown,
       previouschannelgroup,
       nextchannelgroup,
       playpvr,
       playpvrtv,
       playpvrradio,
       record,
       togglecommskip,
       showtimerrule,
       leftclick,
       rightclick,
       middleclick,
       doubleclick,
       longclick,
       wheelup,
       wheeldown,
       mousedrag,
       mousemove,
       tap,
       longpress,
       pangesture,
       zoomgesture,
       rotategesture,
       swipeleft,
       swiperight,
       swipeup,
       swipedown,
       error,
       noop,
   }
}
