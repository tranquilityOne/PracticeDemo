using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Test
{
    /// <summary>
    /// the framework of car definition
    /// </summary>
    public class Framework
    {
        private Bottom bottom;

        public Framework()
        {
            this.bottom = new Bottom();
        }

        /// <summary>
        /// dependency injection
        /// </summary>
        /// <param name="bottom"></param>
        public Framework(Bottom bottom)
        {
            this.bottom = bottom;
        }
    }
}
