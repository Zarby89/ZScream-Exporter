/*
 * Author:  Zarby89
 */

using System;
using ZScream_Exporter.ZCompressLibrary;
/// <summary>
/// Decompresses and compresses data
/// </summary>
public static class Compression
{
    public static int[]
        addresses = new int[223],
        blockSize = new int[223];
    public static byte[] bpp = new byte[223];

    public static byte[] DecompressTiles() //to gfx.bin
    {
        int gfxPointer1 = Addresses.snestopc((ROM.DATA[Constants.gfx_1_pointer + 1] << 8) + (ROM.DATA[Constants.gfx_1_pointer])),
                gfxPointer2 = Addresses.snestopc((ROM.DATA[Constants.gfx_2_pointer + 1] << 8) + (ROM.DATA[Constants.gfx_2_pointer])),
                gfxPointer3 = Addresses.snestopc((ROM.DATA[Constants.gfx_3_pointer + 1] << 8) + (ROM.DATA[Constants.gfx_3_pointer]));

        byte[]
            buffer = new byte[0x6F800],// (185)
            bufferBlock;

        int bufferPos = 0;
        for (int i = 0; i < 96; i++)
        {
            byte[] b = new byte[] { ROM.DATA[gfxPointer3 + i], ROM.DATA[gfxPointer2 + i], ROM.DATA[gfxPointer1 + i], 0 };
            int addr = BitConverter.ToInt32(b, 0);
            addresses[i] = Addresses.snestopc(addr);
            //Console.WriteLine(Addresses.snestopc(addr).ToString("X6"));
            byte[] tbufferBlock = Decompress.ALTTPDecompressGraphics(ROM.DATA, Addresses.snestopc(addr), 0x800, ref blockSize[i]);
            bufferBlock = tbufferBlock;
            if (tbufferBlock.Length != 0x600)
            {
                bpp[i] = 2;
                bufferBlock = new byte[0x600];
                for (int j = 0; j < 0x600; j++)
                    bufferBlock[j] = tbufferBlock[j];
            }
            else bpp[i] = 3;
            //bufferBlock = Decompress(Addresses.snestopc(addr), ROM.DATA);
            for (int j = 0; j < bufferBlock.Length; j++)
            {
                buffer[bufferPos] = bufferBlock[j];
                bufferPos++;
            }
        }

        for (int i = 96; i < 223; i++)
        {
            bpp[i] = 3;
            if (i < 115 || i > 126) //not compressed
            {
                byte[] b = new byte[] { ROM.DATA[gfxPointer3 + i], ROM.DATA[gfxPointer2 + i], ROM.DATA[gfxPointer1 + i], 0 };
                int addr = BitConverter.ToInt32(b, 0);
                addresses[i] = Addresses.snestopc(addr);
                byte[] tbufferBlock = Decompress.ALTTPDecompressGraphics(ROM.DATA, Addresses.snestopc(addr), 0x800, ref blockSize[i]);
                bufferBlock = tbufferBlock;
                if (tbufferBlock.Length != 0x600)
                {
                    bpp[i] = 2;
                    bufferBlock = new byte[0xC00];
                    //Console.WriteLine(tbufferBlock.Length);
                    for (int j = 0; j < tbufferBlock.Length; j++)
                        bufferBlock[j] = tbufferBlock[j];
                    //Console.WriteLine("Buffer Size :" + tbufferBlock.Length.ToString("X4"));
                }

                for (int j = 0; j < bufferBlock.Length; j++)
                {
                    buffer[bufferPos] = bufferBlock[j];
                    bufferPos++;
                }
            }
            else
            {
                byte[] b = new byte[] { ROM.DATA[gfxPointer3 + i], ROM.DATA[gfxPointer2 + i], ROM.DATA[gfxPointer1 + i], 0 };
                int addr = BitConverter.ToInt32(b, 0);
                addr = Addresses.snestopc(addr);
                bpp[i] = 3;
                for (int j = 0; j < 0x600; j++)
                {
                    buffer[bufferPos] = ROM.DATA[addr + j];
                    bufferPos++;
                }
            }
        }
        /*FileStream fs = new FileStream("testgfx.gfx", FileMode.OpenOrCreate, FileAccess.Write);
        fs.Write(buffer.ToArray(), 0, buffer.Length);
        fs.Close();*/
        return buffer;
    }
}