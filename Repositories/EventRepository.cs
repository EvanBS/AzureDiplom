﻿using Diplom.Data;
using Diplom.DataModels;
using Diplom.Services.Communication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Repositories
{
    public class EventRepository : BaseRepository, IEventRepository
    {
        public EventRepository(DiplomDatabaseContext context) : base(context) { }

        public Task<List<DateTime?>> DatesListAsync()
        {
            return _context.Events.Select(e => e.Date).ToListAsync();
        }

        public async Task<IEnumerable<Event>> ListAsync()
        {
            return await _context.Events.Include(e => e.EventPosition).ToListAsync();
        }

        public async Task<IEnumerable<Event>> ListAsync(string eventName)
        {
            return await _context.Events.Include(e => e.EventPosition).Where(e => e.EventName == eventName).ToListAsync();
        }

        public async Task<IEnumerable<Event>> ListAsync(DateTime date)
        {
            return await _context.Events.Include(e => e.EventPosition).Where(e => e.Date.Value.Year == date.Year).ToListAsync();
        }

        public async Task<UpdateEventResponse> UpdateEventAsync(Event @event)
        {
            try
            {
                _context.Events.Update(@event);
                _context.SaveChanges();

                return new UpdateEventResponse();
            }
            catch (Exception ex)
            {
                return new UpdateEventResponse(false, $"An error occurred when updating event: {ex.Message}");
            }
        }
    }
}