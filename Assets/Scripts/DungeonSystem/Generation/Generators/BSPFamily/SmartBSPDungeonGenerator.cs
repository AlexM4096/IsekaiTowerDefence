using System.Collections.Generic;
using System.Linq;
using Tools.BinaryTree;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DungeonSystem.Generation.Generators
{
    // ReSharper disable once InconsistentNaming
    public class SmartBSPDungeonGenerator : BSPDungeonGenerator
    {
        public SmartBSPDungeonGenerator(DungeonGeneratorConfig config) : base(config) {}

        public override Dungeon GenerateDungeon()
        {
            int amount = 1;
            
            RectInt dungeonRoom = new RectInt(Vector2Int.zero, Config.Size);
            BinaryTree<RectInt> binaryTree = new BinaryTree<RectInt>(dungeonRoom);

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
                    
                    break;
                }

                BinaryTreeNode<RectInt> binaryTreeNode = dictionary[room];
                Split(binaryTreeNode);
                
                amount++;
            }

            IEnumerable<RectInt> enumerable = binaryTree.GetLeaves().Select(t => t.Value);
            Dungeon dungeon = new Dungeon(Config.Size, enumerable);
            
            foreach (RectInt room in enumerable)
            {
                foreach (Vector2Int position in room.allPositionsWithin)
                {
                    dungeon[position.x, position.y] = DungeonCellType.Floor;
                }
            }
            
            return dungeon;
        }
    }
}