using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWPF
{
    public class Workshop : Building
    {
        public int Lvl { get; set; }
        public Workshop()
        {
            Lvl = 1;
        }
    }
}
