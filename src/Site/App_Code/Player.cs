using System;
using System.Web;
using System.Web.Caching;

public static class Player {
    public static string GetVideoUrl() {
        var context = HttpContext.Current;
        var videoUrl = ConfContext.GetPlayerUrl();

        if(string.IsNullOrEmpty(videoUrl)) {
            videoUrl = ConfContext.PlayerUrl;
            ConfContext.SetPlayerUrl(videoUrl);
        }

        return videoUrl;
    }
}
