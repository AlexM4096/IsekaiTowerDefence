using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonGeneration
{
    public class SmartBSPDungeonGeneration : DungeonGenerator
    {
        public SmartBSPDungeonGeneration(DungeonGeneratorConfig config) : base(config) {}
        
        public override Dungeon GenerateDungeon()
        {
            int amount = 1;
            
            List<RectInt> finalRooms = new List<RectInt>();
            
            finalRooms.Add(new RectInt(Vector2Int.zero, Config.Size));

            while (amount < Config.RoomsAmount)
            {
                finalRooms.Sort(delegate(RectInt a, RectInt b)
                    {
                        int s = b.width * b.height - a.width * a.height;
                        return s;
                    }
                );

                RectInt room = finalRooms.
                    First(t => 
                        t.width >= 2 * Config.MinimalRoomSize.x || 
                        t.height >= 2 * Config.MinimalRoomSize.y);
                finalRooms.Remove(room);
                
                Split(room, out RectInt room1, out RectInt room2);
                
                finalRooms.Add(room1);
                finalRooms.Add(room2);
                amount++;
            }

            Dungeon dungeon = new Dungeon(Config.Size, finalRooms);
            return dungeon;
        }
        
        public void Split(RectInt room, out RectInt room1, out RectInt room2)
        {
            bool isSplitHorizontal;
        
            int width = room.width;
            int height = room.height;
        
            if ((float)width / height >= 2f)
                isSplitHorizontal = false;
            else if ((float)height / width >= 2f)
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