using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

public static class ConfContext {
    public static string PlayerUrl {
        get{
            return ConfigurationManager.AppSettings["url.playerUrl"];   
        }
    }

    public static string SpeakerUrl {
        get{
            return ConfigurationManager.AppSettings["url.speakerUrl"];
        }
    }

    public static string ScheduleUrl {
        get{
            return ConfigurationManager.AppSettings["url.scheduleUrl"];
        }
    }

    public static ConfDay GetDay(int day) {
        var context = HttpContext.Current;
        var key = string.Format("__conf_day{0}", day);
        return context.Cache[key] as ConfDay;
    }

    public static void SetDay(ConfDay day) {
        var key = string.Format("__conf_day{0}", day.Id);
        AddToCache(key, day, DateTime.Now.AddHours(12));
    }

    public static IList<Speaker> GetSpeakers() {
        var context = HttpContext.Current;
        return context.Cache["__speakers"] as IList<Speaker>;
    }

    public static void SetSpeakers(IList<Speaker> speakers) {
        AddToCache("__speakers", speakers, DateTime.Now.AddHours(12));
    }

    public static string GetPlayerUrl() {
        var context = HttpContext.Current;
        return context.Cache["__videoUrl"] as string;
    }

    public static void SetPlayerUrl(string playerUrl) {
        AddToCache("__videoUrl", playerUrl, DateTime.Now.AddMinutes(10));
    }

    private static void AddToCache(string key, object value, DateTime duration) {
        var context = HttpContext.Current;
        context.Cache.Add(key, value, null, 
            duration, Cache.NoSlidingExpiration, 
            CacheItemPriority.High, 
            null);
    }

    public static void FlushKey( string key) {
        var context = HttpContext.Current;
        context.Cache.Remove(key);
    }

    public static void FlushDays() {
        var format = "__conf_day{0}";
        for(int i=0; i<2; i++) {
            var key = string.Format(format, i+1);
            FlushKey(key);
        }
    }

    public static void FlushSpeakers() {
        FlushKey("__speakers");
    }
}
