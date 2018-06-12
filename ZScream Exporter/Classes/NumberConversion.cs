/*
 * Author:  Trovsky 
 */

using System;
using System.Collections.Generic;

/// <summary>
/// Converts various number types. Ex. An ushort to a short.
/// </summary>
public static class Conversion
{
    public static byte[] ToByteArray(List<byte> listy2)
    {
        byte[] b = new byte[listy2.Count];
        int i = 0;
        foreach (byte vv in listy2)
            b[i++] = vv;
        return b;
    }

    //StackOverflow: "Convert Byte Array to Bit Array?"
    public static IEnumerable<bool> GetBits(byte b)
    {
        for (int i = 0; i < 8; i++, b *= 2)
            yield return (b & 0x80) != 0;
    }

    //StackOverflow: "Convert Byte Array to Bit Array?"
    public static IEnumerable<byte> GetBitsAsByte(byte b)
    {
        for (int i = 0; i < 8; i++, b *= 2)
            yield return Convert.ToByte((b & 0x80) != 0);
    }

    public static string byteToHex(byte b)
    { return bytesToHex(new byte[1] { b }); }
    public static string bytesToHex(byte[] ba)
    { return BitConverter.ToString(ba).Replace("-", ""); }
    public static int toInt(byte a, byte b, byte c, byte d = 0)
    { return BitConverter.ToInt32(new byte[] { d, c, b, a }, 0); }

    public static int toInt(byte a, byte b)
    { return toShort(a, b); }

    public static short toShort(byte a, byte b)
    { return BitConverter.ToInt16(new byte[] { b, a }, 0); }

    public static ushort toUShort(byte a, byte b)
    { return BitConverter.ToUInt16(new byte[] { b, a }, 0); }

    public static int toInt(byte[] b)
    {
        int val = -1;
        switch (b.Length)
        {
            case 1:
                val = b[0];
                break;
            case 2:
                val = BitConverter.ToInt16(b, 0); //val = toInt(b[0], b[1]);
                break;
            case 3:
                val = toInt(b[0], b[1], b[2], 0);
                break;
            case 4:
                val = BitConverter.ToInt32(b, 0); //val = toInt(b[0], b[1], b[2], b[3]);
                break;
            default:
                throw new Exception("Invalid size. Int can't be larger than 4 bytes.");
        }
        return val;
    }

    public static List<byte> ToByteArray(List<List<byte>> byteList)
    {
        List<byte> liii = new List<byte>();
        foreach (List<byte> list in byteList)
            foreach (byte b in list)
                liii.Add(b);
        return liii;
    }

    public static string ToString(List<string> s, string sep = "")
    {
        string str = "";
        int i = 0;
        foreach (string ss in s)
            if (i++ != s.Count - 1)
                str += ss + sep;
            else str += ss;
        return str;
    }

    public static string ToHexString(List<short> s, string sep = "", int length = 2)
    {
        string str = "";
        int i = 0;
        foreach (short ss in s)
            if (i++ != s.Count - 1)
                str += DecHexCalc.decToHex(ss, length: 4) + sep;
            else str += ss;
        return str;
    }

    public static string ToHexString(List<int> s, string sep = "", int length = 2)
    {
        string str = "";
        int i = 0;
        foreach (int ss in s)
        {
            str += DecHexCalc.decToHex(ss, length: 4);
            if (i++ != s.Count - 1)
                str += sep;
        }
        return str;
    }

    public static string ToString(List<byte> s)
    {
        string output = "";
        foreach (byte b in s)
            output += b;
        return output;
    }
    public static List<byte> ToByteList(byte[] aa)
    {
        List<byte> ll = new List<byte>();
        foreach (byte b in aa)
            ll.Add(b);
        return ll;
    }

    public static List<string> ToStringList(string[] s)
    {
        List<string> list = new List<string>();
        foreach (string ss in s)
            list.Add(ss);
        return list;

    }

    public static void ToBytes(int i, out byte hi, out byte lo)
    {
        const int max = byte.MaxValue + 1;
        hi = (byte)(i / max);
        lo = (byte)(i % max);
    }


    public static void makeCountEqual(ref List<byte> bytes1, ref List<byte> bytes2, byte empty = 0x00)
    {
        while (bytes1.Count != bytes2.Count)
            if (bytes1.Count > bytes2.Count)
                bytes2.Add(empty);
            else bytes1.Add(empty);
    }

    public static List<byte[]> convertToListOfByteArray(byte[] b, int size)
    {
        List<byte[]> list = new List<byte[]>();
        byte[] temp = new byte[size];
        int i = 0;
        foreach (byte bb in b)
        {
            if (i > size)
            {
                list.Add(temp);
                temp = new byte[size];
            }
            temp[i++] = bb;
        }
        return list;
    }
}

