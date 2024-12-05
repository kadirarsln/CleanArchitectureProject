
namespace App.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();

}
