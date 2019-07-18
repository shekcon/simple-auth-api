using System;

namespace WebApi.Helpers
{
    /// <sumary> 
    /// Get datetime now at timezone +7 (vietnam)
    /// </sumary>
    public interface IDateTimeZone {
        DateTime Create();
    }
    /// <sumary> 
    /// Get datetime now at timezone +7 (vietnam)
    /// </sumary>
    public class DateTimeZone: IDateTimeZone{
        private TimeZoneInfo _timezone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Bangkok");
        public DateTime Create(){
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timezone);
        }
    }
    
}