namespace libs;

public class Box : GameObject {
    
    private GameObjectFactory gameObjectFactory;
    // private int targetsLeft;
 
    public Map map = GameEngine.Instance.GetMap();
    public Box () : base(){
        this.gameObjectFactory = (GameEngine.Instance.gameObjectFactory as GameObjectFactory);
        // this.targetsLeft = gameObjectFactory.AmountOfBoxes;
            Type = GameObjectType.Player;
            CharRepresentation = '○';
            Color = ConsoleColor.DarkGreen;
    }
    

    public override void Move(int dx, int dy) {
        var engine = GameEngine.Instance;

        bool touched = false;
        int  goToX = PosX +dx;
        int  goToY = PosY +dy;
        
        this.SetPrevPosY(this.PosY);
        this.SetPrevPosX(this.PosX);
        this.PosX += dx;
        this.PosY += dy;
        
        // Type type = map.Get(PosX +dx, PosY +dy).GetType();
        GameObject? PotentialTarget = map.Get(goToY, goToX);
        
        if(PotentialTarget.Type == GameObjectType.Target){
            Console.WriteLine ("touched the target");
            engine.AmountOfBoxes--;
        }

    }
}