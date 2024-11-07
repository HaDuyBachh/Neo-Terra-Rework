using UnityEngine;

namespace MyTools.Event{

    [System.Serializable]
    public struct StringEventStruct{
        public string value;
    }

    [CreateAssetMenu(fileName = nameof(StringEvent), menuName = ("GameEvents/" + nameof(StringEvent)), order = 0)]
    public class StringEvent : GameEvent<StringEventStruct>{}
}