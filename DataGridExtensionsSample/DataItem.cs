﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataGridExtensionsSample
{
    public class DataItem
    {
        public DataItem(Random rand, int index)
        {
            Flag = rand.Next(2) == 0;
            Index = index;
            Column1 = Guid.NewGuid().ToString();
            Column2 = rand.Next(20) == 0 ? null : Guid.NewGuid().ToString();
            Column3 = Guid.NewGuid().ToString();
            Column4 = Guid.NewGuid().ToString();
            Probability = rand.NextDouble();
        }

        public bool Flag { get; private set; }
        public int Index { get; private set; }
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
        public string Column4 { get; set; }
        public double Probability { get; private set; }
    }
}
