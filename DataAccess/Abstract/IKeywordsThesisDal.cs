using Core.DataAccess;
using DataAccess.Entities;

namespace DataAccess.Abstract;

public interface IKeywordsThesisDal : IEntityRepository<KeywordsThesis>
{
    IEnumerable<Keyword> GetKeywordsByThesisId(int id);
}