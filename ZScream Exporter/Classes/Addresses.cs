/*
 * Author:  Zarby89
 */

using System;

/// <summary>
/// 
/// </summary>
public static class Addresses
{
    /// <summary>
    /// Convert Snes Address in PC Address - Mirrored Format
    /// </summary>
    /// <param name="addr"></param>
    /// <returns></returns>
    public static int snestopc(int addr)
    {
        int temp = (addr & 0x7FFF) + ((addr / 2) & 0xFF8000);
        return (temp + 0x0);
    }

    /// <summary>
    /// Convert PC Address to Snes Address
    /// </summary>
    /// <param name="addr"></param>
    /// <returns></returns>
    public static int pctosnes(int addr)
    {
        byte[] b = BitConverter.GetBytes(addr);
        b[2] = (byte)(b[2] * 2);
        if (b[1] >= 0x80)
        
            b[2] += 1;
        else b[1] += 0x80;
        
        return BitConverter.ToInt32(b, 0);
        //snes always have + 0x8000 no matter what, the bank on pc is always / 2

        //return ((addr * 2) & 0xFF0000) + (addr & 0x7FFF) + 0x8000;
    }
}