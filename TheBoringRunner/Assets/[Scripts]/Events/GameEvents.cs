using Collectables;
using Other;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public static class GameEvents
    {
        public static PlayerCollisionEvent PlayerCollisionEvent = new PlayerCollisionEvent();
    }

    public class PlayerCollisionEvent : UnityEvent<GameObject,ColorType.ColorTypes>
    {
    }
}