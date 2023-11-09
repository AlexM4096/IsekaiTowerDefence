using System;
using System.Collections.Generic;
using System.Linq;
using Tools.BinaryTree;
using UnityEngine;
using Random = UnityEngine.Random;


// ReSharper disable once CheckNamespace
namespace DungeonSystem.Generation.Generators
{
    
    // ReSharper disable once InconsistentNaming
    public class BSPDungeonGenerator : DungeonGenerator
    {
        public BSPDungeonGenerator(DungeonGeneratorConfig config) : base(config) {}

        public override Dungeon GenerateDungeon()
        {
            int amount = 1;

            RectInt dungeonRoom = new RectInt(Config.Center, Config.Size);
            BinaryTree<RectInt> binaryTree = new BinaryTree<RectInt>(dungeonRoom);
            
            int levels = Mathf.CeilToInt(Mathf.Log(Config.RoomsAmount, 2));
            for (int i = 0; i < levels; i++)
            {
                foreach (var binaryTreeNode in binaryTree.GetLeaves())
                {
                    RectInt room = binaryTreeNode.Value;
                    
                    if (room.width < Config.MinimalRoomSize * 2 && room.height < Config.MinimalRoomSize * 2)
                        continue;

                    Split(binaryTreeNode);

                    amount++;

                    if (amount >= Config.RoomsAmount)
                    {
                        if (Config.ExactRoomsAmount)
                            break;
                        
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
        
        public void Split(BinaryTreeNode<RectInt> binaryTreeNode)
        {
            bool isSplitHorizontal;

            RectInt room = binaryTreeNode.Value;
            
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

                binaryTreeNode.Right = new BinaryTreeNode<RectInt>(
                    new RectInt(
                        room.position + new Vector2Int(0, height),
                        new Vector2Int(width, room.height - height)
                    )    
                );
            }
            else
            {
                width = Random.Range(Config.MinimalRoomSize, width - Config.MinimalRoomSize);

                binaryTreeNode.Right = new BinaryTreeNode<RectInt>(
                    new RectInt(
                        room.position + new Vector2Int(width, 0),
                        new Vector2Int(room.width - width, height)
                    )
                );
            }

            binaryTreeNode.Left = new BinaryTreeNode<RectInt>(
                new RectInt(
                    room.position,
                    new Vector2Int(width, height)
                )
            );
        }
    }
}