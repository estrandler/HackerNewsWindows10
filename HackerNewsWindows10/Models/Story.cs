using System;
using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace HackerNewsWindows10.Models
{
    public class Story
    {
        public string by { get; set; }
        public int descendants { get; set; }
        public int id { get; set; }
        public List<int> kids { get; set; }
        public int score { get; set; }
        public long time { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string url { get; set; }

        public string readableTime { get; set; }

        public string SetReadableTime()
        {
            var date = DateTime.Now.AddTicks(time*10000);
            var date2 = DateTime.Now;
            var seconds = date2.TimeOfDay.TotalSeconds - date.TimeOfDay.TotalSeconds;

            var interval = Math.Floor(seconds/31536000);
            if (interval > 1) return string.Format("{0} years ago", interval);

            interval = Math.Floor(seconds/2592000);
            if (interval > 1) return string.Format("{0} months ago", interval);

            interval = Math.Floor(seconds/86400);
            if (interval > 1) return string.Format("{0} days ago", interval);

            interval = Math.Floor(seconds/3600);
            if (interval > 1) return string.Format("{0} hours ago", interval);

            interval = Math.Floor(seconds/60);
            return interval > 1 ? string.Format("{0} hours ago", interval) : null;
        }
    }
}