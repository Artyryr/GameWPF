﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWPF
{
    public class Wall : Building
    {
        public int Lvl { get; set; }
        public Wall()
        {
            Lvl = 1;
        }
    }
}
