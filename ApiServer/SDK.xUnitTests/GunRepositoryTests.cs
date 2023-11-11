using SDK.Domain;
using SDK.Repositories.InMemory;

namespace SDK.xUnitTests
{
    public class GunRepositoryTests
    {
        private IGunRepository gunRepository;

        public GunRepositoryTests(IGunRepository repository)
        {
            gunRepository = repository;
            ClearRepository();
        }

        [Fact]

        public async void After_Create_Find_Should_Return_1RecordWithCorrectFields()
        {
            var exteptedCountOfGuns = 1;
            var gunName = "P38";
            uint damage = 100;
            uint price = 350;
            var gunType = GunType.Pistol;
            var elementalEffects = ElementalEffects.Incendiary;
            var gun = new Gun(gunName, damage, price, gunType, elementalEffects);
            await gunRepository.Create(gun);

            var results = await gunRepository.Find(new GunSearchCriteria());
            var resultGun = results.FirstOrDefault();

            Assert.Equal(exteptedCountOfGuns, results.Count());
            Assert.NotNull(resultGun);
            Assert.Equal(gunName, resultGun.Name);
            Assert.Equal(damage, resultGun.DamagePerSecond);
            Assert.Equal(price, resultGun.Price);
            Assert.Equal(gunType, resultGun.Type);
            Assert.Equal(elementalEffects, resultGun.ElementalEffects);
        }

        [Theory]
        [InlineData("TestGunName")]
        [InlineData("SuperBigDick")]
        [InlineData("LuckTaker")]
        public async void FindByName_Works_Properly(string expectedGunName)
        {
            var expectedCountOfGuns = 1;
            var gun1 = new Gun(expectedGunName, 50, 500, GunType.Pistol, ElementalEffects.None);
            var gun2 = new Gun("FakeGun1", 100, 1000, GunType.Pistol, ElementalEffects.None);
            var gun3 = new Gun("FakeGun2", 100, 1000, GunType.Pistol, ElementalEffects.None);
            var gun4 = new Gun("FakeGun3", 100, 1000, GunType.Pistol, ElementalEffects.None);
            AddRecordsToRepository(new List<Gun>() { gun1, gun2, gun3, gun4 });
            var criteria = new GunSearchCriteria() { Name = expectedGunName };

            var result = await gunRepository.Find(criteria);

            Assert.NotNull(result);
            Assert.Equal(expectedCountOfGuns, result.Count());
            Assert.Equal(expectedGunName, result.First().Name);
        }

        [Theory]
        [InlineData(GunType.Shotguns, 1)]
        [InlineData(GunType.CombatRifles, 1)]
        [InlineData(GunType.SniperRifles, 3)]
        public async void FindByType_Works_Properly(GunType expectedGunType, int expectedCountOfRecords)
        {
            var gun1 = new Gun("GunName", 100, 1000, GunType.Shotguns,    ElementalEffects.None);
            var gun2 = new Gun("GunName", 100, 1000, GunType.CombatRifles, ElementalEffects.None);
            var gun3 = new Gun("GunName", 100, 1000, GunType.SniperRifles, ElementalEffects.None);
            var gun4 = new Gun("GunName", 100, 1000, GunType.SniperRifles, ElementalEffects.None);
            var gun5 = new Gun("GunName", 100, 1000, GunType.SniperRifles, ElementalEffects.None);
            AddRecordsToRepository(new List<Gun>() { gun1, gun2, gun3, gun4, gun5 });
            var criteria = new GunSearchCriteria() { Type = expectedGunType };

            var results = await gunRepository.Find(criteria);

            Assert.NotNull(results);
            Assert.Equal(expectedCountOfRecords, results.Count());
            foreach (var record in results)
            {
                Assert.Equal(expectedGunType, record.Type);
            }
        }

        [Theory]
        [InlineData(0, 5)]
        [InlineData(400, 4)]
        [InlineData(800, 2)]
        [InlineData(1000, 1)]
        public async void FindByMinDamagePerSecond_Works_Properly(uint minDMGSearchCriteria, int expectedCountOfRecords)
        {
            var gun1 = new Gun("GunName", 200, 1000, GunType.Pistol, ElementalEffects.None);
            var gun2 = new Gun("GunName", 400, 1000, GunType.Pistol, ElementalEffects.None);
            var gun3 = new Gun("GunName", 600, 1000, GunType.Pistol, ElementalEffects.None);
            var gun4 = new Gun("GunName", 800, 1000, GunType.Pistol, ElementalEffects.None);
            var gun5 = new Gun("GunName", 1000, 1000, GunType.Pistol, ElementalEffects.None);
            AddRecordsToRepository(new List<Gun>() { gun1, gun2, gun3, gun4, gun5 });
            var criteria = new GunSearchCriteria() { MinDamagePerSecond = minDMGSearchCriteria };

            var results = await gunRepository.Find(criteria);

            Assert.NotNull(results);
            Assert.Equal(expectedCountOfRecords, results.Count());
            foreach (var record in results)
            {
                Assert.True(record.DamagePerSecond >= minDMGSearchCriteria);
            }
        }

        [Theory]
        [InlineData(0, 500, 3)]
        [InlineData(500, 1000, 4)]
        public async void FindByPrice_Works_Properly(uint minPriceSearchCriteria, uint maxPriceSearchCriteria, int expectedCountOfRecords)
        {
            var gun1 = new Gun("GunName", 200, 100, GunType.Pistol, ElementalEffects.None);
            var gun2 = new Gun("GunName", 200, 250, GunType.Pistol, ElementalEffects.None);
            var gun3 = new Gun("GunName", 200, 500, GunType.Pistol, ElementalEffects.None);
            var gun4 = new Gun("GunName", 200, 501, GunType.Pistol, ElementalEffects.None);
            var gun5 = new Gun("GunName", 200, 600, GunType.Pistol, ElementalEffects.None);
            var gun6 = new Gun("GunName", 200, 1000, GunType.Pistol, ElementalEffects.None);
            AddRecordsToRepository(new List<Gun>() { gun1, gun2, gun3, gun4, gun5, gun6 });
            var criteria = new GunSearchCriteria() { MinPrice = minPriceSearchCriteria, MaxPrice = maxPriceSearchCriteria };

            var results = await gunRepository.Find(criteria);

            Assert.NotNull(results);
            Assert.Equal(expectedCountOfRecords, results.Count());
            foreach (var record in results)
            {
                Assert.True(minPriceSearchCriteria <= record.Price && record.Price <= maxPriceSearchCriteria);
            }
        }

        [Theory]
        [InlineData(ElementalEffects.None, 1)]
        [InlineData(ElementalEffects.Incendiary, 1)]
        [InlineData(ElementalEffects.Explosive, 3)]
        public async void FindByElementalEffects_Works_Properly(ElementalEffects expectedElementalType, int expectedCountOfRecords)
        {
            var gun1 = new Gun("GunName", 100, 1000, GunType.Pistol, ElementalEffects.None);
            var gun2 = new Gun("GunName", 100, 1000, GunType.Pistol, ElementalEffects.Incendiary);
            var gun3 = new Gun("GunName", 100, 1000, GunType.Pistol, ElementalEffects.Explosive);
            var gun4 = new Gun("GunName", 100, 1000, GunType.Pistol, ElementalEffects.Explosive);
            var gun5 = new Gun("GunName", 100, 1000, GunType.Pistol, ElementalEffects.Explosive);
            AddRecordsToRepository(new List<Gun>() { gun1, gun2, gun3, gun4, gun5 });
            var criteria = new GunSearchCriteria() { ElementalEffects = expectedElementalType };

            var results = await gunRepository.Find(criteria);

            Assert.NotNull(results);
            Assert.Equal(expectedCountOfRecords, results.Count());
            foreach (var record in results)
            {
                Assert.Equal(expectedElementalType, record.ElementalEffects);
            }
        }

        [Fact]
        public async void GetById_Should_Return_Record()
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

        [Fact]
        public async void Update_Should_Change_Only_Allowed_Fields()
        {
            var originalId = Guid.NewGuid().ToString();
            var originalGunName = "MG69";
            uint originalDamage = 100;
            uint originalPrice = 350;
            var originalGunType = GunType.Pistol;
            var originalElementalEffects = ElementalEffects.Incendiary;
            var originalRegistrationDateTime = DateTime.Now;

            var originalGun = Gun.FromPersistance(originalId, originalGunName, originalDamage, originalPrice, originalGunType, originalElementalEffects, originalRegistrationDateTime);
            await gunRepository.Create(originalGun);


            var updatedGunName = "KIRA_THE_BEST_KILLER";
            uint updatedDamage = 10;
            uint updatedPrice = 10;
            var updatedGunType = GunType.SubMachine;
            var updatedElementalEffects = ElementalEffects.None;
            var updatedRegistrationDateTime = originalRegistrationDateTime.AddDays(10);
            var updatedGun = Gun.FromPersistance
            (
                originalId,
                updatedGunName, //====
                updatedDamage, 
                updatedPrice,   //====
                updatedGunType,
                updatedElementalEffects,
                updatedRegistrationDateTime
            );

            await gunRepository.Update(updatedGun);

            var resultAfterUpdate = await gunRepository.Get(originalId);

            Assert.Equal(resultAfterUpdate.Id.ToString(), originalId);
            Assert.Equal(resultAfterUpdate.Name, updatedGunName);
            Assert.Equal(resultAfterUpdate.DamagePerSecond, originalDamage);
            Assert.Equal(resultAfterUpdate.Price, updatedPrice);
            Assert.Equal(resultAfterUpdate.Type, originalGunType);
            Assert.Equal(resultAfterUpdate.ElementalEffects, originalElementalEffects);
            Assert.Equal(resultAfterUpdate.RegistrationDateTime, originalRegistrationDateTime);
        }

        [Fact]
        public async Task Update_Should_ThrowException_WhenRecord_IsNotExist()
        {
            var originalId = Guid.NewGuid().ToString();
            var originalGunName = "MG69";
            uint originalDamage = 100;
            uint originalPrice = 350;
            var originalGunType = GunType.Pistol;
            var originalElementalEffects = ElementalEffects.Incendiary;
            var originalRegistrationDateTime = DateTime.Now;

            var originalGun = Gun.FromPersistance(originalId, originalGunName, originalDamage, originalPrice, originalGunType, originalElementalEffects, originalRegistrationDateTime);
            await gunRepository.Create(originalGun);

            var updatedGunName = "KIRA_THE_BEST_KILLER";
            uint updatedDamage = 10;
            uint updatedPrice = 10;
            var updatedGunType = GunType.SubMachine;
            var updatedElementalEffects = ElementalEffects.None;
            var updatedRegistrationDateTime = originalRegistrationDateTime.AddDays(10);
            var updatedGun = Gun.FromPersistance
            (
                Guid.NewGuid().ToString(),
                updatedGunName, //====
                updatedDamage,
                updatedPrice,   //====
                updatedGunType,
                updatedElementalEffects,
                updatedRegistrationDateTime
            );

            await Assert.ThrowsAsync<Exception>(() => gunRepository.Update(updatedGun));
        }

        [Fact]
        public async void Delete_Works_Properly()
        {
            var gun1 = new Gun("GunName", 100, 1000, GunType.Pistol, ElementalEffects.None);
            var gun2 = new Gun("GunName", 100, 1000, GunType.Pistol, ElementalEffects.None);
            var gun3 = new Gun("GunName", 100, 1000, GunType.Pistol, ElementalEffects.None);
            var gun4 = new Gun("GunName", 100, 1000, GunType.Pistol, ElementalEffects.None);
            var gun5 = new Gun("GunName", 100, 1000, GunType.Pistol, ElementalEffects.None);
            AddRecordsToRepository(new List<Gun>() { gun1, gun2, gun3, gun4, gun5 });

            var records = await gunRepository.Find(new GunSearchCriteria());
            foreach (var record in records)
            {
                await gunRepository.Delete(record.Id.ToString());
            }

            var result = await gunRepository.Find(new GunSearchCriteria());

            Assert.Empty(result);
        }


        private async void AddRecordsToRepository(IEnumerable<Gun> guns)
        {
            foreach (var gun in guns)
            {
                await gunRepository.Create(gun);
            }
        }
        private async void ClearRepository()
        {
            var records = await gunRepository.Find(new GunSearchCriteria());
            foreach (var record in records)
            {
                await gunRepository.Delete(record.Id.ToString());
            }
        }
    }
}