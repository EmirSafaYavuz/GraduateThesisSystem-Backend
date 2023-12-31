using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace DataAccess.Concrete.AdoNet;

public class AnKeywordDal : IKeywordDal
{
    public Keyword GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IList<Keyword> GetAll()
    {
        throw new NotImplementedException();
    }

    public Keyword Add(Keyword entity)
    {
        throw new NotImplementedException();
    }

    public Keyword Update(Keyword entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Keyword entity)
    {
        throw new NotImplementedException();
    }

    public long GetCount()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ThesisDetailDto> GetThesesByKeywordId(int id)
    {
        throw new NotImplementedException();
    }
}