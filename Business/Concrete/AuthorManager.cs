using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

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
        var authors = _authorDal.GetAll();
        return new SuccessDataResult<IEnumerable<Author>>(authors);
    }

    public IDataResult<Author> GetById(int id)
    {
        var author = _authorDal.GetById(id);
        if (author == null)
        {
            return new ErrorDataResult<Author>("Author not found");
        }

        return new SuccessDataResult<Author>(author);
    }

    public IDataResult<IEnumerable<ThesisDetailDto>> GetThesesByAuthorId(int id)
    {
        var result = _authorDal.GetThesesByAuthorId(id);
        return new SuccessDataResult<IEnumerable<ThesisDetailDto>>(result);
    }

    [ValidationAspect(typeof(AuthorValidator), Priority = 1)]
    public IDataResult<Author> Add(Author author)
    {
        var addedAuthor = _authorDal.Add(author);
        return new SuccessDataResult<Author>(addedAuthor);
    }

    public IDataResult<Author> Update(Author author)
    {
        var updatedAuthor = _authorDal.Update(author);
        return new SuccessDataResult<Author>(updatedAuthor);
    }

    public IResult Delete(int id)
    {
        var author = _authorDal.GetById(id);
        if (author == null)
        {
            return new ErrorResult("Author not found");
        }

        _authorDal.Delete(author);
        return new SuccessResult();
    }

    public IDataResult<long> GetCount()
    {
        var count = _authorDal.GetCount();
        return new SuccessDataResult<long>(count);
    }
}