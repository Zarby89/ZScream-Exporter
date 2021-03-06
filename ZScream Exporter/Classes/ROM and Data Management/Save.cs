﻿/*
 * Author:  Zarby89
 */

using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Writes the ROM's data to JSON files.
/// </summary>
class SaveJson
{
    //ROM.DATA is a base rom loaded to get basic information it can either be JP1.0 or US1.2
    //can still use it for load but must not be used 
    RoomSave[] all_rooms;
    MapSave[] all_maps;
    Entrance[] entrances;
    string[] texts;
    Overworld overworld;
    public SaveJson(RoomSave[] all_rooms, MapSave[] all_maps, Entrance[] entrances, string[] texts, Overworld overworld)
    {
        this.all_rooms = all_rooms;
        this.all_maps = all_maps;
        this.entrances = entrances;
        this.texts = texts;
        this.overworld = overworld;
        //TODO : Change Header location to be dynamic instead of static

        if (!Directory.Exists("ProjectDirectory"))
            Directory.CreateDirectory("ProjectDirectory");
        //ZipArchive zipfile = new ZipArchive(new FileStream("PROJECTFILE.zip", FileMode.Open), ZipArchiveMode.Create);

        const string ProjectDirectorySlash = "ProjectDirectory//";

        File.WriteAllText(ProjectDirectorySlash + "Main.cfg", writeProjectConfig());
        Task t1 = Task.Run(() => { writeRooms(ProjectDirectorySlash); });
        Task t2 = Task.Run(() => { writeEntrances(ProjectDirectorySlash); });
        Task t3 = Task.Run(() => { writeOverworldEntrances(ProjectDirectorySlash); });
        Task t4 = Task.Run(() => { writeOverworldExits(ProjectDirectorySlash); });
        Task t5 = Task.Run(() => { writeOverworldHoles(ProjectDirectorySlash); });
        Task t6 = Task.Run(() => { writeText(ProjectDirectorySlash); });
        Task t7 = Task.Run(() => { writePalettes(ProjectDirectorySlash); });
        Task t8 = Task.Run(() => { writeGfx(ProjectDirectorySlash); });
        Task t9 = Task.Run(() => { writeOverworldTiles16(ProjectDirectorySlash); });
        Task t10 = Task.Run(() => { writeOverworldMaps(ProjectDirectorySlash); });
        Task t11 = Task.Run(() => { writeOverworldConfig(ProjectDirectorySlash); });
        Task t12 = Task.Run(() => { writeOverworldGroups(ProjectDirectorySlash); });
        Task t13 = Task.Run(() => { writeOverworldGroups2(ProjectDirectorySlash); });
        Task t14 = Task.Run(() => { writeOverworldSpriteset(ProjectDirectorySlash); });

        t1.Wait();
        t2.Wait();
        t3.Wait();
        t4.Wait();
        t5.Wait();
        t6.Wait();
        t7.Wait();
        t8.Wait();
        t9.Wait();
        t10.Wait();
        t11.Wait();
        t12.Wait();
        t13.Wait();
        t14.Wait();








    }

    public void writeOverworldGroups(string path)
    {
        if (!Directory.Exists(path + "Overworld"))
            Directory.CreateDirectory(path + "Overworld");

        const int dim = 80 * 4;
        byte[] owblocksetgroups = new byte[dim];
        for (int i = 0; i < dim; i++)
            owblocksetgroups[i] = ROM.DATA[ConstantsReader.GetAddress("overworldgfxGroups") + i];
        File.WriteAllText(path + "Overworld//BlocksetGroups.json", JsonConvert.SerializeObject(owblocksetgroups));
    }

    public void writeOverworldSpriteset(string path)
    {
        if (!Directory.Exists(path + "Overworld"))
            Directory.CreateDirectory(path + "Overworld");

        const int dim = 143 * 4;
        byte[] owblocksetgroups = new byte[dim];
        for (int i = 0; i < dim; i++)
            owblocksetgroups[i] = ROM.DATA[ConstantsReader.GetAddress("sprite_blockset_pointer") + i];
        File.WriteAllText(path + "Overworld//SpritesetGroups.json", JsonConvert.SerializeObject(owblocksetgroups));
    }



    public void writeOverworldGroups2(string path)
    {
        if (!Directory.Exists(path + "Overworld"))
            Directory.CreateDirectory(path + "Overworld");

        const int dim = 80 * 8;
        byte[] owblocksetgroups = new byte[dim];
        for (int i = 0; i < dim; i++)
            owblocksetgroups[i] = ROM.DATA[ConstantsReader.GetAddress("overworldgfxGroups2") + i];
        File.WriteAllText(path + "Overworld//BlocksetGroups2.json", JsonConvert.SerializeObject(owblocksetgroups));
    }

    public void writeOverworldConfig(string path)
    {
        if (!Directory.Exists(path + "Overworld"))
            Directory.CreateDirectory(path + "Overworld");

        byte[] owpalettesgroups = new byte[0xA6];

        OverworldConfig c = new OverworldConfig();

        File.WriteAllText(path + "Overworld//Config.json", JsonConvert.SerializeObject(c));

        Color[] grasscolors = new Color[3];
        grasscolors[0] = c.hardCodedLWGrass;
        grasscolors[1] = c.hardCodedDWGrass;
        grasscolors[2] = c.hardCodedDMGrass;
        File.WriteAllText(path + "Overworld//GrassColors.json", JsonConvert.SerializeObject(grasscolors));
    }

    public void writeOverworldTiles16(string path)
    {
        if (!Directory.Exists(path + "Overworld"))
        {
            Directory.CreateDirectory(path + "Overworld");
        }
        File.WriteAllText(path + "Overworld//Tiles16.json", JsonConvert.SerializeObject(overworld.tiles16));
    }

    public void writeOverworldHoles(string path)
    {
        if (!Directory.Exists(path + "Overworld"))
        {
            Directory.CreateDirectory(path + "Overworld");
        }
        if (!Directory.Exists(path + "Overworld//Holes"))
        {
            Directory.CreateDirectory(path + "Overworld//Holes");
        }
        for (int i = 0; i < 0x13; i++)
        {
            short mapId = (short)((ROM.DATA[ConstantsReader.GetAddress("OWHoleArea") + (i * 2) + 1] << 8) + (ROM.DATA[ConstantsReader.GetAddress("OWHoleArea") + (i * 2)]));
            short mapPos = (short)((ROM.DATA[ConstantsReader.GetAddress("OWHolePos") + (i * 2) + 1] << 8) + (ROM.DATA[ConstantsReader.GetAddress("OWHolePos") + (i * 2)]));
            byte entranceId = (byte)((ROM.DATA[ConstantsReader.GetAddress("OWHoleEntrance") + i]));
            EntranceOW eo = new EntranceOW(mapId, mapPos, entranceId);

            File.WriteAllText(path + "Overworld//Holes//Hole" + i.ToString("D3") + ".json", JsonConvert.SerializeObject(eo));
        }
    }



    public void writeOverworldEntrances(string path)
    {
        if (!Directory.Exists(path + "Overworld"))
        {
            Directory.CreateDirectory(path + "Overworld");
        }
        if (!Directory.Exists(path + "Overworld//Entrances"))
        {
            Directory.CreateDirectory(path + "Overworld//Entrances");
        }
        for (int i = 0; i < 129; i++)
        {
            short mapId = (short)((ROM.DATA[ConstantsReader.GetAddress("OWEntranceMap") + (i * 2) + 1] << 8) + (ROM.DATA[ConstantsReader.GetAddress("OWEntranceMap") + (i * 2)]));
            short mapPos = (short)((ROM.DATA[ConstantsReader.GetAddress("OWEntrancePos") + (i * 2) + 1] << 8) + (ROM.DATA[ConstantsReader.GetAddress("OWEntrancePos") + (i * 2)]));
            byte entranceId = (byte)((ROM.DATA[ConstantsReader.GetAddress("OWEntranceEntranceId") + i]));
            EntranceOW eo = new EntranceOW(mapId, mapPos, entranceId);
            File.WriteAllText(path + "Overworld//Entrances//Entrance" + i.ToString("D3") + ".json", JsonConvert.SerializeObject(eo));
        }
    }

    public void writeOverworldExits(string path)
    {
        if (!Directory.Exists(path + "Overworld"))
        {
            Directory.CreateDirectory(path + "Overworld");
        }
        if (!Directory.Exists(path + "Overworld//Exits"))
        {
            Directory.CreateDirectory(path + "Overworld//Exits");
        }
        for (int i = 0; i < 0x4F; i++)
        {
            short[] e = new short[13];
            e[0] = (short)((ROM.DATA[ConstantsReader.GetAddress("OWExitRoomId") + (i * 2) + 1] << 8) + (ROM.DATA[ConstantsReader.GetAddress("OWExitRoomId") + (i * 2)]));
            e[1] = (byte)((ROM.DATA[ConstantsReader.GetAddress("OWExitMapId") + i]));
            e[2] = (short)((ROM.DATA[ConstantsReader.GetAddress("OWExitVram") + (i * 2) + 1] << 8) + (ROM.DATA[ConstantsReader.GetAddress("OWExitVram") + (i * 2)]));
            e[3] = (short)((ROM.DATA[ConstantsReader.GetAddress("OWExitYScroll") + (i * 2) + 1] << 8) + (ROM.DATA[ConstantsReader.GetAddress("OWExitYScroll") + (i * 2)]));
            e[4] = (short)((ROM.DATA[ConstantsReader.GetAddress("OWExitXScroll") + (i * 2) + 1] << 8) + (ROM.DATA[ConstantsReader.GetAddress("OWExitXScroll") + (i * 2)]));
            e[5] = (short)((ROM.DATA[ConstantsReader.GetAddress("OWExitYPlayer") + (i * 2) + 1] << 8) + (ROM.DATA[ConstantsReader.GetAddress("OWExitYPlayer") + (i * 2)]));
            e[6] = (short)((ROM.DATA[ConstantsReader.GetAddress("OWExitXPlayer") + (i * 2) + 1] << 8) + (ROM.DATA[ConstantsReader.GetAddress("OWExitXPlayer") + (i * 2)]));
            e[7] = (short)((ROM.DATA[ConstantsReader.GetAddress("OWExitYCamera") + (i * 2) + 1] << 8) + (ROM.DATA[ConstantsReader.GetAddress("OWExitYCamera") + (i * 2)]));
            e[8] = (short)((ROM.DATA[ConstantsReader.GetAddress("OWExitXCamera") + (i * 2) + 1] << 8) + (ROM.DATA[ConstantsReader.GetAddress("OWExitXCamera") + (i * 2)]));
            e[9] = (byte)((ROM.DATA[ConstantsReader.GetAddress("OWExitUnk1") + i]));
            e[10] = (byte)((ROM.DATA[ConstantsReader.GetAddress("OWExitUnk2") + i]));
            e[11] = (byte)((ROM.DATA[ConstantsReader.GetAddress("OWExitDoorType1") + i]));
            e[12] = (byte)((ROM.DATA[ConstantsReader.GetAddress("OWExitDoorType2") + i]));
            ExitOW eo = (new ExitOW(e[0], (byte)e[1], e[2], e[3], e[4], e[5], e[6], e[7], e[8], (byte)e[9], (byte)e[10], (byte)e[11], (byte)e[12]));
            File.WriteAllText(path + "Overworld//Exits//Exit" + i.ToString("D3") + ".json", JsonConvert.SerializeObject(eo));
        }
    }

    public void writeOverworldMaps(string path)
    {
        if (!Directory.Exists(path + "Overworld"))
        {
            Directory.CreateDirectory(path + "Overworld");
        }
        if (!Directory.Exists(path + "Overworld//Maps"))
        {
            Directory.CreateDirectory(path + "Overworld//Maps");
        }
        for (int i = 0; i < 160; i++)
        {

            File.WriteAllText(path + "Overworld//Maps//Map" + i.ToString("D3") + ".json", JsonConvert.SerializeObject(all_maps[i]));
        }
    }

    public void writeGfx(string path)
    {
        if (!Directory.Exists(path + "Graphics"))
        {
            Directory.CreateDirectory(path + "Graphics");
        }

        for (int i = 0; i < 223; i++)
        {
            GFX.singleGrayscaletobmp(i).Save(path + "Graphics//" + i.ToString("D3") + ".png");
        }
    }

    public void writeText(string path)
    {
        if (!Directory.Exists(path + "Texts"))
        {
            Directory.CreateDirectory(path + "Texts");
        }

        File.WriteAllText(path + "Texts//AllTexts.json", JsonConvert.SerializeObject(TextData.messages.ToArray()));
    }



    public void writePalettes(string path)
    {
        //save them into yy-chr format

        //:thinking:


        //Separating palettes

        //DD218-DD290 lightworld sprites palettes (15*4)

        writePalette(0xDD218, 15, 4, path, "Sprites Palettes", "Lightworld Sprites");

        //DD291-DD308 darkworld sprites palettes (15*4)

        writePalette(0xDD290, 15, 4, path, "Sprites Palettes", "Darkworld Sprites");

        //DD309-DD39D Armors Palettes (15*5)
        writePalette(0xDD308, 15, 5, path, "Link Palettes", "Mails");

        //DD39E-DD445 Spr Aux Palettes? (7*12)
        writePalette(0xDD39E, 7, 12, path, "Sprites Palettes", "Aux Sprites1");

        //DD446-DD4DF Spr Aux2 Palettes? (7*11)
        writePalette(0xDD446, 7, 11, path, "Sprites Palettes", "Aux Sprites2");

        //DD4E0-DD62F Spr Aux Palettes? (7*24)
        writePalette(0xDD4E0, 7, 24, path, "Sprites Palettes", "Aux Sprites3");

        //DD630-DD647 Sword Palettes (3*4)
        writePalette(0xDD39E, 3, 4, path, "Link Palettes", "Sword Sprites");

        //DD648-DD65F Shield Palettes (4*3)
        writePalette(0xDD648, 4, 3, path, "Link Palettes", "Shield Sprites");

        //DD660-DD69F Hud Palettes (4*8)
        writePalette(0xDD660, 4, 8, path, "Hud Palettes", "Hud1");

        //DD6A0-DD6DF Hud Palettes2 (4*8)
        writePalette(0xDD6A0, 4, 8, path, "Hud Palettes", "Hud2");

        //DD6E0-DD709 Unused Palettes (7*3) ?
        writePalette(0xDD6E0, 7, 3, path, "Unused Palettes", "Unused");

        //DD70A-DD733 Map Sprites Palettes (7*3)
        writePalette(0xDD70A, 7, 3, path, "Dungeon Map Palette", "Map Sprite");

        //DD734 Dungeons Palettes :scream: (15*6) * 19
        for (int i = 0; i < 19; i++)
        {
            writePalette(0xDD734 + (i * 180), 15, 6, path, "Dungeon Palette", "Dungeon " + i.ToString("D2"));
        }
        //DE544-DE603 Map bg palette (15*6)
        writePalette(0xDE544, 15, 6, path, "Dungeon Map Palette", "Map Bg");

        //DE604-DE6C7 overworld Aux Palettes (7*14)
        writePalette(0xDE604, 7, 14, path, "Overworld Palette", "Overworld Animated");

        //DE6C8 Main Overworld Palettes (7*5) * 6
        for (int i = 0; i < 6; i++)
        {
            writePalette(0xDE6C8 + (i * 70), 7, 5, path, "Overworld Palette", "Main Overworld " + i.ToString("D2"));
        }

        //DE86C Overworld Aux Palettes (7*3) * 20
        for (int i = 0; i < 20; i++)
        {
            writePalette(0xDE86C + (i * 42), 7, 3, path, "Overworld Palette", "Overworld Aux2 " + i.ToString("D2"));
        }

        //save them in .png format 8x8 for each colors
    }

    public void writePalette(int palettePos, int w, int h, string path, string dir, string name)
    {

        if (!Directory.Exists(path + "Palettes//" + dir))
            Directory.CreateDirectory(path + "Palettes//" + dir);

        //Bitmap paletteBitmap = new Bitmap(w * 8, h * 8);
        Color[] palettes = new Color[h * w];
        int pos = palettePos;
        int ppos = 0;
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {

                palettes[ppos] = GFX.getColor((short)((ROM.DATA[pos + 1] << 8) + ROM.DATA[pos]));
                //Graphics g = Graphics.FromImage(paletteBitmap);
                //g.FillRectangle(new SolidBrush(c), new Rectangle(x * 8, y * 8, 8, 8));
                pos += 2;
                ppos++;
            }
        }
        File.WriteAllText(path + "Palettes//" + dir + "//" + name + ".json", JsonConvert.SerializeObject(palettes));
        /*
        //path = ProjectDirectory//
        paletteBitmap.Save(path + "Palettes//" + dir + "//" + name + ".png");

        paletteBitmap.Dispose();*/


    }

    public string writeProjectConfig()
    {
        configSave cs = new configSave();
        return JsonConvert.SerializeObject(cs);
    }

    public void writeEntrances(string path)
    {
        if (!Directory.Exists(path + "Dungeons"))
            Directory.CreateDirectory(path + "Dungeons");
        if (!Directory.Exists(path + "Dungeons//Entrances"))
            Directory.CreateDirectory(path + "Dungeons//Entrances");
        if (!Directory.Exists(path + "Dungeons//StartingEntrances"))
            Directory.CreateDirectory(path + "Dungeons//StartingEntrances");

        for (int i = 0; i < 133; i++)
        {
            Entrance e = new Entrance((byte)i);
            File.WriteAllText(path + "Dungeons//Entrances//Entrance " + i.ToString("D3") + ".json", JsonConvert.SerializeObject(e, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));

        }
        for (int i = 0; i < 7; i++)
        {
            Entrance e = new Entrance((byte)i, true);
            File.WriteAllText(path + "Dungeons//StartingEntrances//Entrance " + i.ToString("D3") + ".json", JsonConvert.SerializeObject(e, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));

        }


    }

    public void writeRooms(string path)
    {
        if (!Directory.Exists(path + "Dungeons"))
        {
            Directory.CreateDirectory(path + "Dungeons");
        }
        if (!Directory.Exists(path + "Dungeons//Rooms"))
        {
            Directory.CreateDirectory(path + "Dungeons//Rooms");
        }
        for (int i = 0; i < 296; i++)
        {
            RoomSave rs = new RoomSave((short)i);
            File.WriteAllText(path + "Dungeons//Rooms//Room " + i.ToString("D3") + ".json", JsonConvert.SerializeObject(rs, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));

        }
    }

    public int getLongPointerSnestoPc(int pos)
    {
        int p = (ROM.DATA[pos + 2] << 16) + (ROM.DATA[pos + 1] << 8) + (ROM.DATA[pos]);
        return (Addresses.snestopc(p));
    }
}

public class TextSave
{
    public string[] all_texts;
    public TextSave(string[] all_texts)
    {
        this.all_texts = all_texts;
    }
}

//Rooms, Pots, Chests, Sprites, Headers, Done !
public class roomPotSave
{
    public byte x, y, id;
    public bool bg2 = false;
    public roomPotSave(byte id, byte x, byte y, bool bg2)
    {
        this.id = id;
        this.x = x;
        this.y = y;
        this.bg2 = bg2;
    }
}



public class configSave
{
    public string ProjectName = "";
    public string ProjectVersion = "";
    public string[] allDungeons = new string[17];
    public DataRoom[] allrooms = new DataRoom[296];
    public string[] allMapsNames = new string[160];

    public configSave()
    {
        ProjectName = "Test Name";
        ProjectVersion = "V1.0";

        allDungeons = ROMStructure.dungeonsNames;
        DataRoom[] dr = ROMStructure.dungeonsRoomList
        .Where(x => x != null)
        .OrderBy(x => x.id)
        .Select(x => x) //?
        .ToArray();
        allrooms = dr;
    }


}

public struct MapSave
{
    public ushort[,] tiles; //all map tiles (short values) 0 to 1024 from left to right
    public bool largeMap;
    public byte spriteset;
    public short index;
    public byte palette;
    public byte sprite_palette;
    public byte blockset;
    public short msgid;
    public string name;
    public List<Room_Sprite> sprites;
    public List<roomPotSave> items;
    public MapSave(short id, Overworld overworld)
    {
        tiles = new ushort[32, 32];
        largeMap = false;
        this.index = id;
        this.palette = (byte)(ROM.DATA[ConstantsReader.GetAddress("overworldMapPalette") + index] << 2);
        this.blockset = ROM.DATA[ConstantsReader.GetAddress("mapGfx") + index];
        this.sprite_palette = (byte)(ROM.DATA[ConstantsReader.GetAddress("overworldSpritePalette") + index]);
        this.msgid = (short)((ROM.DATA[(ConstantsReader.GetAddress("overworldMessages") + index * 2) + 1] << 8) + ROM.DATA[(ConstantsReader.GetAddress("overworldMessages") + index * 2)]);
        if (index != 0x80)
            if (index <= 150)
                if (ROM.DATA[ConstantsReader.GetAddress("overworldMapSize") + (index & 0x3F)] != 0)
                    largeMap = true;

        this.spriteset = ROM.DATA[ConstantsReader.GetAddress("overworldSpriteset") + index];
        this.name = ROMStructure.mapsNames[index];

        sprites = new List<Room_Sprite>();
        int address = 0;
        if (index < 0x40)
        {
            address = ConstantsReader.GetAddress("overworldSpritesLW");
            }
        else
        {
            address = ConstantsReader.GetAddress("overworldSpritesDW");
            }
        //09 bank ? Need to check if HM change that
        int sprite_address_snes = (09 << 16) +
        (ROM.DATA[address + (index * 2) + 1] << 8) +
        ROM.DATA[address + (index * 2)];
        int sprite_address = Addresses.snestopc(sprite_address_snes);

        while (true)
        {
            byte b1 = ROM.DATA[sprite_address];
            byte b2 = ROM.DATA[sprite_address + 1];
            byte b3 = ROM.DATA[sprite_address + 2];

            if (b1 == 0xFF) { break; }

            sprites.Add(new Room_Sprite(b3, (byte)(b2 & 0x3F), (byte)(b1 & 0x3F), Sprites_Names.name[b3], 0, 0, 0, 0));
            sprite_address += 3;
        }


        items = new List<roomPotSave>();

        int addr = (ConstantsReader.GetAddress("overworldItemsBank") << 16) +
                    (ROM.DATA[ConstantsReader.GetAddress("overworldItemsPointers") + (index * 2) + 1] << 8) +
                    (ROM.DATA[ConstantsReader.GetAddress("overworldItemsPointers") + (index * 2)]);

        addr = Addresses.snestopc(addr);

        while (true)
        {
            byte b1 = ROM.DATA[addr];
            byte b2 = ROM.DATA[addr + 1];
            byte b3 = ROM.DATA[addr + 2];
            if (b1 == 0xFF && b2 == 0xFF)
            {
                break;
            }

            int p = (((b2 & 0x1F) << 8) + b1) >> 1;

            int x = p % 64;
            int y = p >> 6;

            items.Add(new roomPotSave(b3, (byte)x, (byte)y, false));
            addr += 3;
        }


        int t = index * 256;
        for (int y = 0; y < 16; y++)
        {
            for (int x = 0; x < 16; x++)
            {
                tiles[(x * 2), (y * 2)] = overworld.map16tiles[t].tile0;
                tiles[(x * 2) + 1, (y * 2)] = overworld.map16tiles[t].tile1;
                tiles[(x * 2), (y * 2) + 1] = overworld.map16tiles[t].tile2;
                tiles[(x * 2) + 1, (y * 2) + 1] = overworld.map16tiles[t].tile3;
                t++;
            }
        }
    }
}



public class OverworldConfig
{
    public Color hardCodedDWGrass;
    public Color hardCodedLWGrass;
    public Color hardCodedDMGrass;

    public OverworldConfig()
    {
        hardCodedDWGrass = GFX.getColor((short)((ROM.DATA[ConstantsReader.GetAddress("hardcodedGrassDW") + 1] << 8) + ROM.DATA[ConstantsReader.GetAddress("hardcodedGrassDW")]));
        hardCodedLWGrass = GFX.getColor((short)((ROM.DATA[ConstantsReader.GetAddress("hardcodedGrassLW") + 1] << 8) + ROM.DATA[ConstantsReader.GetAddress("hardcodedGrassLW")]));
        hardCodedDMGrass = GFX.getColor((short)((ROM.DATA[ConstantsReader.GetAddress("hardcodedGrassSpecial") + 1] << 8) + ROM.DATA[ConstantsReader.GetAddress("hardcodedGrassSpecial")]));
    }
}

public class PaletteConfig
{ byte[] owpalgroup1 = new byte[0xA6]; }