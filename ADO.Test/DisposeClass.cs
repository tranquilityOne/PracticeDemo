using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.Test
{
    public class DisposeClass : IDisposable
    {
        // Flag: Has Dispose already been called?
        bool disposed = false;
        public List<TravelStatistics> listTravel { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
            }
            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        ~DisposeClass()
        {
            Console.WriteLine("~DisposeClass()");
            Dispose(false);
        }
    }
}
