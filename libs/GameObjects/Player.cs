namespace libs;

public class Player : GameObject
{
    private static Player instance = null;

    public Map map = GameEngine.Instance.GetMap();

    private Player() : base()
    {
        Type = GameObjectType.Player;
        CharRepresentation = '☻';
        Color = ConsoleColor.DarkYellow;
    }

    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Player();
            }
            return instance;
        }
        set { }
    }

    public override void Move(int dx, int dy)
    {
        int goToX = PosX + dx;
        int goToY = PosY + dy;

        // Type type = map.Get(PosX +dx, PosY +dy).GetType();
        GameObject? PotentialBox = map.Get(goToY, goToX);

        // GameObject nextType = map.Get(PosX + dx, PosY + dy).GetType();
        if (PotentialBox.Type == GameObjectType.Obstacle) return;


        if (PotentialBox.Type == GameObjectType.Box)
        {
            GameObject? NextObject = map.Get(goToY + dy, goToX + dx);

            if (NextObject.Type == GameObjectType.Obstacle
            || NextObject.Type == GameObjectType.Box) return;

            PotentialBox.Move(dx, dy);
            if (NextObject.Type == GameObjectType.Target)
            {
                PotentialBox.Color = ConsoleColor.Red;
            }
            else
            {
                PotentialBox.Color = ConsoleColor.Yellow;
            }
        }

        this.SetPrevPosY(this.PosY);
        this.SetPrevPosX(this.PosX);
        this.PosX += dx;
        this.PosY += dy;
    }

    public virtual void Rotate(int dx, int dy)
    {
        if (this.GetPrevPosX() > this.PosX)
        {
            this.CharRepresentation = 'L';
        }
        else if (GetPrevPosX() < this.PosX)
        {
            this.CharRepresentation = 'R';
        }
        else if (GetPrevPosY() < this.PosY)
        {
            this.CharRepresentation = 'D';
        }
        else if (GetPrevPosY() > this.PosY)
        {
            this.CharRepresentation = 'U';
        }
    }
}