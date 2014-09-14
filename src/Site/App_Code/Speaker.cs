using System;

public class Speaker {
    public string Name {get; set;}
    public string FirstName { 
        get {
            var index = Name.IndexOf(" ");
            return Name.Substring(0, index);
        }
    }

    public string LastName { 
        get {
            var index = Name.IndexOf(" ");
            return Name.Substring(index);
        }
    }

    public string Id {get; set;}
    public string Bio {get; set;}
    public string Picture {get; set;}
    public string Website {get; set;}
}
