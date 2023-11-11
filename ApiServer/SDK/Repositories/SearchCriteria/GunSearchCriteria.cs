using SDK.Domain;

namespace SDK.Repositories.SearchCriteria
{
    public class GunSearchCriteria
    {
        public string Name { get; set; } = "";
        public GunType? Type { get; set; }
        public uint? MinDamagePerSecond { get; set; }
        public uint? MinPrice { get; set; }
        public uint? MaxPrice { get; set; }
        public ElementalEffects? ElementalEffects { get; set; }
    }
}
