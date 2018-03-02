using MediaInfoNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaInfor
{
    class Program
    {
        static void Main(string[] args)
        {
            MediaFile aviFile = new MediaFile(AppDomain.CurrentDomain.BaseDirectory + @"\V70514-151553.mp4");
            
            Console.WriteLine();
            Console.WriteLine("General ---------------------------------");
            Console.WriteLine();
            Console.WriteLine("File Name : {0}", aviFile.Name);
            Console.WriteLine("Format : {0}", aviFile.General.Format);
            Console.WriteLine("Duration : {0}", aviFile.General.DurationString);
            Console.WriteLine("Bitrate : {0}", aviFile.General.Bitrate);

            if (aviFile.Audio.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Audio ---------------------------------");
                Console.WriteLine();
                Console.WriteLine("Format : {0}", aviFile.Audio[0].Format);
                Console.WriteLine("Bitrate : {0}", aviFile.Audio[0].Bitrate.ToString());
                Console.WriteLine("Channels : {0}", aviFile.Audio[0].Channels.ToString());
                Console.WriteLine("Sampling : {0}", aviFile.Audio[0].SamplingRate.ToString());
            }

            if (aviFile.Video.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Video ---------------------------------");
                Console.WriteLine();
                Console.WriteLine("Format : {0}", aviFile.Video[0].Format);
                Console.WriteLine("Bit rate : {0}", aviFile.Video[0].Bitrate.ToString());
                Console.WriteLine("Frame rate : {0}", aviFile.Video[0].FrameRate.ToString());
                Console.WriteLine("Frame size : {0}", aviFile.Video[0].FrameSize.ToString());
            }

            Console.ReadLine();
        }
    }
}
