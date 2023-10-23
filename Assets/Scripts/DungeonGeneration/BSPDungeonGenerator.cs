using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonGeneration
{
    public class BSPDungeonGenerator : IDungeonGenerator
    {
        public Dungeon GenerateDungeon(Vector2Int size)
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

            Dungeon dungeon = new Dungeon(size, finalRooms);
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
                height = Random.Range(1, height - 1);
        
                room1 = new RectInt(room.position, new Vector2Int(width, height));
                room2 = new RectInt(room.position + new Vector2Int(0, height), new Vector2Int(width,  room.height - height));
            }
            else
            {
                width = Random.Range(1, width - 1);
        
                room1 = new RectInt(room.position, new Vector2Int(width, height));
                room2 = new RectInt(room.position + new Vector2Int(width, 0), new Vector2Int(room.width - width,  height));
            }
        }
        
        // [SerializeField, Range(1, 2)] private float _attitude;
        //
        // public void GenerateDungeon(Dungeon dungeon)
        // {
        //     List<RectInt> newRooms = new List<RectInt>();
        //     List<RectInt> finalRooms = new List<RectInt>();
        //     
        //     finalRooms.Add(new RectInt(Vector2Int.zero, new Vector2Int(dungeon.Width, dungeon.Height)));
        //
        //     for (int i = 0; i < 4; i++)
        //     {
        //         foreach (var room in finalRooms)
        //         {
        //             Split(room, out var room1, out var room2);
        //             
        //             newRooms.Add(room1);
        //             newRooms.Add(room2);
        //         }
        //         
        //         finalRooms = newRooms;
        //         newRooms = new List<RectInt>();
        //     }
        // }
        //
        // public void Split(RectInt room, out RectInt room1, out RectInt room2)
        // {
        //     bool isSplitHorizontal;
        //
        //     int width = room.width;
        //     int height = room.height;
        //
        //     if ((float)width / height >= _attitude)
        //         isSplitHorizontal = false;
        //     else if ((float)height / width >= _attitude)
        //         isSplitHorizontal = true;
        //     else
        //         isSplitHorizontal = Convert.ToBoolean(Random.Range(0, 2));
        //
        //     if (isSplitHorizontal)
        //     {
        //         height = Random.Range(1, height - 1);
        //
        //         room1 = new RectInt(room.position, new Vector2Int(width, height));
        //         room2 = new RectInt(room.position + new Vector2Int(0, height), new Vector2Int(width,  room.height - height));
        //     }
        //     else
        //     {
        //         width = Random.Range(1, width - 1);
        //
        //         room1 = new RectInt(room.position, new Vector2Int(width, height));
        //         room2 = new RectInt(room.position + new Vector2Int(width, 0), new Vector2Int(room.width - width,  height));
        //     }
        // }
        
    }
}