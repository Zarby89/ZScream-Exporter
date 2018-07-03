/*
 * Author: Zarby89
 */

using System;
using System.Diagnostics;
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
        progressBar.Value = 0;
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Export();
        sw.Stop();
        WriteLog("Json Elapsed Milliseconds : "+ sw.ElapsedMilliseconds.ToString(), Color.DarkRed, FontStyle.Bold);
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
        WriteLog("All data loaded successfuly.", Color.Green, FontStyle.Bold);
        SaveJson s = new SaveJson(all_rooms, all_maps, null, TextData.messages.ToArray(), overworld);
        progressBar.Value = progressBar.Maximum;
        WriteLog("All data exported successfuly.", Color.Green, FontStyle.Bold);
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
                WriteLog("Error : " + e.Message.ToString(), Color.Red);
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
        WriteLog("All dungeon rooms data loaded properly : ", Color.Green);
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
            WriteLog("Overworld tiles data loaded properly", Color.Green);
        }
        catch (Exception e)
        {
            WriteLog("Error : " + e.Message.ToString(), Color.Red);
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
                WriteLog("Error : " + e.Message.ToString(), Color.Red);
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
        WriteLog("Overworld maps data loaded properly", Color.Green);
    }

    public void CheckGameTitle()
    {
        RegionId.GenerateRegion();

        string output = "";
        switch (RegionId.myRegion)
        {
            case (int)RegionId.Region.Japan:
                output = "Japan";
                Constants.Init_Jp();
                goto PrintRegion;
            case (int)RegionId.Region.USA:
                output = "US";
                goto PrintRegion;
            case (int)RegionId.Region.German:
                output = "German";
                goto PrintRegion;
            case (int)RegionId.Region.France:
                output = "France";
                goto PrintRegion;
            case (int)RegionId.Region.Europe:
                output = "Europe";
                goto PrintRegion;
            case (int)RegionId.Region.Canada:
                output = "Canada";
                goto PrintRegion;
            default:
                WriteLog("Unknown Game Title : Using US as default", Color.Orange);
                break;

                PrintRegion:
                WriteLog("Region Detected : " + output, Color.Green);
                break;
        }
        progressBar.Value++;
    }

    public void WriteLog(string line, Color col, FontStyle fs = FontStyle.Regular)
    {
        Font f = new Font(logTextbox.Font, fs);
        string text = line + "\r\n";
        logTextbox.AppendText(text);
        logTextbox.Select((logTextbox.Text.Length - text.Length) + 1, text.Length);
        logTextbox.SelectionColor = col;
        logTextbox.SelectionFont = f;
        logTextbox.Refresh();
    }
}