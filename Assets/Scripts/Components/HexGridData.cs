using MyEcs;



//HexMap
// 0. [a1][a2][a3][a4]  (a1)(a2)(a3)(a4)
// 1. [b1][b2][b3][b4]    (b1)(b2)(b3)
// 2. [c1][c2][c3][c4]  (c1)(c2)(c3)(a4)
public class HexGridData
{
    public Entity[,] data;
    public int Width;
    public int Height;
}