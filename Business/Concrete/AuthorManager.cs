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
        var authors = _authorDal.GetList();
        return new SuccessDataResult<IEnumerable<Author>>(authors);
    }

    public IDataResult<Author> Add(Author author)
    {
        var addedAuthor = _authorDal.Add(author);
        _authorDal.SaveChanges();
        return new SuccessDataResult<Author>(addedAuthor);
    }

    public async Task<IResult> Delete(int id)
    {
        var author = await _authorDal.GetAsync(a => a.Id == id);
        if (author == null)
        {
            return new ErrorResult("Author not found");
        }

        _authorDal.Delete(author);
        await _authorDal.SaveChangesAsync();
        return new SuccessResult();
    }
}