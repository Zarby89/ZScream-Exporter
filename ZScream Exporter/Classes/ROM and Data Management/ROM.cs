/*
 * Author:  Zarby89, Trovsky
 */

/// <summary>
/// Stores the ROM byte array and has a few methods.
/// </summary>
public static class ROM
{
    public static byte[] DATA;

    public static void SetRom(byte[] temp, out bool isHeadered)
    {
        isHeadered = false;
        DATA = new byte[temp.Length];

        //is ROM Headered?
        if ((temp.Length & 0x200) == 0x200)
        {
            //Rom is headered, remove header
            DATA = new byte[temp.Length - 0x200];
            for (int i = 0x200; i < temp.Length; i++)
                DATA[i - 0x200] = temp[i];
            isHeadered = true;
        }
        else DATA = (byte[])temp.Clone();
    }
    public static bool CompareBytes(int location, byte[] array)
    {
        for (int i = 0; i < array.Length; i++)
            if (DATA[location + i] != array[i])
                return false;
        return true;
    }
}