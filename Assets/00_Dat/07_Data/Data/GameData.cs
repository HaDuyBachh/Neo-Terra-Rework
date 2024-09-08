using UnityEngine;

namespace Data{
    [CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
    public class GameData : ScriptableObject{
        public int process = 0;
        public int levelWillPlay = 0;

        public void Init(){
            process = 0;
            levelWillPlay = 1;
        }
    }
}