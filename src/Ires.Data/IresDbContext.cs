using Microsoft.EntityFrameworkCore;

namespace Ires.Data;

public class IresDbContext : DbContext
{
    public IresDbContext(DbContextOptions<IresDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }
}
