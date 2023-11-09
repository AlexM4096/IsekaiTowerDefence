using System.Collections.Generic;
using DungeonSystem.Generation.Generators;
using Unity.VisualScripting;
using UnityEngine;

namespace DungeonSystem.Generation
{
    public class DungeonGeneratorController : MonoBehaviour
    {
        [SerializeField] private DungeonGeneratorConfig config;
        [SerializeField] private DungeonGeneratorType algorithm;
        [SerializeField] private GameObject prefab;
        
        public Dungeon Dungeon;
        
        private readonly List<GameObject> _roomGameObjects = new List<GameObject>();
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
                    transform
                );    
                go.GetComponent<SpriteRenderer>().size = room.size;
                _roomGameObjects.Add(go);
            }
        }

        public void Clear()
        {
            _roomGameObjects.ForEach(DestroyImmediate);
            _roomGameObjects.Clear();
        }

        public void ClearGameObjects()
        {
            foreach (Transform tr in transform)
                _roomGameObjects.Add(tr.gameObject);

            Clear();
        }
    }
}