/*
 * Author:  Zarby89
 */

 /// <summary>
 /// 
 /// </summary>
public struct TileInfo
{
    public bool o, v, h; //o = over, v = vertical mirror, h = horizontal mirror
    public byte palette;
    public short id;
    public TileInfo(short id, byte palette, bool v, bool h, bool o)
    {
        this.id = id;
        this.palette = palette;
        this.v = v;
        this.h = h;
        this.o = o;
    }
}