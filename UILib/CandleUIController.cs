using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace UILib
{
    public class CandleUIController : UIController
    {
        public CandleUIController(int maxCount) : base(maxCount)
        {
            SeriesCollection = new SeriesCollection{
                new CandleSeries()
                {
                    Values = new ChartValues<OhlcPoint>(),
                }
            };
        }

        public override void AddValueToLine(Candle candle, Func<Candle, decimal> func = null)
        {
            var lineValues = SeriesCollection[0].Values;

            lineValues.Add(new OhlcPoint((double)candle.Open, (double)candle.High, (double)candle.Low, (double)candle.Close));

            while (lineValues.Count > MaxPointsCount)
            {
                lineValues.RemoveAt(0);
            }
        }
    }
}
