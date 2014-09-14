using System;
using System.Linq;

public static class SessionExtensions {
    public static bool HasSpeakerBio(this Session session) {
        return session.Speakers != null && 
            !session.Speakers.Any(speaker => string.IsNullOrEmpty(speaker.Id));
    }

    public static DateTime GetSessionTime(this Session session, ConfDay day) {
        var shortDate = day.Date.ToString("MM-dd-yyyy");
        return DateTime.Parse(shortDate + " " + session.Hour);
    }

    public static DateTime ToAttendeeTime(this Session session, ConfDay day) {
        var sessionTime = GetSessionTime(session, day);
        var pstZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
        return TimeZoneInfo.ConvertTimeToUtc(sessionTime, pstZone);
    }
}