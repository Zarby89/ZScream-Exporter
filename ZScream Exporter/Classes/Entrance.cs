﻿/*
 * Author:  Zarby89
 */

 /// <summary>
 /// 
 /// </summary>
public struct Entrance //can be used for starting entrance as well
{
    public short room;//word value for each room

    //Missing Values : 
    public byte scrolledge_HU;//8 bytes per room, HU, FU, HD, FD, HL, FL, HR, FR
    public byte scrolledge_FU;
    public byte scrolledge_HD;
    public byte scrolledge_FD;
    public byte scrolledge_HL;
    public byte scrolledge_FL;
    public byte scrolledge_HR;
    public byte scrolledge_FR;
    public short yscroll;//2bytes each room
    public short xscroll; //2bytes
    public short yposition;//2bytes
    public short xposition;//2bytes
    public short ycamera;//2bytes
    public short xcamera;//2bytes
    public byte blockset; //1byte
    public byte floor; //1byte
    public byte dungeon; //1byte (dungeon id) //Same as music might use the project dungeon name instead
    public byte door; //1byte
    public byte ladderbg; ////1 byte, ---b ---a b = bg2, a = need to check -_-
    public byte scrolling;////1byte --h- --v- 
    public byte scrollquadrant; //1byte
    public short exit;//2byte word 
    public byte music; //1byte //Will need to be renamed and changed to add support to MSU1

    public Entrance(byte entranceId, bool startingEntrance = false)
    {
        room = (short)((ROM.DATA[(Constants.entrance_room + (entranceId * 2)) + 1] << 8) + ROM.DATA[Constants.entrance_room + (entranceId * 2)]);
        yposition = (short)(((ROM.DATA[(Constants.entrance_yposition + (entranceId * 2)) + 1]) << 8) + ROM.DATA[Constants.entrance_yposition + (entranceId * 2)]);
        xposition = (short)(((ROM.DATA[(Constants.entrance_xposition + (entranceId * 2)) + 1]) << 8) + ROM.DATA[Constants.entrance_xposition + (entranceId * 2)]);
        xscroll = (short)(((ROM.DATA[(Constants.entrance_xscroll + (entranceId * 2)) + 1]) << 8) + ROM.DATA[Constants.entrance_xscroll + (entranceId * 2)]);
        yscroll = (short)(((ROM.DATA[(Constants.entrance_yscroll + (entranceId * 2)) + 1]) << 8) + ROM.DATA[Constants.entrance_yscroll + (entranceId * 2)]);
        ycamera = (short)(((ROM.DATA[(Constants.entrance_camerayposition + (entranceId * 2)) + 1]) << 8) + ROM.DATA[Constants.entrance_camerayposition + (entranceId * 2)]);
        xcamera = (short)(((ROM.DATA[(Constants.entrance_cameraxposition + (entranceId * 2)) + 1]) << 8) + ROM.DATA[Constants.entrance_cameraxposition + (entranceId * 2)]);
        blockset = (byte)(ROM.DATA[(Constants.entrance_blockset + entranceId)]);
        music = (byte)(ROM.DATA[(Constants.entrance_music + entranceId)]);
        dungeon = (byte)(ROM.DATA[(Constants.entrance_dungeon + entranceId)]);
        floor = (byte)(ROM.DATA[(Constants.entrance_floor + entranceId)]);
        door = (byte)(ROM.DATA[(Constants.entrance_door + entranceId)]);
        ladderbg = (byte)(ROM.DATA[(Constants.entrance_ladderbg + entranceId)]);
        scrolling = (byte)(ROM.DATA[(Constants.entrance_scrolling + entranceId)]);
        scrollquadrant = (byte)(ROM.DATA[(Constants.entrance_scrollquadrant + entranceId)]);
        exit = (short)(((ROM.DATA[(Constants.entrance_exit + (entranceId * 2)) + 1]) << 8) + ROM.DATA[Constants.entrance_exit + (entranceId * 2)]);
        scrolledge_HU = (byte)(ROM.DATA[(Constants.entrance_scrolledge + entranceId)]);
        scrolledge_FU = (byte)(ROM.DATA[(Constants.entrance_scrolledge + entranceId) + 1]);
        scrolledge_HD = (byte)(ROM.DATA[(Constants.entrance_scrolledge + entranceId) + 2]);
        scrolledge_FD = (byte)(ROM.DATA[(Constants.entrance_scrolledge + entranceId) + 3]);
        scrolledge_HL = (byte)(ROM.DATA[(Constants.entrance_scrolledge + entranceId) + 4]);
        scrolledge_FL = (byte)(ROM.DATA[(Constants.entrance_scrolledge + entranceId) + 5]);
        scrolledge_HR = (byte)(ROM.DATA[(Constants.entrance_scrolledge + entranceId) + 6]);
        scrolledge_FR = (byte)(ROM.DATA[(Constants.entrance_scrolledge + entranceId) + 7]);

        if (startingEntrance == true)
        {
            room = (short)((ROM.DATA[(Constants.startingentrance_room + ((entranceId) * 2)) + 1] << 8) + ROM.DATA[Constants.startingentrance_room + ((entranceId) * 2)]);
            yposition = (short)(((ROM.DATA[(Constants.startingentrance_yposition + ((entranceId) * 2)) + 1] & 0x01) << 8) + ROM.DATA[Constants.startingentrance_yposition + ((entranceId) * 2)]);
            xposition = (short)(((ROM.DATA[(Constants.startingentrance_xposition + ((entranceId) * 2)) + 1] & 0x01) << 8) + ROM.DATA[Constants.startingentrance_xposition + ((entranceId) * 2)]);
            xscroll = (short)(((ROM.DATA[(Constants.startingentrance_xscroll + ((entranceId) * 2)) + 1] & 0x01) << 8) + ROM.DATA[Constants.startingentrance_xscroll + ((entranceId) * 2)]);
            yscroll = (short)(((ROM.DATA[(Constants.startingentrance_yscroll + ((entranceId) * 2)) + 1] & 0x01) << 8) + ROM.DATA[Constants.startingentrance_yscroll + ((entranceId) * 2)]);
            ycamera = (short)(((ROM.DATA[(Constants.startingentrance_camerayposition + ((entranceId) * 2)) + 1] & 0x01) << 8) + ROM.DATA[Constants.startingentrance_camerayposition + ((entranceId) * 2)]);
            xcamera = (short)(((ROM.DATA[(Constants.startingentrance_cameraxposition + ((entranceId) * 2)) + 1] & 0x01) << 8) + ROM.DATA[Constants.startingentrance_cameraxposition + ((entranceId) * 2)]);
            blockset = (byte)(ROM.DATA[(Constants.startingentrance_blockset + entranceId)]);
            music = (byte)(ROM.DATA[(Constants.startingentrance_music + entranceId)]);
            dungeon = (byte)(ROM.DATA[(Constants.startingentrance_dungeon + entranceId)]);
            floor = (byte)(ROM.DATA[(Constants.startingentrance_floor + entranceId)]);
            door = (byte)(ROM.DATA[(Constants.startingentrance_door + entranceId)]);
            ladderbg = (byte)(ROM.DATA[(Constants.startingentrance_ladderbg + entranceId)]);
            scrolling = (byte)(ROM.DATA[(Constants.startingentrance_scrolling + entranceId)]);
            scrollquadrant = (byte)(ROM.DATA[(Constants.startingentrance_scrollquadrant + entranceId)]);
            exit = (short)(((ROM.DATA[(Constants.startingentrance_exit + (entranceId * 2)) + 1] & 0x01) << 8) + ROM.DATA[Constants.startingentrance_exit + (entranceId * 2)]);
            scrolledge_HU = (byte)(ROM.DATA[(Constants.startingentrance_scrolledge + entranceId)]);
            scrolledge_FU = (byte)(ROM.DATA[(Constants.startingentrance_scrolledge + entranceId) + 1]);
            scrolledge_HD = (byte)(ROM.DATA[(Constants.startingentrance_scrolledge + entranceId) + 2]);
            scrolledge_FD = (byte)(ROM.DATA[(Constants.startingentrance_scrolledge + entranceId) + 3]);
            scrolledge_HL = (byte)(ROM.DATA[(Constants.startingentrance_scrolledge + entranceId) + 4]);
            scrolledge_FL = (byte)(ROM.DATA[(Constants.startingentrance_scrolledge + entranceId) + 5]);
            scrolledge_HR = (byte)(ROM.DATA[(Constants.startingentrance_scrolledge + entranceId) + 6]);
            scrolledge_FR = (byte)(ROM.DATA[(Constants.startingentrance_scrolledge + entranceId) + 7]);
        }
    }
}