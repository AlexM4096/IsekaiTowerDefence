using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonGeneration
{
    public class BSPDungeonGenerator : DungeonGenerator
    {
        public BSPDungeonGenerator(DungeonGeneratorConfig config) : base(config) {}
        
        public override Dungeon GenerateDungeon()
        {
            List<RectInt> newRooms = new List<RectInt>();
            List<RectInt> finalRooms = new List<RectInt>();
            
            finalRooms.Add(new RectInt(Vector2Int.zero, Config.Size));
            
            for (int i = 0; i < 4; i++)
            {
                foreach (var room in finalRooms)
                {
                    if (room.width < Config.MinimalRoomSize.x * 2 || room.height < Config.MinimalRoomSize.y * 2)
                    {
                        newRooms.Add(room);
                        continue;
                    }
                    
                    Split(room, out var room1, out var room2);
                    
                    newRooms.Add(room1);
                    newRooms.Add(room2);
                }
                
                finalRooms = newRooms;
                newRooms = new List<RectInt>();
            }

            Dungeon dungeon = new Dungeon(Config.Size, finalRooms);
            return dungeon;
        }
        
        public void Split(RectInt room, out RectInt room1, out RectInt room2)
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
                height = Random.Range(Config.MinimalRoomSize.y, height - Config.MinimalRoomSize.y);
        
                room1 = new RectInt(room.position, new Vector2Int(width, height));
                room2 = new RectInt(room.position + new Vector2Int(0, height), new Vector2Int(width,  room.height - height));
            }
            else
            {
                width = Random.Range(Config.MinimalRoomSize.x, width - Config.MinimalRoomSize.x);
        
                room1 = new RectInt(room.position, new Vector2Int(width, height));
                room2 = new RectInt(room.position + new Vector2Int(width, 0), new Vector2Int(room.width - width,  height));
            }
        }
    }
}