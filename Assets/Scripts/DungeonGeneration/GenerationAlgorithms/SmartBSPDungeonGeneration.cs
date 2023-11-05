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
            
            List<RectInt> rooms = new List<RectInt>();
            
            rooms.Add(new RectInt(Vector2Int.zero, Config.Size));

            while (amount < Config.RoomsAmount)
            {
                rooms.Sort(delegate(RectInt a, RectInt b)
                    {
                        int s = b.width * b.height - a.width * a.height;
                        return s;
                    }
                );

                RectInt room = rooms.
                    First(t => 
                        t.width >= 2 * Config.MinimalRoomSize || 
                        t.height >= 2 * Config.MinimalRoomSize
                        );
                rooms.Remove(room);
                
                Split(room, out RectInt room1, out RectInt room2);
                
                rooms.Add(room1);
                rooms.Add(room2);
                amount++;
            }

            Dungeon dungeon = new Dungeon(Config.Size, rooms);
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