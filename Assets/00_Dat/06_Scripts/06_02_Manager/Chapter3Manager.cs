using Singleton;

namespace Game.Manager {
    public class Chapter3Manager : SingletonMonoBehaviour<Chapter3Manager>{
        public int completePercent = 0;

        public override void Awake()
        {
            base.Awake();
            completePercent = 0;
        }

        
    }
}