using MindMiners.Application.Interface;
using MindMiners.Domain.Entities;
using MindMiners.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MindMiners.Application.Apps
{
    public class ApplicationSubtitle : IAppSubtitle
    {
        InterfaceSubtitle interfaceSubtitle;
        SubtitleEntity subtitle = new SubtitleEntity();

        public ApplicationSubtitle(InterfaceSubtitle interfaceSubtitle)
        {
            this.interfaceSubtitle = interfaceSubtitle;
        }

        public SubtitleEntity AddTime(TimeSpan t)
        {
            if (this.subtitle == null || this.subtitle.Lines.Count == 0)
                throw new Exception("Nenhuma legenda carregada");

            var list = new List<Task>();

            foreach (var dic in subtitle.Lines)
            {
                list.Add(this.AddTimeAsync(t, dic.Value));
            }

            Task.WaitAll(list.ToArray());

            subtitle.Date = DateTime.Now;
            subtitle.Name = String.Format("{0}{1}", subtitle.Date.Ticks.ToString(), subtitle.OriginalName);
            subtitle.IsOriginal = false;
            subtitle.TimeAdded = t;

            this.interfaceSubtitle.Add(subtitle);

            return subtitle;
        }

        private async Task<SubtitleLineEntity> AddTimeAsync(TimeSpan t, SubtitleLineEntity line)
        {
            Console.WriteLine("Starting parser");
            var taskResult = await Task.Run(() =>
            {
                line.Start = line.Start.Add(t);
                line.End = line.End.Add(t);

                return line;
            });
            return taskResult;
        }

        public IList<SubtitleEntity> Listar()
        {
            return this.interfaceSubtitle.List();
        }

        public void AddSubtitle(StringBuilder srt, string name)
        {
            this.subtitle = this.SetNewSubtitle(name);

            string[] delim = { Environment.NewLine, "\n" };
            string[] lines = srt.ToString().Split(delim, StringSplitOptions.None);
            int currentPos = 0;
            int posCurrentLine = 1;


            foreach (var line in lines)
            {
                if (!String.IsNullOrEmpty(line))
                {
                    if (line == (currentPos + 1).ToString())
                    {
                        currentPos++;
                        this.subtitle.Lines.Add(currentPos, new SubtitleLineEntity() { Text = new StringBuilder() });
                        posCurrentLine = 2;
                        continue;
                    }

                    if (posCurrentLine == 2)
                    {
                        var times = line.Split("-->");
                        var cult = CultureInfo.GetCultureInfo("fr");
                        this.subtitle.Lines[currentPos].Start = TimeSpan.Parse(times[0].Trim(), cult);
                        this.subtitle.Lines[currentPos].End = TimeSpan.Parse(times[1].Trim(), cult);
                        posCurrentLine++;
                        continue;
                    }

                    if (posCurrentLine > 2)
                    {
                        this.subtitle.Lines[currentPos].Text.AppendLine(line);
                        posCurrentLine++;
                    }
                }
            }

            //this.interfaceSubtitle.Add(this.subtitle);
        }

        private SubtitleEntity SetNewSubtitle(string name)
        {
            return new SubtitleEntity()
            {
                Name = name,
                Date = DateTime.Now,
                Lines = new Dictionary<int, SubtitleLineEntity>(),
                IsOriginal = true,
                OriginalName = name
            };
        }
        public string TesteDaMindMiners()
        {
            return "Test da Mind Miners";
        }

        public SubtitleEntity GetSubtitle(string name)
        {
            return  this.interfaceSubtitle.GetByName(name);


        }
    }
}
