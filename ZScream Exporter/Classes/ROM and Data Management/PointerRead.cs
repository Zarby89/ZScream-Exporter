/*
 * Author:  Trovsky
 */

using System;

public static class PointerRead
{
    public static int LongRead_LoHiBank(int address)
    { return LongRead_LoHiBank(address, address + 1, address + 2); }

    public static int LongRead_LoHiBank(int loAddress, int hiAddress, int bankAddress)
    {
        return AddressLoROM.SnesToPc(
            ROM.DATA[loAddress],
            ROM.DATA[hiAddress],
            ROM.DATA[bankAddress]);
    }

    public static int ShortRead_LoHi(int address)
    {
        return AddressLoROM.SnesToPc
            (
            ROM.DATA[address],
            ROM.DATA[address + 1],
            AddressLoROM.PcToSnes_Bank(address)
            );
    }

    public const int maxAddressFor3BytePointer = 0xFFFFFF;

    public static void CheckAddressWithinRangeOf3Byte(int address)
    {
        if (!(address < PointerRead.maxAddressFor3BytePointer))
            throw new ArgumentOutOfRangeException();
    }

    public static byte[] GeneratePointer3(int address)
    {
        return new byte[]
        {
            (byte)AddressLoROM.PcToSnes_Lo(address),
            (byte)AddressLoROM.PcToSnes_Hi(address),
            (byte)AddressLoROM.PcToSnes_Bank(address)
        };
    }

    public static byte[] GeneratePointer2(int address)
    {
        return new byte[]
        {
            (byte)AddressLoROM.PcToSnes_Lo(address),
            (byte)AddressLoROM.PcToSnes_Hi(address),
        };
    }
}