using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Web;

public static class Schedule {
    public static ConfDay GetConfDay(int day) {
       var confDay = ConfContext.GetDay(day);
       if(confDay != null) {
           return confDay;
       }

       var document = GetScheduleXml();

       var foundDay = document.Root
           .Elements()
           .Where(e => int.Parse(e.Attribute("id").Value) == day)
           .SingleOrDefault();
       
       if(foundDay == null) {
           return null;
       }

       var serializer = GetSerializer();
       confDay = serializer.Deserialize(foundDay.CreateReader()) as ConfDay;

       var sessions = confDay.Sessions.OrderBy(s => s.GetSessionTime(confDay));
       confDay.Sessions = sessions.ToList();
       ConfContext.SetDay(confDay);

       return confDay;
    }

   private static XmlSerializer GetSerializer(){
       return new XmlSerializer(typeof(ConfDay));
   } 

   private static XDocument GetScheduleXml() {
       var path = ConfContext.ScheduleUrl;
       path = HttpContext.Current.Server.MapPath(path);
       return XDocument.Load(path);
   }
}
