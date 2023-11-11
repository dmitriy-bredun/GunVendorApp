using SDK.Domain;
using SDK.Repositories.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDK.Repositories.InFile
{
    public class GunRepositoryInFile : IGunRepository
    {
        public Task Create(Gun gun)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Gun>> Find(GunSearchCriteria critetia)
        {
            throw new NotImplementedException();
        }

        public Task<Gun?> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Gun gun)
        {
            throw new NotImplementedException();
        }
    }
}
