namespace PopulationPortal.Application.Infrastructures
{
    public interface IApplicationDbContextFactory 
    {
        public IApplicationDbContext CreateDbContext();
    }
}
