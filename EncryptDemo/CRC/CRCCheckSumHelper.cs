using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EncryptDemo
{
    class CRC
    {
        internal static String GetCRC_CCITT(String value)
        {
            byte[] Bt = value.HexStringToByte();
            var Re = GetCRC_CCITT(Bt, Bt.Length);

            return BitConverter.GetBytes(Re).ToArray().ToHexString(); ;
        }
        internal static ushort GetCRC_CCITT(byte[] data, int len)
        {
            return new Crc16Ccitt(InitialCRCValue.Kermit).CalculateCheckSum(data);
        }

    }

    enum InitialCRCValue { XModem, NonZero1 = 0xffff, NonZero2 = 0x1D0F, Kermit }

    class Crc16Ccitt
    {
        const ushort poly = 4129;
        ushort[] table = new ushort[256];
        ushort initialValue = 0;

        internal ushort CalculateCheckSum(byte[] bytes)
        {
            if (this.vsl == InitialCRCValue.Kermit)
            {
                return (ushort)CalculateCRC(bytes);
            }

            ushort crc = this.initialValue;
            for (int i = 0; i < bytes.Length; ++i)
            {
                crc = (ushort)((crc << 8) ^ table[((crc >> 8) ^ (0xff & bytes[i]))]);
            }
            return crc;
        }


        int CalculateCRC(byte[] val)
        {
            long crc;
            long q;
            byte c;
            crc = 0;
            for (int i = 0; i < val.Length; i++)
            {
                c = val[i];
                q = (crc ^ c) & 0x0f;
                crc = (crc >> 4) ^ (q * 0x1081);
                q = (crc ^ (c >> 4)) & 0xf;
                crc = (crc >> 4) ^ (q * 0x1081);
            }
            return (byte)crc << 8 | (byte)(crc >> 8);
        }
        InitialCRCValue vsl;
        internal Crc16Ccitt(InitialCRCValue initialValue)
        {
            this.vsl = initialValue;
            this.initialValue = (ushort)initialValue;
            ushort temp, a;
            for (int i = 0; i < table.Length; ++i)
            {
                temp = 0;
                a = (ushort)(i << 8);
                for (int j = 0; j < 8; ++j)
                {
                    if (((temp ^ a) & 0x8000) != 0)
                    {
                        temp = (ushort)((temp << 1) ^ poly);
                    }
                    else
                    {
                        temp <<= 1;
                    }
                    a <<= 1;
                }
                table[i] = temp;
            }
        }
    }
}
