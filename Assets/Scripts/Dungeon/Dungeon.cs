using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Dungeon
{
    public enum DungeonCellType : byte
    {
        None,
        Floor,
        Wall,
        Path
    }
    
    public class Dungeon : Array2D<DungeonCellType>
    {
        public readonly IEnumerable<RectInt> Rooms;

        public Dungeon(int width, int height, IEnumerable<RectInt> rooms) : base(width, height)
        {
            Rooms = rooms;
        }

        public Dungeon(Vector2Int size, IEnumerable<RectInt> rooms) : this(size.x, size.y, rooms) {}
    }
}