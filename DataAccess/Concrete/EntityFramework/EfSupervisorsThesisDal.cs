using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Concrete.EntityFramework;

public class EfSupervisorsThesisDal : EfEntityRepositoryBase<SupervisorsThesis, MyDbContext>, ISupervisorsThesisDal
{
    
}