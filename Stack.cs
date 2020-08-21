using System;

namespace system
{
    public class Stack
    {
        public ushort[] stack;

        public Stack()
        {
            stack = new ushort[0xff];

            for (int i = 0; i < stack.Length; i++)
            {
                stack[i] = 0x0000;
            }
        }

        public void push(byte stackPointer, ushort value)
        {
            stack[stackPointer] = value;
        }

        public ushort pull(byte stackPointer)
        {
            return stack[stackPointer];
        }
    }
}
