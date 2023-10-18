using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonGeneration
{
    public class TestDungeonGenerator : MonoBehaviour
    {
        public Vector2Int size;
        public GameObject prefab;
        
        private void Awake()
        {
            List<RectInt> newRooms = new List<RectInt>();
            List<RectInt> finalRooms = new List<RectInt>();
            
            finalRooms.Add(new RectInt(Vector2Int.zero, size));

            for (int i = 0; i < 4; i++)
            {
                foreach (var room in finalRooms)
                {
                    Split(room, out var room1, out var room2);
                    
                    newRooms.Add(room1);
                    newRooms.Add(room2);
                }
                
                finalRooms = newRooms;
                newRooms = new List<RectInt>();
            }

            foreach (var room in finalRooms)
            {
                if (room.width == 0 || room.height == 0) continue;
                
                GameObject go = Instantiate(prefab, new Vector3(room.center.x, room.center.y, 0), Quaternion.identity, transform);
                go.GetComponent<SpriteRenderer>().size = room.size;
            }
        }

        public static void Split(RectInt room, out RectInt room1, out RectInt room2)
        {
            bool isSplitHorizontal;

            int width = room.width;
            int height = room.height;

            if ((float)width / height >= 1.5f)
                isSplitHorizontal = false;
            else if ((float)height / width >= 1.5f)
                isSplitHorizontal = true;
            else
                isSplitHorizontal = Convert.ToBoolean(Random.Range(0, 2));

            if (isSplitHorizontal)
            {
                height = Random.Range(1, height);

                room1 = new RectInt(room.position, new Vector2Int(width, height));
                room2 = new RectInt(room.position + new Vector2Int(0, height), new Vector2Int(width,  room.height - height));
            }
            else
            {
                width = Random.Range(1, width);

                room1 = new RectInt(room.position, new Vector2Int(width, height));
                room2 = new RectInt(room.position + new Vector2Int(width, 0), new Vector2Int(room.width - width,  height));
            }
        }
    }
}