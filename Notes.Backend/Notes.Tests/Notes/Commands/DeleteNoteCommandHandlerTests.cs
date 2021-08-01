using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exeptions;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteCommand;
using Notes.Tests.Common;
using Xunit;

namespace Notes.Tests.Notes.Commands
{
    public class DeleteNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteNoteCommandHandler_Success()
        {
            //Arrange
            var handler = new DeleteNoteCommandHandler(Context);
            
            //Act
            await handler.Handle(new DeleteNoteCommand
            {
                Id = NotesContextFactory.NotesIdForDelete,
                UserId = NotesContextFactory.UserAId
            }, CancellationToken.None);
            
            //Assert
            Assert.Null(Context.Notes.SingleOrDefault(note => 
                note.Id == NotesContextFactory.NotesIdForDelete));
        }

        [Fact]
        public async Task DeleteNoteCommandHandler_FailOnWrongId()
        {
            //Arrange
            var handler = new DeleteNoteCommandHandler(Context);

            //Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteNoteCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = NotesContextFactory.UserAId
                    }, CancellationToken.None));
        }
        
        [Fact]
        public async Task DeleteNoteCommandHandler_FailOnWrongUserId()
        {
            //Arrange
            var deleteHandler = new DeleteNoteCommandHandler(Context);
            var createHandler = new CreateNoteCommandHandler(Context);
            var noteId = await createHandler.Handle(
                new CreateNoteCommand
                {
                    Title = "Note title",
                    UserId = NotesContextFactory.UserAId
                }, CancellationToken.None);

            //Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteNoteCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = NotesContextFactory.UserAId
                    }, CancellationToken.None));
        }
    }
}