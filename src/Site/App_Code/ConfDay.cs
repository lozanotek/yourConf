using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("Day")]
public class ConfDay {
    [XmlIgnore]
    public string Header {
        get {
            return string.Format("Day {0} - {1}", Id, Date.ToLongDateString());
        }
    }

    [XmlElement("Session", typeof(Session))]
    public List<Session> Sessions { get; set; }

    [XmlAttribute("id")]
    public int Id { get; set; }

    [XmlAttribute("date")]
    public DateTime Date { get; set; }
 }
