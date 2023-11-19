using DungeonSystem.Generation;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace DungeonSystem.Visualization
{
    public class DungeonVisualizer : MonoBehaviour
    {
        [SerializeField] private DungeonController dungeonController; 
        
        [SerializeField] private Tilemap floorTilemap;
        [SerializeField] private TileBase floorTile;
        
        [SerializeField] private Tilemap wallTilemap;
        [SerializeField] private TileBase wallTile;

        public void VisualizeDungeon()
        {
            var dungeon = dungeonController.Dungeon;

            for (int x = 0; x < dungeon.Width; x++)
            {
                for (int y = 0; y < dungeon.Height; y++)
                {
                    if (dungeon[x, y] == DungeonCellType.Floor)
                        floorTilemap.SetTile(new Vector3Int(x, y, 0), floorTile);
                    else if (dungeon[x, y] == DungeonCellType.Wall)
                        wallTilemap.SetTile(new Vector3Int(x, y, 0), wallTile);
                }
            }
        }

        public void ClearTilemap()
        {
            floorTilemap.ClearAllTiles();
            wallTilemap.ClearAllTiles();
        }
    }
}