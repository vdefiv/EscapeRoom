namespace libs;

public class GameObject : IGameObject, IMovement
{
    private char _charRepresentation = '#';
    private ConsoleColor _color;

    private int _posX;
    private int _posY;

    private int _prevPosX;
    private int _prevPosY;

    public GameObjectType Type;


    public GameObject()
    {
        this._posX = 5;
        this._posY = 5;
        this._color = ConsoleColor.Gray;
    }

    public GameObject(int posX, int posY)
    {
        this._posX = posX;
        this._posY = posY;
    }

    public GameObject(int posX, int posY, ConsoleColor color)
    {
        this._posX = posX;
        this._posY = posY;
        this._color = color;
    }

    public char CharRepresentation
    {
        get { return _charRepresentation; }
        set { _charRepresentation = value; }
    }

    public ConsoleColor Color
    {
        get { return _color; }
        set { _color = value; }
    }

    public int PosX
    {
        get { return _posX; }
        set { _posX = value; }
    }

    public int PosY
    {
        get { return _posY; }
        set { _posY = value; }
    }

    public int GetPrevPosY()
    {
        if (_prevPosY == null || _prevPosY == 0)
        {
            return _posY;
        }
        return _prevPosY;
    }

    public int GetPrevPosX()
    {
        if (_prevPosX == null || _prevPosX == 0)
        {
            return _posX;
        }
        return _prevPosX;
    }
    public void SetPrevPosY(int value)
    {
        _prevPosY = value;
    }

    public void SetPrevPosX(int value)
    {
        _prevPosX = value;
    }

    // public virtual void Move(int dx, int dy)
    // {
    //     _prevPosX = _posX;
    //     _prevPosY = _posY;
    //     _posX += dx;
    //     _posY += dy;
    // }
        //DIALOG STUFF
    public Dialog? dialog;
            public Map map = GameEngine.Instance.GetMap();

    protected List<DialogNode> dialogNodes = new List<DialogNode>();

    public virtual void Move(int dx, int dy) {
        
        int targetX = _posX + dx;
        int targetY = _posY + dy;
        int goToX = PosX + dx;
        int goToY = PosY + dy;

        GameObject? nextObject = map.Get(goToY, goToX);

        //Use LINQ to query objects in target Position.
        var collisionObjects = GameEngine.gameObjects
        .Where(e => e.PosX == targetX && e.PosY == targetY);

        //If no Obstacles found --> MOVE
        if(collisionObjects.Count() == 0){      
            _prevPosX = _posX;
            _prevPosY = _posY;
            _posX += dx;
            _posY += dy;
            return;
        }
         var objectCollidedWith = collisionObjects.First();
        
        Console.WriteLine("Collision with " + collisionObjects.First().Type);
        //if object is an Obstacle or the Target --> return
        if(objectCollidedWith.Type == GameObjectType.Obstacle || objectCollidedWith.Type == GameObjectType.Target ) return;

        //if object is a Box --> check if it can be moved
        if(objectCollidedWith.Type == GameObjectType.Box){
            GameObject? nextNextObject = map.Get(goToY + dy, goToX + dx);
            if(nextNextObject.Type == GameObjectType.Obstacle || nextNextObject.Type == GameObjectType.Box) return;
            objectCollidedWith.Move(dx, dy);
        }


        if(objectCollidedWith.HasDialog()) collisionObjects.First().dialog.Start();
        
        
    }

    public bool HasDialog(){
        return (dialog == null) ? false : true;
    }
}
