using SDK.Repositories.InMemory;

namespace SDK.xUnitTests
{
    public class GunRepositoryTests
    {
        private IGunRepository gunRepository;

        public GunRepositoryTests(IGunRepository repository)
        {
            gunRepository = repository;
        }

        [Fact]
        
        public async void After_Create_Find_Should_Return_1Record()
        {
            var gun = new Gun("P38", 100, 350, GunType.Pistol, ElementalEffects.Incendiary);
            await gunRepository.Create(gun);
            var criteria = new GunSearchCriteria();
            var exteptedCpoutOfGuns = 1;

            var result = await gunRepository.Find(criteria);

            Assert.NotNull(result);
            Assert.Equal(exteptedCpoutOfGuns, result.Count());
        }

        [Fact]
        public async void FindByType_Should_Works_Properly()
        {
            var exteptedCountOfGuns = 3;
            var gun1 = new Gun("Sniper_Rifle_1", 800, 7000, GunType.SniperRifles, ElementalEffects.Incendiary);
            var gun2 = new Gun("Sniper_Rifle_2", 735, 6500, GunType.SniperRifles, ElementalEffects.Corrosive);
            var gun3 = new Gun("Sniper_Rifle_3", 915, 10000, GunType.SniperRifles, ElementalEffects.Slag);
            await gunRepository.Create(gun1);
            await gunRepository.Create(gun2);
            await gunRepository.Create(gun3);

            var criteria = new GunSearchCriteria()
            {
                Type = GunType.SniperRifles
            };

            var result = await gunRepository.Find(criteria);

            Assert.NotNull(result);
            Assert.Equal(exteptedCountOfGuns, result.Count());
        }

        [Fact]
        public async void Get_Should_Return_Record_ById()
        {
            var id = Guid.NewGuid().ToString();
            var gun = Gun.FromPersistance(id, "P38", 100, 350, GunType.Pistol, ElementalEffects.Incendiary, DateTime.Now);

            await gunRepository.Create(gun);

            var result = await gunRepository.Get(id);

            Assert.NotNull(result);
            Assert.Equal(gun.Id, result.Id);
            Assert.Equal(gun.Name, result.Name);
            Assert.Equal(gun.DamagePerSecond, result.DamagePerSecond);
            Assert.Equal(gun.Price, result.Price);
            Assert.Equal(gun.Type, result.Type);
            Assert.Equal(gun.ElementalEffects, result.ElementalEffects);
            Assert.Equal(gun.RegistrationDateTime, result.RegistrationDateTime);
        }

        [Fact]
        public async void Get_ShouldNot_Return_Record_ById_If_Id_IsNotExist()
        {
            var id = Guid.NewGuid().ToString();
            var gun = Gun.FromPersistance(id, "P38", 100, 350, GunType.Pistol, ElementalEffects.Incendiary, DateTime.Now);

            await gunRepository.Create(gun);

            var fakeId = Guid.NewGuid().ToString();
            var gunFromRepository = await gunRepository.Get(fakeId);

            Assert.Null(gunFromRepository);
        }
    }
}