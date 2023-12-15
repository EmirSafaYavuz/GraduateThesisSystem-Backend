using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete;

public class AuthorManager : IAuthorService
{
    private readonly IAuthorDal _authorDal;

    public AuthorManager(IAuthorDal authorDal)
    {
        _authorDal = authorDal;
    }

    public IDataResult<IEnumerable<Author>> GetAll()
    {
        var authorsWithTheses = _authorDal.Query().Include(a => a.Theses).ToList();
        return new SuccessDataResult<IEnumerable<Author>>(authorsWithTheses);
    }
}