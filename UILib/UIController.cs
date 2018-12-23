using Data;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UILib
{
    public abstract class UIController
    {
        public SeriesCollection SeriesCollection { get; set; } = new SeriesCollection();

        public Candle LastCandle { get; protected set; }
        public int MaxPointsCount { get; set; }

        public UIController(int maxCount) { MaxPointsCount = maxCount; }

        public abstract void AddValueToLine(Candle candle, Func<Candle, decimal> func);
    }
}

