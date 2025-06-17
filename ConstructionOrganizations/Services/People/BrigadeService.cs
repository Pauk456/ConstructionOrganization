using ConstructionOrganizations.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Services.People
{
    public class BrigadeService
    {
        private readonly AppDbContext _context;

        public BrigadeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Brigade> CreateAsync(Brigade brigade)
        {
            _context.Brigades.Add(brigade);
            await _context.SaveChangesAsync();
            return brigade;
        }

        public async Task<object> GetByIdAsync(int id)
        {
            var brigade = await _context.Brigades
                .Include(b => b.Members)
                .Include(b => b.BrigadeWorkAssignments)
                    .ThenInclude(bwa => bwa.WorkSchedule)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (brigade == null) return null;

            var memberIds = brigade.Members.Select(m => m.Id).ToList();
            var workScheduleIds = brigade.BrigadeWorkAssignments.Select(bwa => bwa.WorkScheduleId).ToList();

            return new
            {
                Id = brigade.Id,
                Members = memberIds,
                WorkScheduleIds = workScheduleIds
            };
        }
    }
}