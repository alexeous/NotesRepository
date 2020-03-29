﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesRepository.Models
{
    interface INoteRepository
    {
        Task<Note> GetByIdAsync(int id);
        Task<List<Note>> GetAllAsync();
        Task<List<TitleOnlyNote>> GetAllTitleOnlyNotesAsync();
        Task AddAsync(Note note);
        Task UpdateAsync(Note note);
        Task RemoveByIdAsync(int id);
    }
}
