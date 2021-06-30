namespace Notes.Persistance
{
    public class DbInitializer
    {
        public static void Initialized(NotesDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}