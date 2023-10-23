using UnityEngine;

namespace DungeonGeneration
{
    public interface IDungeonGenerator
    {
        Dungeon GenerateDungeon(Vector2Int size);
    }
}