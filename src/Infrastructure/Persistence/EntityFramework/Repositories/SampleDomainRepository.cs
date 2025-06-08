using CleanArchitectureTemplate.Domain.SampleDomains;
using CleanArchitectureTemplate.Domain.SampleDomains.Enums;
using CleanArchitectureTemplate.Domain.SampleDomains.Interfaces;
using CleanArchitectureTemplate.Domain.BuildingBlocks.BaseTypes;
using CleanArchitectureTemplate.Infrastructure.Persistence.EntityFramework.Repositories.Base;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.EntityFramework.Repositories
{
    public class SampleDomainRepository(ApplicationDbContext dbContext) : BaseRepository<SampleDomain, int>(dbContext), ISampleDomainRepository
    {
        public Task<PageList<SampleDomain>> SearchAsync(string search, PageOption<SampleDomainSorting> pagedOptions)
        {
            IQueryable<SampleDomain> query = dbSet.AsQueryable();
            query = AppendSorting(query, pagedOptions);

            return GetPagedResultAsync(query, pagedOptions.PageNumber, pagedOptions.PageSize);
        }

        #region Private Methods

        private static IQueryable<SampleDomain> AppendSorting(IQueryable<SampleDomain> query, PageOption<SampleDomainSorting> pagedOptions)
        {
            switch (pagedOptions.SortingBy)
            {
                case SampleDomainSorting.Name:
                    {
                        return pagedOptions.SortingType == PageSortingType.Ascending
                            ? query.OrderBy(q => q.Name)
                            : query.OrderByDescending(q => q.Name);
                    }
                case SampleDomainSorting.Id:
                    {
                        return pagedOptions.SortingType == PageSortingType.Ascending
                            ? query.OrderBy(q => q.Id)
                            : query.OrderByDescending(q => q.Id);
                    }
                default:
                    return query.OrderByDescending(q => q.Auditing.CreatedAt);
            }
        }

        #endregion
    }
}
