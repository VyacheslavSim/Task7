using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Data;

namespace UILib
{
    public class ParabolicSARController : UIController
    {
        public ParabolicSARController(int maxCount) : base(maxCount)
        {
            SeriesCollection = new SeriesCollection{
                new LineSeries
                {
                    AreaLimit = -10,
                    Values = new ChartValues<ObservableValue>(),
                }
            };
        }

        public override void AddValueToLine(Candle candle, Func<Candle, decimal> func)
        {
            var curValue = func.Invoke(candle);
            var lineValues = SeriesCollection[0].Values;
            double value = LastCandle == null ? 0 : (double)(candle.High -  func.Invoke(LastCandle)) * 0.02 + (double)func.Invoke(LastCandle);
            lineValues.Add(new ObservableValue(value));
            while (lineValues.Count > MaxPointsCount)
            {
                lineValues.RemoveAt(0);
            }
            LastCandle = candle;
        }
    }
}
