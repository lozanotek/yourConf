using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web;

public static class Speakers {
   public static IList<Speaker> GetSpeakers() {
       var speakers = ConfContext.GetSpeakers();

       if(speakers == null) {
           speakers = BuildList();
           ConfContext.SetSpeakers(speakers);
       }

       return speakers;
   }

   private static IList<Speaker> BuildList() {
       var document = GetSpeakerXml();
       var rootElement = document.Root;

       var query = from elem in rootElement.Elements()
        select new Speaker {
           Bio = elem.Element("Bio").Value,
           Name = elem.Element("Name").Value,
           Id = elem.Element("Id").Value,
           Picture = elem.Element("Picture").Value,
           Website = elem.Element("Website").Value,
        };

        return query
            .Where(speaker => !string.IsNullOrEmpty(speaker.Bio))
            .OrderBy(speaker => speaker.Name)
            .ToList();
   }

   private static XDocument GetSpeakerXml() {
       var path = ConfContext.SpeakerUrl;
       path = HttpContext.Current.Server.MapPath(path);
       return XDocument.Load(path);
   }
}
