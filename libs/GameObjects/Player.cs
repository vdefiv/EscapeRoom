namespace libs;

public class Player : GameObject {
    private static Player instance = null;

    public Map map = GameEngine.Instance.GetMap();

    private Player() : base() {
        Type = GameObjectType.Player;
        CharRepresentation = '☻';
        Color = ConsoleColor.DarkYellow;

    }

    public static Player Instance {
        get {
            if (instance == null) {
                instance = new Player();
            }
            return instance;
        }
    }

    public override void Move(int dx, int dy) {
        if(map.Get(PosX + dx, PosY + dy).GetType() == typeof(Obstacle)) {
            Console.WriteLine("Player created, map" + map.Get(0, 0).GetType());
        }
        // Console.WriteLine("Player created, map" + map.Get(0, 0).GetType());

    }

    
}