/*
 * Author:  Zarby89
 */


 /// <summary>
 /// 
 /// </summary>
public struct ExitOW
{
    public short roomId;
    public byte mapId;
    public short vramLocation;
    public short
        xScroll,
        yScroll,
        playerX,
        playerY,
        cameraX,
        cameraY;

    public byte
        unk1,
        unk2,
        doorType1,
        doorType2;

    public ExitOW(short roomId, byte mapId, short vramLocation, short yScroll, short xScroll, short playerY, short playerX, short cameraY, short cameraX, byte unk1, byte unk2, byte doorType1, byte doorType2)
    {
        this.roomId = roomId;
        this.mapId = mapId;
        this.vramLocation = vramLocation;
        this.xScroll = xScroll;
        this.yScroll = yScroll;
        this.playerX = playerX;
        this.playerY = playerY;
        this.cameraX = cameraX;
        this.cameraY = cameraY;
        this.unk1 = unk1;
        this.unk2 = unk2;
        this.doorType1 = doorType1;
        this.doorType2 = doorType2;
    }
}