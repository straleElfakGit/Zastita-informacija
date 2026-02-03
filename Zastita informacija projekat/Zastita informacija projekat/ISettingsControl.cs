using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zastita_informacija_projekat
{
    internal interface ISettingsControl
    {
        bool DesilaSePromena { get; }
        void SaveSettings();
    }
}
