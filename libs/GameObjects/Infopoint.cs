namespace libs;

public class Infopoint : GameObject {
    public Infopoint () : base() {
        this.Type = GameObjectType.Infopoint;
        this.CharRepresentation = '◙';
        this.Color = ConsoleColor.Cyan;
    }
}