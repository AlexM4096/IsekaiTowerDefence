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
    
    public class DungeonGrid : Grid2D<DungeonCellType>
    {
        public readonly Vector2Int size;
        
        public DungeonGrid(int width, int height) : base(width, height)
        {
            size = new Vector2Int(width, height);
        }
    }
}