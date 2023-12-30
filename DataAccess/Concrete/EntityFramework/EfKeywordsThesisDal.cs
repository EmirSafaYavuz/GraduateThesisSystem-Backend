using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Concrete.EntityFramework;

public class EfKeywordsThesisDal : EfEntityRepositoryBase<KeywordsThesis, MyDbContext>, IKeywordsThesisDal
{
    
}