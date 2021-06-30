﻿using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using Notes.Domain;
using Notes.Persistance.EntityTypeConfigurations;

namespace Notes.Persistance
{
    public class NotesDbContext : DbContext , INoteDbContext
    {
        public DbSet<Note> Notes { get; set; }
        
        public NotesDbContext(DbContextOptions<NotesDbContext> options)
            : base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new NoteConfiguration());
            base.OnModelCreating(builder);
        }
    }
}