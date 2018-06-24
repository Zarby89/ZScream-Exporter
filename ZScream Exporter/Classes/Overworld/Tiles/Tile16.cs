/*
 * Author:  Zarby89
 */

 /// <summary>
 /// 
 /// </summary>
public struct Tile16
{
    //[0,1]
    //[2,3]
    public Tile8 tile0, tile1, tile2, tile3;
    public Tile16(Tile8 tile0, Tile8 tile1, Tile8 tile2, Tile8 tile3)
    {
        this.tile0 = tile0;
        this.tile1 = tile1;
        this.tile2 = tile2;
        this.tile3 = tile3;
    }
}