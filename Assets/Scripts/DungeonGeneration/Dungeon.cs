using System.Collections.Generic;
using UnityEngine;

namespace DungeonGeneration
{
    public enum DungeonCellType : byte
    {
        None,
        Floor,
        Wall,
        Path
    }
    
    public class Dungeon : Grid2D<DungeonCellType>
    {
        public readonly IReadOnlyList<RectInt> Rooms;

        public Dungeon(int width, int height, IEnumerable<RectInt> rooms) : base(width, height)
        {
            Rooms = (IReadOnlyList<RectInt>)rooms;
        }

        public Dungeon(Vector2Int size, IEnumerable<RectInt> rooms) : this(size.x, size.y, rooms) {}
    }
}