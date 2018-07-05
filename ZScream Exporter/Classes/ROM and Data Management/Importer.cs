/*
 * Author:  Zarby89
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

/// <summary>
/// 
/// </summary>
public class Importer
{
    private RichTextBox logTextbox;
    private ProgressBar progressBar;
    private Overworld overworld = new Overworld();
    private byte[] romData;

    public Importer(byte[] romData, ProgressBar progressBar, RichTextBox logTextbox)
    {
        this.logTextbox = logTextbox;
        this.progressBar = progressBar;
        this.romData = romData;
        ROM.DATA = romData;
        ROMStructure.loadDefaultProject();
        Import();
    }

    public MapSave[] all_maps = new MapSave[160];
    public EntranceOW[] all_entrancesOW = new EntranceOW[129];
    public void Import()
    {
        RegionId.GenerateRegion();
        ConstantsReader.SetupRegion(RegionId.myRegion, "../../");

        all_maps = new MapSave[160];
        CheckGameTitle();
        LoadOverworldTiles();
        LoadOverworldEntrances();
        progressBar.Value = progressBar.Maximum;
        WriteLog("All 'Overworld' data saved in ROM successfuly.", Color.Green, FontStyle.Bold);

        try
        {
            //GFX.gfxdata = Compression.DecompressTiles();
            SaveFileDialog sf = new SaveFileDialog();
            if (sf.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(sf.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(ROM.DATA, 0, ROM.DATA.Length);
                fs.Close();
            }

        }
        catch (Exception e)
        {
            WriteLog("Error : " + e.Message.ToString(), Color.Red);
            return;
        }
    }

    public byte[] getLargeMaps()
    {
        List<byte> largemaps = new List<byte>();
        for (int i = 0; i < 64; i++)
        {
            if (i > 0)
                if (all_maps[i - 1].largeMap)
                    if (largemaps.Contains((byte)(i - 1)))
                        continue;

            if (i > 7)
                if (all_maps[i - 8].largeMap)
                    if (largemaps.Contains((byte)(i - 8)))
                        continue;

            if (i > 8)
                if (all_maps[i - 9].largeMap)
                    if (largemaps.Contains((byte)(i - 9)))
                        continue;

            if (all_maps[i].largeMap)
                largemaps.Add((byte)i);
        }

        return largemaps.ToArray();
    }

    public void LoadOverworldTiles()
    {
        overworld.AssembleMap16Tiles(true);

        for (int i = 0; i < 160; i++)
        {
            all_maps[i] = JsonConvert.DeserializeObject<MapSave>(File.ReadAllText("ProjectDirectory//Overworld//Maps//Map" + i.ToString("D3") + ".json"));

            overworld.AllMapTilesFromMap(i, all_maps[i].tiles);
            if (i == 159)
            {
                string s = "";
                int tpos = 0;
                for (int y = 0; y < 16; y++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        Tile32 map16 = new Tile32(all_maps[i].tiles[(x * 2), (y * 2)], all_maps[i].tiles[(x * 2) + 1, (y * 2)], all_maps[i].tiles[(x * 2), (y * 2) + 1], all_maps[i].tiles[(x * 2) + 1, (y * 2) + 1]);
                        s += "[" + map16.tile0.ToString("D4") + "," + map16.tile1.ToString("D4") + "," + map16.tile2.ToString("D4") + "," + map16.tile3.ToString("D4") + "] ";
                        tpos++;
                    }
                    s += "\r\n";
                }
                File.WriteAllText("TileDebug2.txt", s);
            }

        }
        byte[] largemaps = getLargeMaps();
        overworld.createMap32TilesFrom16();
        overworld.savemapstorom();


        WriteLog("Overworld tiles data loaded properly", Color.Green);
    }

    public void LoadOverworldEntrances()
    {
        for (int i = 0; i < 129; i++)
        {
            all_entrancesOW[i] = JsonConvert.DeserializeObject<EntranceOW>(File.ReadAllText("ProjectDirectory//Overworld//Entrances//Entrance" + i.ToString("D3") + ".json"));

            ROM.DATA[ConstantsReader.GetAddress("OWEntranceMap") + (i * 2) + 1] = (byte)((all_entrancesOW[i].mapId >> 8) & 0xFF);
            ROM.DATA[ConstantsReader.GetAddress("OWEntranceMap") + (i * 2)] = (byte)((all_entrancesOW[i].mapId) & 0xFF);

            ROM.DATA[ConstantsReader.GetAddress("OWEntrancePos") + (i * 2) + 1] = (byte)((all_entrancesOW[i].mapPos >> 8) & 0xFF);
            ROM.DATA[ConstantsReader.GetAddress("OWEntrancePos") + (i * 2)] = (byte)((all_entrancesOW[i].mapPos) & 0xFF);

            ROM.DATA[ConstantsReader.GetAddress("OWEntranceEntranceId") + i] = (byte)((all_entrancesOW[i].entranceId) & 0xFF);
        }
        WriteLog("Overworld Entrances data loaded properly", Color.Green);
    }

    public void CheckGameTitle()
    {
        RegionId.GenerateRegion();

        string output = "";
        switch (RegionId.myRegion)
        {
            case (int)RegionId.Region.Japan:
                output = "Japan";
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