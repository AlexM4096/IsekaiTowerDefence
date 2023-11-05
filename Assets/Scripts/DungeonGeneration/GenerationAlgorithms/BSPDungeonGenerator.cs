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
            
            while (finalRooms.Count < Config.RoomsAmount)
            {
                foreach (var room in finalRooms)
                {
                    if (room.width < Config.MinimalRoomSize * 2 && room.height < Config.MinimalRoomSize * 2)
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
            
            if (width >= 2 * Config.MinimalRoomSize && height < 2 * Config.MinimalRoomSize)
                isSplitHorizontal = false;
            else if (height >= 2 * Config.MinimalRoomSize && width < 2 * Config.MinimalRoomSize)
                isSplitHorizontal = true;
            else
                isSplitHorizontal = Convert.ToBoolean(Random.Range(0, 2));
        
            if (isSplitHorizontal)
            {
                height = Random.Range(Config.MinimalRoomSize, height - Config.MinimalRoomSize);
        
                room1 = new RectInt(room.position, new Vector2Int(width, height));
                room2 = new RectInt(room.position + new Vector2Int(0, height), new Vector2Int(width,  room.height - height));
            }
            else
            {
                width = Random.Range(Config.MinimalRoomSize, width - Config.MinimalRoomSize);
        
                room1 = new RectInt(room.position, new Vector2Int(width, height));
                room2 = new RectInt(room.position + new Vector2Int(width, 0), new Vector2Int(room.width - width,  height));
            }
        }
    }
}