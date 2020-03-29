﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NotesRepository.Models
{
    class NoteRepository : INoteRepository
    {
        private readonly NotesDbContext db;

        public NoteRepository(NotesDbContext db)
        {
            this.db = db;
        }

        public async Task<Note> AddAsync(NoteWithoutId noteWithoutId)
        {
            var note = new Note
            {
                Title = noteWithoutId.Title,
                Content = noteWithoutId.Content
            };
            await db.Notes.AddAsync(note);
            await db.SaveChangesAsync();
            
            return note;
        }

        public Task<List<Note>> GetAllAsync()
        {
            return db.Notes.AsNoTracking().ToListAsync();
        }

        public Task<List<NoteWithoutContent>> GetAllNotesWithoutContentAsync()
        {
            return db.Notes
                .Select(note => new NoteWithoutContent
                {
                    Id = note.Id,
                    Title = note.Title
                }).ToListAsync();
        }

        public Task<Note> GetByIdAsync(int id)
        {
            return db.Notes.AsNoTracking().Where(note => note.Id == id).FirstOrDefaultAsync();
        }

        public async Task RemoveByIdAsync(int id)
        {
            db.Notes.Remove(await db.Notes.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public Task UpdateAsync(Note note)
        {
            db.Notes.Update(note);
            return db.SaveChangesAsync();
        }
    }
}
