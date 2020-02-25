using Common.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore
{
    public class DataBaseInitializer : IDataBaseInitializer
    {
        private readonly DataContext _context;
        public DataBaseInitializer(DataContext context)
        {
            this._context = context;
        }

        public void Initialize()
        {
            using (_context)
            {
                // turn off timeout for initial seeding
                this._context.Database.SetCommandTimeout(System.TimeSpan.FromDays(1));
                // check data base version and migrate / seed if needed
                this._context.Database.Migrate();
            }
        }
    }
}