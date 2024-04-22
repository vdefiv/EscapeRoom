namespace libs;

public class GameObjectFactory : IGameObjectFactory
{
    
    private int amountOfBoxes = 0;

    public int GetAmountOfBoxes { get => amountOfBoxes; }

    public GameObject CreateGameObject(dynamic obj) {

        GameObject newObj = new GameObject();
        int type = obj.Type;
        switch (type)
        {
            case (int) GameObjectType.Player:
                // newObj = obj.ToObject<Player>();
                newObj = Player.Instance;
                newObj.PosX = obj.PosX;
                newObj.PosY = obj.PosY;
                break;
            case (int) GameObjectType.Obstacle:
                newObj = obj.ToObject<Obstacle>();
                break;
            case (int) GameObjectType.Box:
                newObj = obj.ToObject<Box>();
                amountOfBoxes++;
                break;
            case (int) GameObjectType.Target:
                newObj = obj.ToObject<Target>();
              break;
        }

        return newObj;
    }
    public void SetAmountOfBoxes(int amount)
        {
            amountOfBoxes = amount;
        }
}
