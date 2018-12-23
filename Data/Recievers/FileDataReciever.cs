using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Recievers
{
    public abstract class FileDataReciever : DataReciever
    {
        public FileInfo Source { get; set; }
        protected Queue<Candle> Candles { get; set; } = new Queue<Candle>();

        public FileDataReciever(FileInfo source)
        {
            Source = source;
            ReadDataFromFile();
        }

        public override Candle GetData()
        {
            if (Candles.Count > 0)
            {
                return Candles.Dequeue();
            }
            return null;
        }

        protected abstract void ReadDataFromFile();
    }
}
