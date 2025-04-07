public class Entity
{
    public int Id { get; set; }

    public static int NextId { get; private set; } = 0;

    public Entity()
    {
        Id = NextId++;
    }

    public static void ResetNextId()
    {
        NextId = 0;
    }
}