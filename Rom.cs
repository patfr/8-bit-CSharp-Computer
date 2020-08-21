using System;

namespace system
{
    public class Rom
    {
        public byte[] rom;

        public Rom()
        {
            rom = new byte[0x8000];

            for (int i = 0; i < rom.Length; i++)
            {
                rom[i] = 0x03; // EXT
            }

            rom[0x0000] = 0xa9; // LDA 0x02
            rom[0x0001] = 0x02;
            rom[0x0002] = 0x8d; // STA 0x0000
            rom[0x0003] = 0x00;
            rom[0x0004] = 0x00;
            rom[0x0005] = 0xa9; // LDA 0x01
            rom[0x0006] = 0x01;
            rom[0x0007] = 0x13; // PRA
            rom[0x0008] = 0x6d; // ADC 0x0000
            rom[0x0009] = 0x00;
            rom[0x000a] = 0x00;
            rom[0x000b] = 0x70; // BVS 0xffff
            rom[0x000c] = 0xff;
            rom[0x000d] = 0xff;
            rom[0x000e] = 0x48; // PHA
            rom[0x000f] = 0xa9; // LDA 0x01
            rom[0x0010] = 0x01;
            rom[0x0011] = 0x6d; // ADC 0x0000
            rom[0x0012] = 0x00;
            rom[0x0013] = 0x00;
            rom[0x0014] = 0x8d; // STA 0x0000
            rom[0x0015] = 0x00;
            rom[0x0016] = 0x00;
            rom[0x0017] = 0x68; // PLA
            rom[0x0018] = 0x6c; // JMP 0x8007
            rom[0x0019] = 0x07;
            rom[0x001a] = 0x80;

            // start
            rom[0x7ffc] = 0x00;
            rom[0x7ffd] = 0x80;
        }

        public byte read(ushort address)
        {
            return rom[address - 0x8000];
        }
    }
}
