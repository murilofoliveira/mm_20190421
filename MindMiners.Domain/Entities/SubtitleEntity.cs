using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MindMiners.Domain.Entities
{

    public class SubtitleEntity : ICloneable
    {
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public bool IsOriginal { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public DateTime Date { get; set; }

        public Dictionary<int, SubtitleLineEntity> Lines { get; set; }
        public List<string> StrFile { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }



    public static class SubtitleEntityExtension
    {
        public static void ToSrtFile(this SubtitleEntity file)
        {
            var strFile = new List<string>();
            foreach (var line in file.Lines)
            {
                strFile.Add(line.Key.ToString());

                strFile.Add(String.Format(@"{0} --> {1}", line.Value.Start.ToString().PadRight(12, '0').Substring(0, 12).Replace('.', ','), line.Value.End.ToString().PadRight(12, '0').Substring(0, 12).Replace('.', ',')));

                strFile.Add(line.Value.Text.ToString());

            }

            file.StrFile = strFile;

        }
    }


}
