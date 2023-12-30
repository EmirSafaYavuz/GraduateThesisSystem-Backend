using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace DataAccess.Concrete.EntityFramework;

public class EfInstituteDal : EfEntityRepositoryBase<Institute, MyDbContext>, IInstituteDal
{
    private readonly MyDbContext _context;

    public EfInstituteDal(MyDbContext context)
    {
        _context = context;
    }

    public IEnumerable<InstituteDetailDto> GetListDetailDto()
    {
        var result = from i in _context.Institutes
            join u in _context.Universities on i.UniversityId equals u.Id
            select new InstituteDetailDto
            {
                Id = i.Id,
                Name = i.Name,
                UniversityId = u.Id,
                UniversityName = u.Name
            };
        return result;
    }
}