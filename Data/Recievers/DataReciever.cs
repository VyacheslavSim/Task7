﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;

namespace Data.Recievers
{
    public abstract class DataReciever : IDataReciever
    {
        public abstract Candle GetData();
    }
}
