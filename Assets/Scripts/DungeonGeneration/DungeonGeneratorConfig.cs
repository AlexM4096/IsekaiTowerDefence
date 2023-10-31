using UnityEngine;

namespace DungeonGeneration
{
    [CreateAssetMenu(menuName = "Create Dungeon Generator Config")]
    public class DungeonGeneratorConfig : ScriptableObject
    {
        [SerializeField] private Vector2Int size;
        [SerializeField] private Vector2Int minimalRoomSize;
        [SerializeField, Range(0, 100)] private int roomsAmount;

        public Vector2Int Size => size;
        public Vector2Int MinimalRoomSize => minimalRoomSize;
        public int RoomsAmount => roomsAmount;
    }
}