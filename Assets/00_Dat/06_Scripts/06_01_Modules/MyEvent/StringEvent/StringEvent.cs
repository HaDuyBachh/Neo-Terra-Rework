using UnityEngine;

namespace MyTools.Event{

    [System.Serializable]
    public struct StringEventStruct{
        public string value;
        public TypeState typeState;
    }

    [CreateAssetMenu(fileName = nameof(StringEvent), menuName = ("GameEvents/" + nameof(StringEvent)), order = 0)]
    public class StringEvent : GameEvent<StringEventStruct>{}
}