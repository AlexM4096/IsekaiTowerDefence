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
    public class BSPDungeonGenerator : DungeonGenerator
    {
        public BSPDungeonGenerator(DungeonGeneratorConfig config) : base(config) {}

        public override Dungeon GenerateDungeon()
        {
            int amount = 1;
            
            BinaryTree<RectInt> binaryTree = new(new RectInt(Vector2Int.zero, Config.Size));
            
            int levels = Mathf.CeilToInt(Mathf.Log(Config.RoomsAmount, 2));
            for (int i = 0; i < levels; i++)
            {
                foreach (var binaryTreeNode in binaryTree.GetLeaves())
                {
                    RectInt room = binaryTreeNode.Value;
                    
                    if (room.width < Config.MinimalRoomSize * 2 && room.height < Config.MinimalRoomSize * 2)
                        continue;

                    Split(room, out var room1, out var room2);

                    binaryTreeNode.Right = new BinaryTreeNode<RectInt>(room1);
                    binaryTreeNode.Left = new BinaryTreeNode<RectInt>(room2);

                    amount++;

                    if (amount >= Config.RoomsAmount)
                    {
                        if (Config.ExactRoomsAmount)
                            break;
                        else
                            return null;
                    }
                }
            }

            if (amount < Config.RoomsAmount && Config.ExactRoomsAmount)
                return null;
   

            IEnumerable<RectInt> rooms = binaryTree.GetLeaves().Select(t => t.Value);
            Dungeon dungeon = new(Config.Size, rooms);
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