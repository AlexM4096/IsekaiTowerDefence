using System;
using System.Collections.Generic;
using System.Linq;
using DungeonSystem.Generation.Generators;
using Tools;
using Tools.DelaunayTriangulation;
using Unity.VisualScripting;
using UnityEngine;
using Edge = Tools.DelaunayTriangulation.Edge;

namespace DungeonSystem.Generation
{
    public class DungeonController : MonoBehaviour
    {
        [SerializeField] private DungeonGeneratorConfig config;
        [SerializeField] private DungeonGeneratorType algorithm;
        [SerializeField] private GameObject prefab;
        
        public Dungeon Dungeon;
        
        private readonly List<GameObject> _roomGameObjects = new List<GameObject>();
        private readonly DungeonGeneratorFactory _dungeonGeneratorFactory = new DungeonGeneratorFactory();

        private HashSet<Edge> _edges = new HashSet<Edge>();
        
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
            
            List<Point> points = Dungeon.Rooms.Select(t => new Point(t.center)).ToList();
            // IEnumerable<Triangle> triangles = Triangulator.Triangulate(points);
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

        // private void OnDrawGizmos()
        // {
        //     if (Dungeon == null) return;
        //     
        //     foreach (Edge edge in _edges)
        //     {
        //         Gizmos.DrawLine(edge.Start.Value.ToVector3(), edge.End.Value.ToVector3());
        //     }
        // }
    }
}