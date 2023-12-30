using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework;

public class EfUniversityDal : EfEntityRepositoryBase<University, MyDbContext>, IUniversityDal
{
    private readonly MyDbContext _context;

    public EfUniversityDal(MyDbContext context)
    {
        _context = context;
    }

    public IEnumerable<UniversityDetailDto> GetListDetailDto(Expression<Func<UniversityDetailDto, bool>> expression = null)
    {
        var result = from u in _context.Universities
            join l in _context.Locations on u.LocationId equals l.Id
            select new UniversityDetailDto
            {
                Id = u.Id,
                Name = u.Name,
                LocationId = l.Id,
                City = l.City,
                Country = l.Country
            };

        return expression == null
            ? result.AsNoTracking()
            : result.Where(expression).AsNoTracking();
    }

    public UniversityDetailDto GetDetailDto(Expression<Func<UniversityDetailDto, bool>> expression)
    {
        var result = from u in _context.Universities
            join l in _context.Locations on u.LocationId equals l.Id
            select new UniversityDetailDto
            {
                Id = u.Id,
                Name = u.Name,
                LocationId = l.Id,
                City = l.City,
                Country = l.Country
            };

        return result.FirstOrDefault(expression);
    }

    public IEnumerable<InstituteDetailDto> GetInstitutesByUniversityId(int id)
    {
        var result = _context.Institutes
            .Where(i => i.UniversityId == id)
            .Select(i => new InstituteDetailDto
            {
                Id = i.Id,
                Name = i.Name,
                UniversityId = i.UniversityId,
                UniversityName = i.University.Name
            });

        return result.AsNoTracking();
    }
}
