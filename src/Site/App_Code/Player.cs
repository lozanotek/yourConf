using System;
using System.Web;
using System.Web.Caching;
using System.Linq;
using System.Xml.Linq;

public static class Player {
    public static string GetVideoUrl() {
        var context = HttpContext.Current;
        var videoUrl = ConfContext.GetPlayerUrl();

        if(string.IsNullOrEmpty(videoUrl)){
            var videoStream = GetVideoStream();
            if(videoStream == null) return string.Empty;

            videoUrl = string.Format("http://www.youtube.com/embed/{0}", videoStream.VideoId);
            ConfContext.SetPlayerUrl(videoUrl);
        }

        return videoUrl;
    }

    private static VideoStream GetVideoStream() {
        var document = GetStreamDocument();
        if(document == null) return null;
        var rootElement = document.Root;

        var query = from elem in rootElement.Elements()
            select new VideoStream {
                VideoId = elem.Attribute("videoId").Value
         };

        var list = query.ToList();
         
        return list[0];
    }

    private static XDocument GetStreamDocument() {
       var path = ConfContext.PlayerUrl;
       path = HttpContext.Current.Server.MapPath(path);
       return XDocument.Load(path);      
    }
}
