using Dungeon.Generation;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Dungeon.Visualization
{
    public class DungeonVisualizer : MonoBehaviour
    {
        [SerializeField] private DungeonGeneratorController dungeonGeneratorController; 
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private TileBase floorTile;

        public void VisualizeDungeon()
        {
            var dungeon = dungeonGeneratorController.Dungeon;

            for (int x = 0; x < dungeon.Width; x++)
            {
                for (int y = 0; y < dungeon.Height; y++)
                {
                    if (dungeon[x, y] == DungeonCellType.Floor)
                        tilemap.SetTile(new Vector3Int(x, y, 0), floorTile);
                }
            }
        }

        public void ClearTilemap() => tilemap.ClearAllTiles();
    }
}