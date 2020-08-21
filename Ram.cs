using System;

namespace system
{
    public class Ram
    {
        public byte[] ram; 

        public Ram()
        {
            ram = new byte[0x8000];

            for (int i = 0; i < ram.Length; i++)
            {
                ram[i] = 0x00;
            }
        }

        public byte read(ushort address)
        {
            return ram[address];
        }

        public void write(ushort address, byte value)
        {
            ram[address] = value;
        }
    }
}
