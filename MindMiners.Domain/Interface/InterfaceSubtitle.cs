using MindMiners.Domain.Entities;
using MindMiners.Domain.Interface.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindMiners.Domain.Interface
{
    public interface InterfaceSubtitle : InterfaceGeneric<SubtitleEntity>
    {
        SubtitleEntity GetByName(string name);
    }
}
