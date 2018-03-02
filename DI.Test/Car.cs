using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Test
{
    /// <summary>
    /// car definition
    /// </summary>
    public class Car
    {
        private Framework frameword;

        public Car()
        {
            this.frameword = new Framework();
        }

        /// <summary>
        /// dependency injection
        /// </summary>
        /// <param name="frameword"></param>
        public Car(Framework frameword)
        {
            this.frameword = frameword;
        }

        public void Run()
        {
            Console.WriteLine("car run...");
        }
    }
}
