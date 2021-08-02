using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Persistance;
using Notes.Tests.Common;
using Notes.Tests.Notes.Commands;
using Shouldly;
using Xunit;

namespace Notes.Tests.Notes.Queries
{
    [Collection("QueryCollection")]
    public class GetNoteDetailsQueryHandlerTests
    {
        private readonly NotesDbContext Context;
        private readonly IMapper Mapper;

        public GetNoteDetailsQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetNotesDetailsQueryHandler_Success()
        {
            //Arrange
            var handler = new GetNoteDetailsQueryHandler(Context, Mapper);

            //Act
            var result = await handler.Handle(
                new GetNoteDetailsQuery
                {
                    UserId = NotesContextFactory.UserBId,
                    Id = Guid.Parse("337312C6-57E7-4026-986D-44D32B21953F")
                }, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<NoteDetailsVm>();
            result.Tile.ShouldBe("Title2");
            result.CreationDate.ShouldBe(DateTime.Today);
        }
    }
}