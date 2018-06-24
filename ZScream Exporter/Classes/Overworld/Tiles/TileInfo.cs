/*
 * Author:  Zarby89
 */

 /// <summary>
 /// 
 /// </summary>
public struct Tile8
{
    public bool o, v, h; //o = over, v = vertical mirror, h = horizontal mirror
    public byte palette;
    public short id;

    public Tile8(short tile)
    {
        /*
         * vhopppcc, cccccccc
         * v = mirror vertical
         * h = mirror horizontal
         * p = palette
         * c = tile index
         * o = ???
         */

        o = v = h = false;

        id = (short)(tile & 0x3FF);
        palette = (byte)((tile >> 10) & 0x07);
        if ((tile & 0x2000) == 0x2000)
            o = true;
        if ((tile & 0x4000) == 0x4000)
            h = true;
        if ((tile & 0x8000) == 0x8000)
            v = true;
    }

    public Tile8(short id, byte palette, bool v, bool h, bool o)
    {
        this.id = id;
        this.palette = palette;
        this.v = v;
        this.h = h;
        this.o = o;
    }
}