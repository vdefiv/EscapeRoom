namespace libs;

public class Target : GameObject {

    public Target () : base(){
        Type = GameObjectType.Player;
        CharRepresentation = 'I';
        Color = ConsoleColor.Cyan;
    }
}