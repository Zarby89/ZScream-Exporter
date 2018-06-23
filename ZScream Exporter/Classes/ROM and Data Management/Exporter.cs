/*
 * Author: Zarby89
 */

using System;
using System.Drawing;
using System.Windows.Forms;
/// <summary>
/// 
/// </summary>
public class Exporter
{

    private RichTextBox logTextbox;
    private ProgressBar progressBar;
    private Overworld overworld = new Overworld();
    private byte[] romData;
    public Exporter(byte[] romData, ProgressBar progressBar, RichTextBox logTextbox)
    {
        this.logTextbox = logTextbox;
        this.progressBar = progressBar;
        this.romData = romData;
        ROM.DATA = romData;
        ROMStructure.loadDefaultProject();
        Export();
    }

    public RoomSave[] all_rooms = new RoomSave[296];
    public MapSave[] all_maps = new MapSave[160];
    public void Export()
    {
        all_rooms = new RoomSave[296];
        all_maps = new MapSave[160];
        CheckGameTitle();
        progressBar.Value++;
        LoadDungeonsRooms();
        progressBar.Value++;
        LoadOverworldTiles();
        progressBar.Value++;
        LoadOverworldMaps();
        progressBar.Value++;
        TextData.readAllText();
        LoadedProjectStatistics.texts = TextData.messages.Count;
        progressBar.Value++;
        writeLog("All data loaded successfuly.", Color.Green, FontStyle.Bold);

        SaveJson s = new SaveJson(all_rooms, all_maps, null, TextData.messages.ToArray(), overworld);
        progressBar.Value = progressBar.Maximum;
        writeLog("All data exported successfuly.", Color.Green, FontStyle.Bold);
    }

    public void LoadDungeonsRooms()
    {
        int objCount = 0,
            chestCount = 0,
            itemCount = 0,
            blockCount = 0,
            torchCount = 0,
            pitsCount = 0,
            spritesCount = 0,
            roomCount = 0;

        for (int i = 0; i < 296; i++)
        {
            try
            {
                all_rooms[i] = new RoomSave((short)i);
                objCount += all_rooms[i].tilesObjects.Count;
                chestCount += all_rooms[i].chest_list.Count;
                itemCount += all_rooms[i].pot_items.Count;
                blockCount += all_rooms[i].blocks.Count;
                torchCount += all_rooms[i].torches.Count;
                pitsCount += all_rooms[i].damagepit ? 1 : 0;
                spritesCount += all_rooms[i].sprites.Count;
                if (all_rooms[i].tilesObjects.Count != 0)
                {
                    roomCount++;
                }
                if (i == 5)
                {
                    Console.WriteLine(all_rooms[i].tilesObjects.Count);
                }
            }
            catch (Exception e)
            {
                writeLog("Error : " + e.Message.ToString(), Color.Red);
                return;
            }
        }
        LoadedProjectStatistics.blocksRooms = blockCount;
        LoadedProjectStatistics.chestsRooms = chestCount;
        LoadedProjectStatistics.chestsRoomsLength = ((ROM.DATA[Constants.chests_length_pointer + 1] << 8) + (ROM.DATA[Constants.chests_length_pointer])) / 3;
        LoadedProjectStatistics.blocksRoomsLength = ((short)((ROM.DATA[Constants.blocks_length + 1] << 8) + ROM.DATA[Constants.blocks_length])) / 4;
        LoadedProjectStatistics.torchesRoomsLength = 86;//(ROM.DATA[Constants.torches_length_pointer + 1] << 8) + ROM.DATA[Constants.torches_length_pointer];
        LoadedProjectStatistics.entrancesRooms = 132;
        LoadedProjectStatistics.itemsRooms = itemCount;
        LoadedProjectStatistics.pitsRooms = pitsCount;
        LoadedProjectStatistics.pitsRoomsLength = (ROM.DATA[Constants.pit_count] / 2);
        LoadedProjectStatistics.torchesRooms = torchCount;
        LoadedProjectStatistics.usedRooms = roomCount;
        LoadedProjectStatistics.spritesRooms = spritesCount;
        LoadedProjectStatistics.objectsRooms = objCount;
        writeLog("All dungeon rooms data loaded properly : ", Color.Green);
    }

    public void LoadOverworldTiles()
    {
        try
        {
            GFX.gfxdata = Compression.DecompressTiles();
            overworld.AssembleMap16Tiles();
            overworld.AssembleMap32Tiles();

            overworld.DecompressAllMapTiles(); //need to change
            overworld.createMap32TilesFrom16(); //need to change
            writeLog("Overworld tiles data loaded properly", Color.Green);
        }
        catch (Exception e)
        {
            writeLog("Error : " + e.Message.ToString(), Color.Red);
        }
    }

    public void LoadOverworldMaps()
    {
        ushort[,] unusedTiles = new ushort[32, 32];
        int mapCount = 0;
        for (int i = 0; i < 160; i++)
        {
            try
            {
                all_maps[i] = new MapSave((short)i, overworld);
                //TODO: Remove that and find a way to compare the tiles arrays
                if (i >= 131 && i <= 146)
                    continue;
                mapCount++;

            }
            catch (Exception e)
            {
                writeLog("Error : " + e.Message.ToString(), Color.Red);
                return;
            }
        }
        LoadedProjectStatistics.usedMaps = mapCount;
        LoadedProjectStatistics.tiles32Maps = overworld.tiles32count;
        LoadedProjectStatistics.itemsMaps = -1;
        LoadedProjectStatistics.spritesMaps = -1;
        LoadedProjectStatistics.overlaysMaps = -1;
        LoadedProjectStatistics.entrancesMaps = -1;
        LoadedProjectStatistics.exitsMaps = -1;
        LoadedProjectStatistics.holesMaps = -1;
        LoadedProjectStatistics.whirlpoolMaps = -1;
        writeLog("Overworld maps data loaded properly", Color.Green);
    }

    public void CheckGameTitle()
    {
        //Search for "THE LEGEND OF ZELDA" - US 1.2
        if (compareBytes(0x7FC0, new byte[] { 0x54, 0x48, 0x45, 0x20, 0x4C, 0x45, 0x47, 0x45, 0x4E, 0x44, 0x20, 0x4F, 0x46, 0x20, 0x5A, 0x45, 0x4C, 0x44, 0x41 }))
        {
            //US 1.2 detected
            writeLog("Version Detected : US 1.2", Color.Green);
        }
        //Search for "ZELDANODENSETSU" - JP 1.0
        else if (compareBytes(0x7FC0, new byte[] { 0x5A, 0x45, 0x4C, 0x44, 0x41, 0x4E, 0x4F, 0x44, 0x45, 0x4E, 0x53, 0x45, 0x54, 0x53, 0x55 }))
        {
            //JP 1.0 detected
            writeLog("Version Detected : JP 1.0", Color.Green);
            Constants.Init_Jp(); //use JP Constants
        }
        else
        {
            //Unknown Title
            writeLog("Unknown Game Title : Using US 1.2 as default", Color.Orange);
        }
        progressBar.Value++;
    }

    public void writeLog(string line, Color col, FontStyle fs = FontStyle.Regular)
    {
        Font f = new Font(logTextbox.Font, fs);
        string text = line + "\r\n";
        logTextbox.AppendText(text);
        logTextbox.Select((logTextbox.Text.Length - text.Length) + 1, text.Length);
        logTextbox.SelectionColor = col;
        logTextbox.SelectionFont = f;
        logTextbox.Refresh();
    }

    public bool compareBytes(int location, byte[] array)
    {
        for (int i = 0; i < array.Length; i++)
            if (romData[location + i] != array[i])
                return false;
        return true;
    }
}