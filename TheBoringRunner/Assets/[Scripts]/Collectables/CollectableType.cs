using Other;
using UnityEngine;

namespace Collectables
{
    [CreateAssetMenu(fileName = "New Collectable Type", menuName = "Create New Collectable Type")]
    public class CollectableType : ScriptableObject
    {
        public ColorType.ColorTypes colorTypes;
    }
}