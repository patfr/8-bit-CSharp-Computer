using System;

namespace system
{
    public class Cpu
    {
        public byte A;
        public byte X;
        public byte Y;
        public ushort PC;
        public byte S;
        public Ram ram;
        public Rom rom;
        public Stack stack;
        public byte C;
        public byte V;
        public byte N;

        public Cpu()
        {
            A = 0x00;
            X = 0x00;
            Y = 0x00;
            PC = 0x0000;
            S = 0x00;
            C = 0x0;
            V = 0x0;
            N = 0x0;
            ram = new Ram();
            rom = new Rom();
            stack = new Stack();
        }

        public void StartProgram()
        {
            int add1 = rom.read(0xfffc);
            PC = (ushort)(rom.read(0xfffd) * 256 + add1);

            while (true)
            {
                switch (rom.read(PC))
                {
                    case 0x03:
                        EXT_Implied();
                        break;
                    case 0x13:
                        PRA();
                        break;
                    case 0x18:
                        CLC_Implied();
                        break;
                    case 0x48:
                        PHA_Implied();
                        break;
                    case 0x68:
                        PLA_Implied();
                        break;
                    case 0x6c:
                        JMP_Indirect();
                        break;
                    case 0x6d:
                        ADC_ABS();
                        break;
                    case 0x70:
                        BVS_Relative();
                        break;
                    case 0x8d:
                        STA_ABS();
                        break;
                    case 0xa9:
                        LDA_IMM();
                        break;
                    case 0xad:
                        LDA_ABS();
                        break;
                    default:
                        PC += 1;
                        break;
                }
            }
        }

        public void ADC_ABS()
        {
            int add1 = rom.read((ushort)(PC + 1));
            int add2 = rom.read((ushort)(PC + 2)) * 256 + add1;
            int normal = A;
            A += ram.read((ushort)(add2));
            A += C;
            if (A < normal)
            {
                V = 0x1;
                C = 0x1;
            }
            else
            {
                V = 0x0;
                C = 0x0;
            }
            PC += 0x3;
        }

        public void BVS_Relative()
        {
            if (V == 0x1)
            {
                int add3 = rom.read((ushort)(PC + 1));
                int add4 = rom.read((ushort)(PC + 2)) * 256 + add3;
                PC = (ushort)(add4);
            }
            else
            {
                PC += 0x3;
            }
        }

        public void CLC_Implied()
        {
            C = 0x0;
            PC += 0x1;
        }

        public void EXT_Implied()
        {
            System.Environment.Exit(0);
        }

        public void JMP_Indirect()
        {
            int add3 = rom.read((ushort)(PC + 1));
            int add4 = rom.read((ushort)(PC + 2)) * 256 + add3;
            PC = (ushort)(add4);
        }

        public void LDA_ABS()
        {
            int add1 = rom.read((ushort)(PC + 1));
            int add2 = rom.read((ushort)(PC + 2)) * 256 + add1;
            A = ram.read((ushort)(add2));
            PC += 0x3;
        }

        public void LDA_IMM()
        {
            A = rom.read((ushort)(PC + 1));
            PC += 0x2;
        }

        public void PHA_Implied()
        {
            stack.push(S, (ushort)(A));
            S += 1;
            PC += 0x1;
        }

        public void PLA_Implied()
        {
            S -= 1;
            A = (byte)(stack.pull(S));
            PC += 0x1;
        }

        public void PRA()
        {
            Console.WriteLine(A);
            PC += 0x1;
        }

        public void STA_ABS()
        {
            int add1 = rom.read((ushort)(PC + 1));
            int add2 = rom.read((ushort)(PC + 2)) * 256 + add1;
            ram.write((ushort)(add2), A);
            PC += 0x3;
        }
    }
}
