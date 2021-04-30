using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "New Player Type", menuName = "New Player Type")]
    public class PlayerType : ScriptableObject
    {
        public float speed;
        public float health;
    }
}