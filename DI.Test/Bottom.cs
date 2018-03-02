using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Test
{
    /// <summary>
    /// the bottom of car definition
    /// </summary>
    public class Bottom
    {
        /// <summary>
        /// create relation
        /// </summary>
        private Tire tire;

        public Bottom()
        {
            this.tire = new Tire();
        }

        /// <summary>
        /// Dependency Injection
        /// </summary>
        /// <param name="tire"></param>
        public Bottom(Tire tire)
        {
            this.tire = tire;
        }
    }
}
