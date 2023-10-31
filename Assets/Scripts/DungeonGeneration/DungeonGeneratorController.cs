using System.Collections.Generic;
using UnityEngine;

namespace DungeonGeneration
{
    public class DungeonGeneratorController : MonoBehaviour
    {
        [SerializeField] private DungeonGeneratorConfig config;
        [SerializeField] private DungeonGeneratorType algorithm;
        [SerializeField] private GameObject prefab;

        private DungeonGeneratorFactory _dungeonGeneratorFactory;
        private DungeonGenerator _dungeonGenerator; 
        private Dungeon _dungeon;
        private List<GameObject> _rooms = new List<GameObject>();

        private void OnValidate()
        {
            _dungeonGeneratorFactory = new DungeonGeneratorFactory(config);
        }

        public void Generate()
        {
            _rooms.ForEach(DestroyImmediate);
            _rooms.Clear();

            _dungeonGenerator = _dungeonGeneratorFactory.Get(algorithm);
            _dungeon = _dungeonGenerator.GenerateDungeon();
            
            foreach (var room in _dungeon.Rooms)
            {
                if (room.width == 0 || room.height == 0) continue;
                
                GameObject go = Instantiate(prefab, new Vector3(room.center.x, room.center.y, 0), Quaternion.identity, transform);
                go.GetComponent<SpriteRenderer>().size = room.size;
                _rooms.Add(go);
            }
        }
    }
}