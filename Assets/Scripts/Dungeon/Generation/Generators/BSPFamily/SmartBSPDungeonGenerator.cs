using System;
using System.Collections.Generic;
using System.Linq;
using Tools.BinaryTree;
using UnityEngine;
using Random = UnityEngine.Random;

// ReSharper disable once CheckNamespace
namespace Dungeon.Generation.Generators
{
    // ReSharper disable once InconsistentNaming
    public class SmartBSPDungeonGenerator : BSPDungeonGenerator
    {
        public SmartBSPDungeonGenerator(DungeonGeneratorConfig config) : base(config) {}
        
        // public override Dungeon GenerateDungeon()
        // {
        //     int amount = 1;
        //     
        //     List<RectInt> rooms = new List<RectInt>();
        //     
        //     rooms.Add(new RectInt(Vector2Int.zero, Config.Size));
        //
        //     while (amount < Config.RoomsAmount)
        //     {
        //         rooms.Sort(delegate(RectInt a, RectInt b)
        //             {
        //                 int s = b.width * b.height - a.width * a.height;
        //                 return s;
        //             }
        //         );
        //
        //         RectInt room = rooms.
        //             FirstOrDefault(t => 
        //                 t.width >= 2 * Config.MinimalRoomSize || 
        //                 t.height >= 2 * Config.MinimalRoomSize
        //                 );
        //
        //         if (Equals(room, default(RectInt)))
        //             return null;
        //         
        //         rooms.Remove(room);
        //         
        //         Split(room, out RectInt room1, out RectInt room2);
        //         
        //         rooms.Add(room1);
        //         rooms.Add(room2);
        //         amount++;
        //     }
        //
        //     Dungeon dungeon = new Dungeon(Config.Size, rooms);
        //     return dungeon;
        // }
        
        public override Dungeon GenerateDungeon()
        {
            int amount = 1;
            
            BinaryTree<RectInt> binaryTree = new(new RectInt(Vector2Int.zero, Config.Size));

            while (amount < Config.RoomsAmount)
            {
                Dictionary<RectInt, BinaryTreeNode<RectInt>> dictionary = binaryTree.GetLeaves()
                    .ToDictionary(t => t.Value);

                List<RectInt> rooms = dictionary.Select(t => t.Key).ToList();
                rooms.Sort(delegate(RectInt a, RectInt b)
                    {
                        int s = b.width * b.height - a.width * a.height;
                        return s;
                    }
                );
                
                RectInt room = rooms.
                FirstOrDefault(t => 
                    t.width >= 2 * Config.MinimalRoomSize || 
                    t.height >= 2 * Config.MinimalRoomSize
                    );

                if (Equals(room, default(RectInt)))
                {
                    if (Config.ExactRoomsAmount)
                        return null;
                    else
                        break;
                }

                Split(room, out RectInt room1, out RectInt room2);

                BinaryTreeNode<RectInt> binaryTreeNode = dictionary[room];
                
                binaryTreeNode.Right = new BinaryTreeNode<RectInt>(room1);
                binaryTreeNode.Left = new BinaryTreeNode<RectInt>(room2);

                amount++;
            }

            IEnumerable<RectInt> enumerable = binaryTree.GetLeaves().Select(t => t.Value);
            Dungeon dungeon = new Dungeon(Config.Size, enumerable);
            return dungeon;
        }
    }
}