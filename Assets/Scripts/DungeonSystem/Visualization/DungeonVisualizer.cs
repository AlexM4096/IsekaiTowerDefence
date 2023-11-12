using DungeonSystem.Generation;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace DungeonSystem.Visualization
{
    public class DungeonVisualizer : MonoBehaviour
    {
        [FormerlySerializedAs("dungeonGeneratorController")] [SerializeField] private DungeonController dungeonController; 
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private TileBase floorTile;

        public void VisualizeDungeon()
        {
            var dungeon = dungeonController.Dungeon;

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