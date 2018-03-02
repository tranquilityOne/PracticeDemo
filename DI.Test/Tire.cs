using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Test
{
    /// <summary>
    /// tire define
    /// </summary>
    public  class Tire
    {
        private int size { get; set; }

        public Tire()
        {
            this.size = 30;
        }
        public Tire(int size)
        {
            this.size = size;
        }

    }
}
