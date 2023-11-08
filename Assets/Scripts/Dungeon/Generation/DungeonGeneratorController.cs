using System.Collections.Generic;
using Dungeon.Generation.Generators;
using UnityEngine;

namespace Dungeon.Generation
{
    public class DungeonGeneratorController : MonoBehaviour
    {
        [SerializeField] private DungeonGeneratorConfig config;
        [SerializeField] private DungeonGeneratorType algorithm;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform roomsHolder;
        
        public Dungeon Dungeon;
        
        private readonly List<GameObject> _rooms = new List<GameObject>();
        private readonly DungeonGeneratorFactory _dungeonGeneratorFactory = new DungeonGeneratorFactory();
        
        private DungeonGenerator _dungeonGenerator;
        
        private const int MaximalAmountOfTiresToGenerate = 10;

        private void OnValidate()
        {
            _dungeonGeneratorFactory.UpdateConfig(config);
        }

        public void Generate()
        {
            Clear();

            _dungeonGenerator = _dungeonGeneratorFactory.Get(algorithm);

            int tries = 0;
            while (!_dungeonGenerator.TryGenerateDungeon(out Dungeon))
            {
                tries++;

                if (tries <= MaximalAmountOfTiresToGenerate) 
                    continue;
                
                print("Please try again or use less room to generate!");
                return;
            }

            foreach (var room in Dungeon.Rooms)
            {
                GameObject go = Instantiate(
                    prefab, 
                    new Vector3(room.center.x, room.center.y, 0), 
                    Quaternion.identity, 
                    roomsHolder
                    );
                go.GetComponent<SpriteRenderer>().size = room.size;
                _rooms.Add(go);
            }
        }

        public void Clear()
        {
            _rooms.ForEach(DestroyImmediate);
            _rooms.Clear();
        }
    }
}