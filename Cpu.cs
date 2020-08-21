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
                        EXT();
                        break;
                    case 0x13:
                        PRA();
                        break;
                    case 0x18:
                        CLC();
                        break;
                    case 0x48:
                        PHA();
                        break;
                    case 0x68:
                        PLA();
                        break;
                    case 0x6c:
                        JMP();
                        break;
                    case 0x6d:
                        ADC();
                        break;
                    case 0x70:
                        BVS();
                        break;
                    case 0x8d:
                        STA();
                        break;
                    case 0xa9:
                        LDA_IMM();
                        break;
                    case 0xad:
                        LDA_ABS();
                        break;
                    case 0xea:
                        NOP();
                        break;
                    case 0xee:
                        INC();
                        break;
                    default:
                        PC += 1;
                        break;
                }
            }
        }

        public void ADC()
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

        public void BVS()
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

        public void CLC()
        {
            C = 0x0;
            PC += 0x1;
        }

        public void EXT()
        {
            System.Environment.Exit(0);
        }

        public void INC()
        {
            int add1 = rom.read((ushort)(PC + 1));
            int add2 = rom.read((ushort)(PC + 2)) * 256 + add1;
            byte value = ram.read((ushort)(add2));
            byte normal = value;
            value += 1;
            if (value < normal)
            {
                V = 0x1;
            }
            ram.write((ushort)(add2), value);
            PC += 3;
        }

        public void JMP()
        {
            int add1 = rom.read((ushort)(PC + 1));
            int add2 = rom.read((ushort)(PC + 2)) * 256 + add1;
            PC = (ushort)(add2);
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

        public void NOP()
        {
            PC += 0x1;
        }

        public void PHA()
        {
            stack.push(S, (ushort)(A));
            S += 1;
            PC += 0x1;
        }

        public void PLA()
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

        public void STA()
        {
            int add1 = rom.read((ushort)(PC + 1));
            int add2 = rom.read((ushort)(PC + 2)) * 256 + add1;
            ram.write((ushort)(add2), A);
            PC += 0x3;
        }
    }
}
