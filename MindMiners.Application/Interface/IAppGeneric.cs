using System;
using System.Collections.Generic;
using System.Text;

namespace MindMiners.Application.Interface
{
    public interface IAppGeneric<T> where T : class
    {
        string TesteDaMindMiners();
    }
}
