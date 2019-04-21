using MindMiners.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MindMiners.Application.Interface
{
    public interface IAppSubtitle : IAppGeneric<SubtitleEntity>
    {
        SubtitleEntity AddTime(TimeSpan t);
        IList<SubtitleEntity> Listar();
        SubtitleEntity GetSubtitle(string name);
        void AddSubtitle(StringBuilder text, string name);
    }
}
