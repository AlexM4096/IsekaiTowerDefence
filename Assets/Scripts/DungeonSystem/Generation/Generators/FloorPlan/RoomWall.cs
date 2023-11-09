using Tools;
using UnityEngine;

namespace DungeonSystem.Generation.Generators.FloorPlan
{
    public class RoomWall
    {
        public Ref<Vector2Int> Start { get; set; }
        public Ref<Vector2Int> End { get; set; }

        public bool CanGrow;

        public RoomWall(Ref<Vector2Int> start, Ref<Vector2Int> end)
        {
            Start = start;
            End = end;
        }
    }
}