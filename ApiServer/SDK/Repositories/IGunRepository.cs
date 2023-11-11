using SDK.Domain;
using SDK.Repositories.SearchCriteria;

namespace SDK.Repositories
{
    public interface IGunRepository
    {
        Task Create(Gun gun);
        Task<Gun?> Get(string id);
        Task<IEnumerable<Gun>> Find(GunSearchCriteria critetia);
        Task Update(Gun gun);
        Task Delete(string id);
    }
}
