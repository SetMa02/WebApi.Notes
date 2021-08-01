using System;
using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using Notes.Persistance;
namespace Notes.Tests.Common
{
    public class NotesContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();
        
        public static Guid NotesIdForDelete = Guid.NewGuid();
        public static Guid NotesIdForUpdate = Guid.NewGuid();

        public static NotesDbContext Create()
        {
            var options = new DbContextOptionsBuilder<NotesDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new NotesDbContext(options);
            context.Database.EnsureCreated();
            context.AddRange(
                new Note
                {
                    CreatingDate = DateTime.Today,
                    Details = "Details1",
                    EditDate = null,
                    Id = Guid.Parse("09893E42-EB1B-4BA6-B79D-9B47DEB18E05"),
                    Title = "Title1",
                    UserId = UserAId
                },
                new Note
                {
                    CreatingDate = DateTime.Today,
                    Details = "Details2",
                    EditDate = null,
                    Id = Guid.Parse("{337312C6-57E7-4026-986D-44D32B21953F}"),
                    Title = "Title2",
                    UserId = UserBId
                },
                new Note
                {
                    CreatingDate = DateTime.Today,
                    Details = "Details3",
                    EditDate = null,
                    Id = NotesIdForDelete,
                    Title = "Title3",
                    UserId = UserAId
                },
                new Note
                {
                    CreatingDate = DateTime.Today,
                    Details = "Details4",
                    EditDate = null,
                    Id = NotesIdForUpdate,
                    Title = "Title4",
                    UserId = UserBId
                }   
                );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(NotesDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
        
    }
}