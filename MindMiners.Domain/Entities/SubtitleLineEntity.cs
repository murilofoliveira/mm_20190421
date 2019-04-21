using System;
using System.Collections.Generic;
using System.Text;

namespace MindMiners.Domain.Entities
{
    public class SubtitleLineEntity
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public StringBuilder Text { get; set; }
    }
}
