/*
 * Author:  Trovsky
 */

using System;
using System.Linq;

/// <summary>
/// Identifies what region the ROM is.
/// </summary>
public static class RegionId
{
    /// <summary>
    /// Stored the region as an int. Use the region enum to interpret the int value.
    /// </summary>
    public static int myRegion;

    /// <summary>
    /// Public enum for the region.
    /// </summary>
    public enum Region
    {
        Invalid =-1,
        Japan = 0,
        USA = 1,
        German,
        France,
        Europe,
        Canada
    }

    private static byte[] dialogueCode = new byte[] { 0xF3, 0x68, 0xC8, 0x28, 0x7F, 0x38 }; //F368C8287F38


    private static int[] location = new int[]
    {
        0x77D30,
        0x75383,
        0x6678B,
        0x6676B,
        0x753A6,
        0x670FB
    };

    /// <summary>
    /// Determine the ROM region. The result is stored in the variable "myRegion".
    /// </summary>
    public static void GenerateRegion()
    {

        /*
         * Each region shares the same code for dialogue, but in each region
         * the location is different! So, here we check to see where
         * the code located.
         * 
         * The game will turn up as invalid if the dialogue routine is changed.
         * Conker's High Rule Tail is one example.
         */

        myRegion = (int)Region.Invalid;
        for (int i = 0; i < dialogueCode.Length; i++)
        {
            byte[] b = new byte[dialogueCode.Length];
            Array.Copy(ROM.DATA, location[i], b, 0, dialogueCode.Length);

            if (b.SequenceEqual(dialogueCode))
            {
                //The region was found! Assign the variable and exit.
                myRegion = i;
                break;
            }
        }
    }
}