/*
 * Author:  Trovsky
 */

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
    public enum region
    {
        Invalid = -1,
        Japan,
        USA,
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
    public static void generateRegion()
    {

        /*
         * Each region shares the same code for dialogue, but in each region
         * the location is different! So, here we check to see where
         * the code located.
         * 
         * The game will turn up as invalid if the dialogue routine is changed.
         * Conker's High Rule Tail is one example.
         */

        myRegion = (int)region.Invalid;
        for (int i = 0; i < dialogueCode.Length; i++)
        {
            byte[] b = RomIO.read(location[i], dialogueCode.Length);
            if (b.SequenceEqual(dialogueCode))
            {
                //The region was found! Assign the variable and exit.
                myRegion = i;
                break;
            }
        }
        
        //if (myRegion == (int)region.Invalid)
            //throw new Exception();
    }
}
