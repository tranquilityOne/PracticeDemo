using SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.ClientEngine;

namespace SuperSocket.Client
{
    class TestReceiveFilter : BeginEndMarkReceiveFilter<StringPackageInfo>
    {
        static byte[] beginMark = new byte[] { 0x23, 0x23 };
        static byte[] endMark = new byte[] { 0x24, 0x24 };
        public TestReceiveFilter()
            : base(beginMark, endMark)
        { 
            
        }

        public override StringPackageInfo Filter(BufferList data, out int rest)
        {
            return base.Filter(data, out rest);
        }

        public override StringPackageInfo ResolvePackage(IBufferStream bufferStream)
        {
            throw new NotImplementedException();
        }
        
    }
}
