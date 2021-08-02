using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exeptions;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Persistance;
using Notes.Tests.Common;
using Xunit;

namespace Notes.Tests.Notes.Commands
{
    public class UpdateNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateNoteCommandHandler_Success()
        {
            //Arrange
            var handler = new UpdateNoteCommandHandler(Context);
            var updatedTitle = "new Title";

            //Act
            await handler.Handle(new UpdateNoteCommand
            {
                Id = NotesContextFactory.NotesIdForUpdate,
                UserId = NotesContextFactory.UserBId,
                Title = updatedTitle
            }, CancellationToken.None);

            //Assert
            Assert.NotNull(await Context.Notes.SingleOrDefaultAsync(note => 
                note.Id == NotesContextFactory.NotesIdForUpdate && note.Title == updatedTitle));
        }

        [Fact]
        public async Task UpdateNoteCommandHandler_FailOnWrongId()
        {
            //Arrange
            var handle = new UpdateNoteCommandHandler(Context);

            //Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handle.Handle(
                    new UpdateNoteCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = NotesContextFactory.UserAId
                    }, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateNoteCommandHandler_FailOnWrongUserId()
        {
            //Arrange
            var handler = new UpdateNoteCommandHandler(Context);

            //Act
            
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(
                    new UpdateNoteCommand
                    {
                        Id = NotesContextFactory.NotesIdForUpdate,
                        UserId = NotesContextFactory.UserAId
                    }, CancellationToken.None);
            });
        }
    }
}