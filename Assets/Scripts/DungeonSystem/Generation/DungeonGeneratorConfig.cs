using UnityEngine;

namespace DungeonSystem.Generation
{
    [CreateAssetMenu(menuName = "Create Dungeon Generator Config")]
    public class DungeonGeneratorConfig : ScriptableObject
    {
        [field: SerializeField] public Vector2Int Size { get; private set; }
        [field: SerializeField, Range(4, 16)] public int MinimalRoomSize { get; private set; }
        [field: SerializeField, Range(1, 128)] public int RoomsAmount{ get; private set; }
        [field: SerializeField] public bool ExactRoomsAmount { get; private set; } = true;
        
        public Vector2Int Center => - Size / 2;
    }
}