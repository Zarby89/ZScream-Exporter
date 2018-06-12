/*
 * Author:  Trovsky
 */

using System;

/// <summary>
/// 
/// </summary>
public static class DecHexCalc
{
    private const int two = 2;
    private const string 
        preX = "0x", 
        preSoCash = "$";

    public static string decToHex(int i, int prefix = 0, int length = 2)
    {
        string pre = "";
        if (prefix == 1)
            pre = preX;
        else if (prefix == 2)
            pre = preSoCash;
        return pre + i.ToString("X" + length.ToString());
    }

    public static string decToHex(sbyte i, int prefix = 0, int length = 2)
    { return decToHex(i, prefix, length); }

    public static int hexToDec(string s)
    { return Convert.ToInt32(s, 16); }

    public static byte hexToByte(string s)
    {
        byte b = new byte();
        for (int i = 0; i < s.Length; i += 2)
            b = (byte)hexToDec(s.Substring(i, 2));
        return b;
    }
    public static byte[] hexToBytes(string s)
    {
        byte[] b = new byte[s.Length / 2];
        for (int i = 0; i < s.Length; i += 2)
            b[i / 2] = Convert.ToByte(s.Substring(i, 2), 16);
        return b;
    }

    //Stack overflow: Check a string to see if all characters are hexidemical values
    public static bool isHex(string text)
    { return System.Text.RegularExpressions.Regex.IsMatch(text, @"\A\b[0-9a-fA-F]+\b\Z"); }

    public static string removePrefix(string text)
    { return text.Replace(preX, "").Replace(preSoCash, ""); }

    public static int hasPrefix(string text)
    {
        int val = 0;
        if (text.Contains(preX))
            val = preX.Length;
        else if (text.Contains(preSoCash))
            val = preSoCash.Length;
        return val;
    }
    public static int getValueOfBytes(params byte[] bb)
    { return BitConverter.ToInt16(bb, 0); }

    public static int getValueOfBytes(params int[] bb)
    {
        byte[] b = new byte[bb.Length];
        int i = 0;
        foreach (int by in bb)
        {
            if (by < byte.MaxValue)
                b[i++] = (byte)by;
            else throw new Exception("Value over " + decToHex(byte.MaxValue, 1));
        }
        return BitConverter.ToInt32(b, 0);
    }
}