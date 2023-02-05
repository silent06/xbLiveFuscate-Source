using System;
using System.IO;
using System.Text;

namespace xbLiveFuscate
{

    internal class EndianWriter : BinaryWriter
    {
        private readonly EndianStyle _endianStyle;

        public EndianWriter(Stream Stream, EndianStyle EndianStyle)
            : base(Stream)
        {
            _endianStyle = EndianStyle;
        }

        public void Seek(int position)
        {
            base.Seek(position, SeekOrigin.Begin);
        }

        public void SeekNWrite(int position, double value)
        {
            Seek(position);
            Write(value, _endianStyle);
        }

        public void SeekNWrite(int position, byte value)
        {
            Seek(position);
            Write(value, _endianStyle);
        }

        public void SeekNWrite(int position, short value)
        {
            Seek(position);
            Write(value, _endianStyle);
        }

        public void SeekNWrite(int position, byte[] value)
        {
            Seek(position);
            base.Write(value);
        }

        public void SeekNWrite(int position, int value)
        {
            Seek(position);
            Write(value, _endianStyle);
        }

        public void SeekNWrite(int position, long value)
        {
            Seek(position);
            Write(value, _endianStyle);
        }

        public void SeekNWrite(int position, float value)
        {
            Seek(position);
            Write(value, _endianStyle);
        }

        public void SeekNWrite(int position, ushort value)
        {
            Seek(position);
            Write(value, _endianStyle);
        }

        public void SeekNWrite(int position, uint value)
        {
            Seek(position);
            Write(value, _endianStyle);
        }

        public void SeekNWrite(int position, ulong value)
        {
            Seek(position);
            Write(value, _endianStyle);
        }

        public override void Write(double value)
        {
            Write(value, _endianStyle);
        }

        public override void Write(short value)
        {
            Write(value, _endianStyle);
        }

        public override void Write(int value)
        {
            Write(value, _endianStyle);
        }

        public override void Write(long value)
        {
            Write(value, _endianStyle);
        }

        public override void Write(float value)
        {
            Write(value, _endianStyle);
        }

        public override void Write(ushort value)
        {
            Write(value, _endianStyle);
        }

        public override void Write(uint value)
        {
            Write(value, _endianStyle);
        }

        public override void Write(ulong value)
        {
            Write(value, _endianStyle);
        }

        public void Write(double value, EndianStyle EndianStyle)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianStyle == EndianStyle.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void Write(short value, EndianStyle EndianStyle)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianStyle == EndianStyle.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void Write(int value, EndianStyle EndianStyle)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianStyle == EndianStyle.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void Write(long value, EndianStyle EndianStyle)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianStyle == EndianStyle.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void Write(float value, EndianStyle EndianStyle)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianStyle == EndianStyle.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void Write(ushort value, EndianStyle EndianStyle)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianStyle == EndianStyle.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void Write(uint value, EndianStyle EndianStyle)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianStyle == EndianStyle.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void Write(ulong value, EndianStyle EndianStyle)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianStyle == EndianStyle.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void WriteInt24(int value)
        {
            WriteInt24(value, _endianStyle);
        }

        public void WriteInt24(int value, EndianStyle EndianStyle)
        {
            byte[] array = BitConverter.GetBytes(value);
            Array.Resize(ref array, 3);
            if (EndianStyle == EndianStyle.BigEndian)
            {
                Array.Reverse(array);
            }
            base.Write(array);
        }

        public void WriteString(string value)
        {
            char[] chars = value.ToCharArray();
            base.Write(chars);
        }

        public void WriteUnicodeString(string Value)
        {
            byte[] bytes = Encoding.BigEndianUnicode.GetBytes(Value);
            base.Write(bytes);
            base.Write(new byte[2]);
        }

        public void WriteUnicodeString(string String, int length)
        {
            WriteUnicodeString(String, length, _endianStyle);
        }

        public void WriteUnicodeString(string String, int length, EndianStyle endianStyle)
        {
            int length2 = String.Length;
            for (int i = 0; i < length2 && i <= length; i++)
            {
                ushort value = String[i];
                Write(value, endianStyle);
            }
            int num = (length - length2) * 2;
            if (num > 0)
            {
                Write(new byte[num]);
            }
        }
    }
}