/*
 * Author:  Trovsky, ????
 */

using System;

/// <summary>
/// Converts addresses
/// </summary>
public class AddressLoROM
{
    public static int PcToSnes(int addr, bool hasHeader = false)
    {
        //lazy code
        int lo = PcToSnes_Lo(addr, hasHeader),
            hi = PcToSnes_Hi(addr, hasHeader),
            bank = PcToSnes_Bank(addr, hasHeader);
        return Convert.ToInt32(bank.ToString("X2") + "" + hi.ToString("X2") + "" + lo.ToString("X2"), 16);
    }
    public static int SnesToPc(int address, bool hasHeader = false)
    {
        const int temp1 = 0x100, temp2 = temp1 * temp1, temp3 = temp2 * temp1;
        //I don't know exactly why this works, but it does.
        return SnesToPc(
            address % temp1,            //lo
            address % temp2 / temp1,    //hi
            address % temp3 / temp2,    //bank
            hasHeader);
    }

    //source: https://www.smwcentral.net/?p=viewthread&t=13167&page=1&pid=188431#p188431
    public static int SnesToPc(int lo, int hi, int bank, bool hasHeader = false)
    {
        if (((bank & 0xF0) == 0x70) /*|| ((hi & 0x80) != 0x80)*/)
            throw new Exception("Invalid parameters for SNES ROM.");
        return (lo & 0xFF) + (0x100 * (hi & 0xFF)) + (0x8000 * (bank & 0x7F)) - Header_f(!hasHeader) - 0x7E00;
    }
    //source: https://www.smwcentral.net/?p=viewthread&t=13167&page=1&pid=188431#p188431
    public static int PcToSnes_Lo(int addr, bool hasHeader = false)
    {
        ValidCheck(addr, hasHeader);
        return (addr & 0xFF);
    }
    //source: https://www.smwcentral.net/?p=viewthread&t=13167&page=1&pid=188431#p188431
    public static int PcToSnes_Hi(int addr, bool hasHeader = false)
    {
        ValidCheck(addr, hasHeader);
        return (((addr - Header_f(hasHeader)) / 0x100) & 0x7F) + 0x80;
    }
    //source: https://www.smwcentral.net/?p=viewthread&t=13167&page=1&pid=188431#p188431
    public static int PcToSnes_Bank(int addr, bool hasHeader = false)
    {
        ValidCheck(addr, hasHeader);
        int returnVal = ((addr - Header_f(hasHeader)) / 0x8000);
        if (addr >= (0x380000 + (Header_f(hasHeader))))
            returnVal = returnVal | 0x80;
        return returnVal;
    }
    private static void ValidCheck(int addr, bool hasHeader)
    {
        if ((hasHeader && (addr < 0x200)) || (addr >= 0x400000 + Header_f(hasHeader)))
            throw new Exception("Invalid parameters for SNES ROM.");
    }

    private static int Header_f(bool hasHeader)
    { return (hasHeader) ? 0x200 : 0; }
}
