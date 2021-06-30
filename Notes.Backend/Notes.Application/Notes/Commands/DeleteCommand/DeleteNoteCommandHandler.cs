﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Notes.Application.Common.Exeptions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.DeleteCommand
{
    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
    {
        private readonly INoteDbContext _dbContext;

        public DeleteNoteCommandHandler(INoteDbContext dbContext) => _dbContext = dbContext;
        
        public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Notes.FindAsync(new object[] {request.Id}, cancellationToken);
            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            _dbContext.Notes.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return  Unit.Value;
        }
    }
}