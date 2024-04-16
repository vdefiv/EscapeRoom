namespace libs;

public class Target : GameObject {

    public Target () : base(){
        Type = GameObjectType.Player;
        CharRepresentation = '*';
        Color = ConsoleColor.DarkGreen;
    }
}