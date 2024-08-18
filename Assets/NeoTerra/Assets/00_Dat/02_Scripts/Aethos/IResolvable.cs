namespace Game.Object{
    public interface IResolvable{
        T Resolve<T>() where T : class, IObjectComponent;
    }
}