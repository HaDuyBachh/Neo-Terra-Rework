namespace Game.Object{
    public interface IObjectAction : IUpdateable{
        public bool Enter();
        public bool Exit(ActionEnum actionAfter);
    }
}