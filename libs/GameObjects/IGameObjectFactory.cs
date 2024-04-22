namespace libs;

public interface IGameObjectFactory
{
    public GameObject CreateGameObject(dynamic obj);
    
    public int GetAmountOfBoxes { get; }
};