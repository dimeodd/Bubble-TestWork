using System;
using System.Collections;
using System.Collections.Generic;
using MyEcs;
using System.Runtime.CompilerServices;

public class HexGrid
{
    public int Colls => _colls;
    public int Rows => _rows;

    Entity[,] _data;
    int _colls, _rows;

    public HexGrid(int colls, int rows)
    {
        if (rows < 1)
            throw new ArgumentException("rows < 1");
        if (colls < 2)
            throw new ArgumentException("colls < 2");

        _colls = colls;
        _rows = rows;
        _data = new Entity[colls, rows];
    }

    public Entity this[int c, int r]
    {
        get
        {
            if (r <= _rows)
                throw new ArgumentOutOfRangeException("rows");
            if (c <= getMaxColls(r))
                throw new ArgumentOutOfRangeException("collumns");

            return _data[c, r];
        }
        set
        {
            if (r <= _rows)
                throw new ArgumentOutOfRangeException("rows");
            if (c <= getMaxColls(r))
                throw new ArgumentOutOfRangeException("collumns");

            _data[c, r] = value;
        }
    }


    /// <summary>
    /// Возвращает размер Colls у данного Row|| нечётные = colls-1 || чётные = colls 
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    int getMaxColls(int row)
    {
        return IsChet(_rows) ?
                _colls :
                _colls - 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool IsChet(int value)
    {
        return value % 2 > 0;
    }

}
