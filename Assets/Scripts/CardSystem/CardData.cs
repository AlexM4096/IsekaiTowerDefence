using UnityEngine;

namespace CardSystem
{
    [CreateAssetMenu]
    public class CardData : ScriptableObject
    {
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public short Name { get; private set; }
        [field: SerializeField] public CardCategory Category { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public int Description { get; private set; }
    }
}
