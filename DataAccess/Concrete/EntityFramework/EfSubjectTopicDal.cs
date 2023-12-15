using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Concrete.EntityFramework;

public class EfSubjectTopicDal : EfEntityRepositoryBase<SubjectTopic, MyDbContext>, ISubjectTopicDal
{
    public EfSubjectTopicDal(MyDbContext context) : base(context)
    {
    }
}