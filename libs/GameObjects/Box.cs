namespace libs;

public class Box : GameObject {

    public Map map = GameEngine.Instance.GetMap();


    public Box () : base(){
        Type = GameObjectType.Player;
        CharRepresentation = '○';
        Color = ConsoleColor.DarkGreen;
    }

    public override void Move(int dx, int dy) {
        int  goToX = PosX +dx;
        int  goToY = PosY +dy;
        
        // Type type = map.Get(PosX +dx, PosY +dy).GetType();
        GameObject? PotentialTarget = map.Get(goToY, goToX);
        
        if(PotentialTarget.Type == GameObjectType.Target){
            Console.WriteLine ("touched the target, you won");
        }

        this.SetPrevPosY(this.PosY);
        this.SetPrevPosX(this.PosX);
        this.PosX += dx;
        this.PosY += dy;
    }
}