using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

[XmlRoot("Session")]
public class Session {
    [XmlAttribute("hour")]
    public string Hour { get; set; }
    public string Title { get; set; }
    public string Abstract { get; set; }

    [XmlElement("Speaker", typeof(Speaker))]
    public List<Speaker> Speakers { get; set; }
}

