using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodesavanjaAlgoritama
{
    public abstract class AlgorithmSettings 
    {
        public abstract bool ConsistantSettings();
    }

    public interface ISettingsManager<T> where T : AlgorithmSettings
    {
        void Save(T settings);
        T Load();
    }
}
