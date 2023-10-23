using UnityEngine;

namespace DungeonGeneration
{
    public class TestDungeonGenerator : MonoBehaviour
    {
        [SerializeField] private Vector2Int size;
        [SerializeField] private GameObject prefab;
        private Dungeon _dungeon;

        private void Awake()
        {
            BSPDungeonGenerator sss = new BSPDungeonGenerator();
            _dungeon = sss.GenerateDungeon(size);
            
            foreach (var room in _dungeon.Rooms)
            {
                if (room.width == 0 || room.height == 0) continue;
                
                GameObject go = Instantiate(prefab, new Vector3(room.center.x, room.center.y, 0), Quaternion.identity, transform);
                go.GetComponent<SpriteRenderer>().size = room.size;
            }
        }
    }
}