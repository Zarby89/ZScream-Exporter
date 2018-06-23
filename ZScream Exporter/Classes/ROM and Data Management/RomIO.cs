/*
 * Author:  Trovsky, qwertymodo (Consultant), Superskuj
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Class for ROM input and output
/// </summary>
public static class RomIO
{
    private static string filePath;

    /// <summary>
    /// Storage for entire ROM
    /// </summary>
    private static byte[] allofROM;
    private static int header_offset;
    private const int
        checksumOffset = 0x3244,
        checkSumLength = 4;

    public static int GetHeaderOffset()
    { return header_offset; }

    /// <summary>
    /// Initilializes a new instance of RomIO
    /// </summary>
    /// <param name="filePath"></param>
    /// <exception cref="FileNotFoundException"></exception>
    /// <exception cref="FileLoadException"></exception>
    /// 
    public static void Constructor(string filePath)
    {
        RomIO.filePath = filePath;
        allofROM = File.ReadAllBytes(filePath);
        header_offset = (IsHeaderless()) ? 0x00 : 0x0200;
    }

    public static bool IsChecksumGood()
    { return !SNESChecksum.isHeaderBad(allofROM); }

    private static void ChangePos(ref int pos)
    { pos += header_offset; }

    /// <summary>
    /// Returns the size of the ROM
    /// </summary>
    public static int Size
    { get { return allofROM.Length; } }

    /// <summary>
    /// Reads the ROM at a specified address
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="size"></param>
    /// <param name="isByteSwap"></param>
    /// <returns></returns>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public static byte[] Read(int pos, int size, bool isByteSwap = false)
    {
        ChangePos(ref pos);
        if (pos < 0 || (size + pos > allofROM.Length) || (pos < 0 && size <= 0))
            throw new IndexOutOfRangeException();
        byte[] byteArray = new byte[size];
        for (int i = 0; i < size; i++)
            byteArray[i] = allofROM[pos + i];
        return (isByteSwap ? ByteSwap(byteArray) : byteArray);
    }

    /// <summary>
    /// Reads the ROM at a specified address
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public static byte Read(int pos)
    {
        ChangePos(ref pos);
        if (pos < Size)
            return allofROM[pos];
        else throw new IndexOutOfRangeException();
    }

    /// <summary>
    /// Read ROM with delimeter
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="delim"></param>
    /// <returns></returns>
    public static List<byte> ReadWithDelim(int pos, int delim)
    {
        ChangePos(ref pos);
        List<byte> list = new List<byte>();
        int i = 0;
        while (allofROM[pos + i] != delim)
            list.Add(allofROM[pos + i++]);
        return list;
    }

    /// <summary>
    /// Read as hex with delimeter. 
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="delim">Optional ref value of the index the delimeter is</param>
    /// <param name="length"></param>
    /// <param name="seperator">Optional seperator</param>
    /// <returns></returns>
    public static string ReadWithDelimAsHex(int pos, int delim, ref int length, string seperator = "")
    {
        ChangePos(ref pos);
        List<byte> b = ReadWithDelim(pos, delim);
        string output = "";
        foreach (byte bb in b)
            output += DecHexCalc.decToHex(bb) + seperator;
        length = b.Count;
        return output;
    }

    /// <summary>
    /// Read as hex with delimeter. 
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="delim"></param>
    /// <param name="seperator"></param>
    /// <returns></returns>

    public static string ReadWithDelimAsHex(int pos, int delim, string seperator = "")
    {
        int end = 0;
        return ReadWithDelimAsHex(pos, delim, ref end, seperator);
    }

    /// <summary>
    /// Read bytes as a hex string
    /// </summary>
    /// <param name="startPosition"></param>
    /// <param name="size"></param>
    /// <param name="seperator"></param>
    /// <returns></returns>
    public static string ReadInHex(int startPosition, int size = 1, string seperator = "")
    {
        ChangePos(ref startPosition);
        string output = "";
        for (int i = 0; i < size; i++)
            output += DecHexCalc.decToHex(allofROM[startPosition + i]) + seperator;
        return output;
    }

    /// <summary>
    /// Read rom as binary values (ones and zeros)
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="size"></param>
    /// <param name="isByteSwap"></param>
    /// <returns></returns>
    public static byte[] ReadInBinary(int pos, int size = 0, bool isByteSwap = false)
    {
        return Read(pos, size, isByteSwap).SelectMany(
            Conversion.GetBitsAsByte).ToArray();
    }

    private static byte[] ByteSwap(byte[] b)
    {
        //make sure the swapping range is even
        for (int i = 0; i < (b.Length - 1) * 2 / 2; i += 2)
        {
            byte temp = b[i];
            b[i] = b[i + 1];
            b[i + 1] = temp;
        }
        return b;
    }

    /// <summary>
    /// Write to ROM array
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool WriteToArray(int pos, byte b)
    { return WriteToArray(pos, 1, new byte[1] { b }); }

    /// <summary>
    /// Write to the ROM array
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="size"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public static bool WriteToArray(int pos, int size, byte[] b)
    {
        ChangePos(ref pos);
        bool pass = false;
        if (!((size + pos > allofROM.Length) || (pos < 0 && size <= 0)))
        {
            for (int i = 0; i < size; i++)
                if (i <= b.Length)
                    allofROM[i + pos] = b[i];
            pass = true;
        }
        else throw new IndexOutOfRangeException();
        return pass;
    }

    /// <summary>
    /// Write to ROM array
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="size"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool WriteToArray(int pos, int size, List<byte> b)
    {
        if (size != b.Count)
            throw new Exception();
        byte[] barr = b.ToArray();
        return WriteToArray(pos, size, barr);
    }

    /// <summary>
    /// Write to ROM file
    /// </summary>
    public static bool WriteToROM()
    {
        bool goodCheck = IsChecksumGood();
        if (!goodCheck)
            allofROM = SNESChecksum.FixROM(allofROM);
        File.WriteAllBytes(filePath, allofROM);
        return !goodCheck;
    }

    /// <summary>
    /// Check if area of ROM contains the same byte values.
    /// </summary>
    /// <param name="address"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public static bool IsEmpty(int address, int size)
    {
        ChangePos(ref address);
        byte[] b = Read(address, size);
        bool pass = true;
        for (int i = 1, initialByte = b[i++]; i < b.Length && pass == true; i++)
            pass = b[i] == initialByte;
        return pass;
    }

    /// <summary>
    /// Switch ROM data between the specified locations.
    /// </summary>
    /// <param name="address1"></param>
    /// <param name="address2"></param>
    /// <param name="size"></param>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public static void SwapBytes(int address1, int address2, int size)
    {
        for (int i = 0; i < size; i++)
            SwapByte(address1 + i, address2 + i);
    }

    /// <summary>
    /// Switch ROM data between the specified locations.
    /// </summary>
    /// <param name="address1"></param>
    /// <param name="address2"></param>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public static void SwapByte(int address1, int address2)
    {
        ChangePos(ref address1);
        ChangePos(ref address2);
        byte temp = Read(address1);
        WriteToArray(address1, Read(address2));
        WriteToArray(address2, temp);
    }

    /// Superskuj
    ///<Summary>
    /// Receives a rom as a memorystream and checks if it has a copier header.
    /// returns 0 if headerless, 1 if headered
    /// </Summary>
    public static bool IsHeaderless()
    {
        bool isHeaderless = true;
        const string headerCheck = "00000000";
        string arrayString = "";

        /* can't use the read method because we actually
         * want to read the header here.
         */
        byte[] byteArray = new byte[]
        {
                allofROM[0x10],
                allofROM[0x11],
                allofROM[0x12],
                allofROM[0x13]
        };

        for (int i = 0; i < byteArray.Length; i++)
            arrayString += Conversion.byteToHex(byteArray[i]);
        if (arrayString == headerCheck)
            isHeaderless = false;
        return isHeaderless;
    }
}