/*
 * Author:  Zarby89, Trovsky (cleanup)
 */
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using ZScream_Exporter.ZCompressLibrary;

/// <summary>
/// 
/// </summary>
public class Overworld
{
    public List<Tile16> tiles16;
    public List<Tile32> tiles32;
    private int[] map32address;

    public Tile32[] map16tiles;
    public List<Size> posSize;

    public Tile32[] t32Unique;
    public List<ushort> t32;

    public Overworld()
    {
        tiles16 = new List<Tile16>();
        tiles32 = new List<Tile32>();

        map16tiles = new Tile32[40960];
        posSize = new List<Size>();

        t32Unique = new Tile32[10000];
        t32 = new List<ushort>();
    }
    
    public void AssembleMap16Tiles(bool fromJson = false)
    {
        if (!fromJson)
        {
            int tpos = ConstantsReader.GetAddress("map16Tiles");
            for (int i = 0; i < 3760; i += 1)
            {
                Tile8 t0 = new Tile8(BitConverter.ToInt16(ROM.DATA, (tpos)));
                tpos += 2;
                Tile8 t1 = new Tile8(BitConverter.ToInt16(ROM.DATA, (tpos)));
                tpos += 2;
                Tile8 t2 = new Tile8(BitConverter.ToInt16(ROM.DATA, (tpos)));
                tpos += 2;
                Tile8 t3 = new Tile8(BitConverter.ToInt16(ROM.DATA, (tpos)));
                tpos += 2;
                tiles16.Add(new Tile16(t0, t1, t2, t3));
            }
        }
        else tiles16 = JsonConvert.DeserializeObject<Tile16[]>(File.ReadAllText("ProjectDirectory//Overworld//Tiles16.json")).ToList();
    }

    public void AssembleMap32Tiles()
    {
        map32address = new int[]
        {
            ConstantsReader.GetAddress("map32TilesTL"),
            ConstantsReader.GetAddress("map32TilesTR"),
            ConstantsReader.GetAddress("map32TilesBL"),
            ConstantsReader.GetAddress("map32TilesBR")
        };

        const int dim = 4;

        for (int i = 0; i < 0x33F0; i += 6)
        {
            ushort[,] b = new ushort[dim, dim];
            ushort tl, tr, bl, br;
            for (int k = 0; k < 4; k++)
            {
                tl = generate(i, k, (int)Dimension.map32TilesTL);
                tr = generate(i, k, (int)Dimension.map32TilesTR);
                bl = generate(i, k, (int)Dimension.map32TilesBL);
                br = generate(i, k, (int)Dimension.map32TilesBR);
                tiles32.Add(new Tile32(tl, tr, bl, br));
            }
        }
    }

    private enum Dimension
    {
        map32TilesTL = 0,
        map32TilesTR = 1,
        map32TilesBL = 2,
        map32TilesBR = 3
    }

    private ushort generate(int i, int k, int dimension)
    {
        return (ushort)(ROM.DATA[map32address[dimension] + k + (i)]
            + (((ROM.DATA[map32address[dimension] + (i) + (k <= 1 ? 4 : 5)] >> (k % 2 == 0 ? 4 : 0)) & 0x0F) * 256));
    }

    public void DecompressAllMapTiles()
    {
        //locat functions
        int genPointer(int address, int i) => PointerRead.LongRead_LoHiBank(address + i * 3);
        byte[] Decomp(int pointer, ref int compressedSize)
            => Decompress.ALTTPDecompressOverworld(ROM.DATA, pointer, 1000, ref compressedSize);

        int npos = 0;
        for (int i = 0; i < 160; i++)
        {
            int p1 = genPointer(ConstantsReader.GetAddress("compressedAllMap32PointersHigh"), i),
                p2 = genPointer(ConstantsReader.GetAddress("compressedAllMap32PointersLow"), i);

            int ttpos = 0, compressedSize1 = 0, compressedSize2 = 0;

            byte[]
                bytes = Decomp(p2, ref compressedSize1),
                bytes2 = Decomp(p1, ref compressedSize2);

            for (int y = 0; y < 16; y++)
                for (int x = 0, tpos; x < 16; x++, npos++, ttpos++)
                {
                    tpos = (ushort)((bytes2[ttpos] << 8) + bytes[ttpos]);
                    if (tpos < tiles32.Count)
                        map16tiles[npos] = new Tile32(tiles32[tpos].tile0, tiles32[tpos].tile1, tiles32[tpos].tile2, tiles32[tpos].tile3);
                    else
                    {
                        Console.WriteLine("Found 0,0,0,0");
                        map16tiles[npos] = new Tile32(0, 0, 0, 0);
                    }
                }
        }
    }

    public int tiles32count = 0;
    public void createMap32TilesFrom16()
    {
        t32.Clear();
        t32Unique = new Tile32[10000];
        tiles32count = 0;
        //40960 = numbers of 32x32 tiles 

        const int nullVal = -1;
        for (int i = 0; i < 40960; i++)
        {
            short foundIndex = nullVal;
            for (int j = 0; j < tiles32count; j++)
                if (t32Unique[j].tile0 == map16tiles[i].tile0)
                    if (t32Unique[j].tile1 == map16tiles[i].tile1)
                        if (t32Unique[j].tile2 == map16tiles[i].tile2)
                            if (t32Unique[j].tile3 == map16tiles[i].tile3)
                            {
                                foundIndex = (short)j;
                                break;
                            }


            if (foundIndex == nullVal)
            {
                t32Unique[tiles32count] = new Tile32(map16tiles[i].tile0, map16tiles[i].tile1, map16tiles[i].tile2, map16tiles[i].tile3);
                t32.Add((ushort)tiles32count);
                tiles32count++;
            }
            else t32.Add((ushort)foundIndex);
        }

        Console.WriteLine("Nbr of tiles32 = " + tiles32count);
    }

    //UNUSED CODE
    public void AllMapTilesFromMap(int mapid, ushort[,] tiles, bool large = false)
    {

        string s = "";
        int tpos = mapid * 256;
        for (int y = 0; y < 16; y++)
        {
            for (int x = 0; x < 16; x++)
            {
                map16tiles[tpos] = new Tile32(tiles[(x * 2), (y * 2)], tiles[(x * 2) + 1, (y * 2)], tiles[(x * 2), (y * 2) + 1], tiles[(x * 2) + 1, (y * 2) + 1]);
                s += "[" + map16tiles[tpos].tile0.ToString("D4") + "," + map16tiles[tpos].tile1.ToString("D4") + "," + map16tiles[tpos].tile2.ToString("D4") + "," + map16tiles[tpos].tile3.ToString("D4") + "] ";
                tpos++;

            }
            s += "\r\n";
        }
        File.WriteAllText("TileDebug.txt", s);

    }

    public void Save32Tiles()
    {
        int index = 0;
        int c = tiles32count + 8; //Need to compare US and JP, JP Limit is 33C0, US is 33F0
        for (int i = 0; i < c - 4; i += 6)
        {
            if (i >= 0x33F0)
            {
                Console.WriteLine("Too Many Unique Tiles !");
                break;
            }

            //Top Left
            ROM.DATA[ConstantsReader.GetAddress("map32TilesTL") + (i + 0)] = (byte)(t32Unique[index + 0].tile0 & 0xFF);
            ROM.DATA[ConstantsReader.GetAddress("map32TilesTL") + (i + 1)] = (byte)(t32Unique[index + 1].tile0 & 0xFF);
            ROM.DATA[ConstantsReader.GetAddress("map32TilesTL") + (i + 2)] = (byte)(t32Unique[index + 2].tile0 & 0xFF);
            ROM.DATA[ConstantsReader.GetAddress("map32TilesTL") + (i + 3)] = (byte)(t32Unique[index + 3].tile0 & 0xFF);

            ROM.DATA[ConstantsReader.GetAddress("map32TilesTL") + (i + 4)] = (byte)(((t32Unique[index].tile0 >> 4) & 0xF0) + ((t32Unique[index + 1].tile0 >> 8) & 0x0F));
            ROM.DATA[ConstantsReader.GetAddress("map32TilesTL") + (i + 5)] = (byte)(((t32Unique[index + 2].tile0 >> 4) & 0xF0) + ((t32Unique[index + 3].tile0 >> 8) & 0x0F));

            //Top Right
            ROM.DATA[ConstantsReader.GetAddress("map32TilesTR") + (i)] = (byte)(t32Unique[index].tile1 & 0xFF);
            ROM.DATA[ConstantsReader.GetAddress("map32TilesTR") + (i + 1)] = (byte)(t32Unique[index + 1].tile1 & 0xFF);
            ROM.DATA[ConstantsReader.GetAddress("map32TilesTR") + (i + 2)] = (byte)(t32Unique[index + 2].tile1 & 0xFF);
            ROM.DATA[ConstantsReader.GetAddress("map32TilesTR") + (i + 3)] = (byte)(t32Unique[index + 3].tile1 & 0xFF);

            ROM.DATA[ConstantsReader.GetAddress("map32TilesTR") + (i + 4)] = (byte)(((t32Unique[index].tile1 >> 4) & 0xF0) | ((t32Unique[index + 1].tile1 >> 8) & 0x0F));
            ROM.DATA[ConstantsReader.GetAddress("map32TilesTR") + (i + 5)] = (byte)(((t32Unique[index + 2].tile1 >> 4) & 0xF0) | ((t32Unique[index + 3].tile1 >> 8) & 0x0F));

            //Bottom Left
            ROM.DATA[ConstantsReader.GetAddress("map32TilesBL") + (i)] = (byte)(t32Unique[index].tile2 & 0xFF);
            ROM.DATA[ConstantsReader.GetAddress("map32TilesBL") + (i + 1)] = (byte)(t32Unique[index + 1].tile2 & 0xFF);
            ROM.DATA[ConstantsReader.GetAddress("map32TilesBL") + (i + 2)] = (byte)(t32Unique[index + 2].tile2 & 0xFF);
            ROM.DATA[ConstantsReader.GetAddress("map32TilesBL") + (i + 3)] = (byte)(t32Unique[index + 3].tile2 & 0xFF);

            ROM.DATA[ConstantsReader.GetAddress("map32TilesBL") + (i + 4)] = (byte)(((t32Unique[index].tile2 >> 4) & 0xF0) | ((t32Unique[index + 1].tile2 >> 8) & 0x0F));
            ROM.DATA[ConstantsReader.GetAddress("map32TilesBL") + (i + 5)] = (byte)(((t32Unique[index + 2].tile2 >> 4) & 0xF0) | ((t32Unique[index + 3].tile2 >> 8) & 0x0F));

            //Bottom Right
            ROM.DATA[ConstantsReader.GetAddress("map32TilesBR") + (i)] = (byte)(t32Unique[index].tile3 & 0xFF);
            ROM.DATA[ConstantsReader.GetAddress("map32TilesBR") + (i + 1)] = (byte)(t32Unique[index + 1].tile3 & 0xFF);
            ROM.DATA[ConstantsReader.GetAddress("map32TilesBR") + (i + 2)] = (byte)(t32Unique[index + 2].tile3 & 0xFF);
            ROM.DATA[ConstantsReader.GetAddress("map32TilesBR") + (i + 3)] = (byte)(t32Unique[index + 3].tile3 & 0xFF);

            ROM.DATA[ConstantsReader.GetAddress("map32TilesBR") + (i + 4)] = (byte)(((t32Unique[index].tile3 >> 4) & 0xF0) | ((t32Unique[index + 1].tile3 >> 8) & 0x0F));
            ROM.DATA[ConstantsReader.GetAddress("map32TilesBR") + (i + 5)] = (byte)(((t32Unique[index + 2].tile3 >> 4) & 0xF0) | ((t32Unique[index + 3].tile3 >> 8) & 0x0F));

            index += 4;
            c += 2;
        }
    }

    public void savemapstorom()
    {
        int pos = 0x1A0000;
        for (int i = 0; i < 160; i++)
        {
            int npos = 0;
            byte[]
                singlemap1 = new byte[256],
                singlemap2 = new byte[256];
            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    singlemap1[npos] = (byte)(t32[npos + (i * 256)] & 0xFF);
                    singlemap2[npos] = (byte)((t32[npos + (i * 256)] >> 8) & 0xFF);
                    npos++;
                }
            }

            int snesPos = Addresses.pctosnes(pos);
            ROM.DATA[(ConstantsReader.GetAddress("compressedAllMap32PointersHigh")) + 0 + (int)(3 * i)] = (byte)(snesPos & 0xFF);
            ROM.DATA[(ConstantsReader.GetAddress("compressedAllMap32PointersHigh")) + 1 + (int)(3 * i)] = (byte)((snesPos >> 8) & 0xFF);
            ROM.DATA[(ConstantsReader.GetAddress("compressedAllMap32PointersHigh")) + 2 + (int)(3 * i)] = (byte)((snesPos >> 16) & 0xFF);

            ROM.DATA[pos] = 0xE0;
            ROM.DATA[pos + 1] = 0xFF;
            pos += 2;
            for (int j = 0; j < 256; j++)
            {
                ROM.DATA[pos] = singlemap2[j];
                pos += 1;
            }
            ROM.DATA[pos] = 0xFF;
            pos += 1;
            snesPos = Addresses.pctosnes(pos);
            ROM.DATA[(ConstantsReader.GetAddress("compressedAllMap32PointersLow")) + 0 + (int)(3 * i)] = (byte)((snesPos >> 00) & 0xFF);
            ROM.DATA[(ConstantsReader.GetAddress("compressedAllMap32PointersLow")) + 1 + (int)(3 * i)] = (byte)((snesPos >> 08) & 0xFF);
            ROM.DATA[(ConstantsReader.GetAddress("compressedAllMap32PointersLow")) + 2 + (int)(3 * i)] = (byte)((snesPos >> 16) & 0xFF);

            ROM.DATA[pos] = 0xE0;
            ROM.DATA[pos + 1] = 0xFF;
            pos += 2;
            for (int j = 0; j < 256; j++)
            {
                ROM.DATA[pos] = singlemap1[j];
                pos += 1;
            }
            ROM.DATA[pos] = 0xFF;
            pos += 1;

        }
        Save32Tiles();
    }
}