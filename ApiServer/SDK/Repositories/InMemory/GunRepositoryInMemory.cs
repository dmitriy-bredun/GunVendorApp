using SDK.Domain;
using SDK.Repositories.SearchCriteria;
using System.Collections.Concurrent;

namespace SDK.Repositories.InMemory
{
    public class GunRepositoryInMemory : IGunRepository
    {
        private static ConcurrentDictionary<string, Gun> storage = new ConcurrentDictionary<string, Gun>();

        public Task Create(Gun gun)
        {
            var id = gun.Id.ToString();
            storage.TryAdd(id, gun);
            return Task.CompletedTask;
        }
        public Task<Gun?> Get(string id)

        {
            storage.TryGetValue(id, out var result);
            return Task.FromResult(result);
        }

        public Task<IEnumerable<Gun>> Find(GunSearchCriteria critetia)
        {
            var records = (IEnumerable<Gun>)storage.Values;

            if (!string.IsNullOrEmpty(critetia.Name))
            {
                records = records.Where(rec => rec.Name.Contains(critetia.Name));
            }

            if (critetia.Type.HasValue)
            {
                records = records.Where(rec => rec.Type == critetia.Type);
            }

            if (critetia.MinDamagePerSecond.HasValue)
            {
                records = records.Where(rec => rec.DamagePerSecond >= critetia.MinDamagePerSecond);
            }

            if (critetia.MinPrice.HasValue)
            {
                records = records.Where(rec => rec.Price >= critetia.MinPrice);
            }

            if (critetia.MaxPrice.HasValue)
            {
                records = records.Where(rec => rec.Price <= critetia.MaxPrice);
            }

            if (critetia.ElementalEffects.HasValue)
            {
                records = records.Where(rec => rec.ElementalEffects == critetia.ElementalEffects);
            }

            return Task.FromResult(records);
        }

        public Task Update(Gun gun)
        {
            if (!storage.TryGetValue(gun.Id.ToString(), out var existedGun))
            {
                throw new Exception("Record with such Id was not found");
            }

            var updatedGun = Gun.FromPersistance(
                    existedGun.Id.ToString(),
                    gun.Name,
                    existedGun.DamagePerSecond,
                    gun.Price,
                    existedGun.Type,
                    existedGun.ElementalEffects,
                    existedGun.RegistrationDateTime);

            var Id = existedGun.Id.ToString();

            storage[Id] = updatedGun;

            return Task.CompletedTask;
        }

        public Task Delete(string id)
        {
            storage.TryRemove(id, out var result);
            return Task.CompletedTask;
        }
    }
}
