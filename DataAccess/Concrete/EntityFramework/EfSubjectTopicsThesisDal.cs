using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Concrete.EntityFramework;

public class EfSubjectTopicsThesisDal : EfEntityRepositoryBase<SubjectTopicsThesis, MyDbContext>, ISubjectTopicsThesisDal
{
    
}