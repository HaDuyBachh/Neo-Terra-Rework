using System;
using UnityEngine;
namespace Game.Event{
    public class GameEvent{
        public static Action<Vector3> OnPlayerMove;

        public static Action OnReachTarget;

        public static Action<string> OnTalkToPlayer;
        public static Action<Trash, bool> OnScanObject;
    }
}