using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Data.Interfaces;
using Data;
namespace DataController
{
    public class IncomingDataController
    {
        public delegate void DataEventHandler(Candle data);
        public event DataEventHandler DataReceived;
        private readonly IDataReciever _dataReciever;
        private Timer _t;

        public IncomingDataController(IDataReciever dataReciever)
        {
            _dataReciever = dataReciever;
        }

        public void StartReceive(int timeout)
        {
            _t = new Timer(timeout);
            _t.Elapsed += _t_Elapsed;
            _t.Start();
        }

        public void StopReceive()
        {
            _t.Stop();
        }

        private void _t_Elapsed(object sender, ElapsedEventArgs e)
        {
            var data = _dataReciever.GetData();
            OnDataReceived(data);
        }

        protected virtual void OnDataReceived(Candle data)
        {
            DataReceived?.Invoke(data);
        }
    }
}
