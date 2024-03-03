using EbinApi.Contexts;
using EbinApi.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace EbinApi.Services
{
    public class CompanyService(EbinContext context)
    {
        private readonly EbinContext _context = context;

        public async Task<List<Company>> GetCompaniesShortInfo()
        {
            return await _context.Companies
                .Select(company => new Company()
                {
                    Id = company.Id,
                    Name = company.Name
                })
                .ToListAsync();
        }
    }
}