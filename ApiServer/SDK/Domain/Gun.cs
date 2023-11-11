namespace SDK.Domain
{
    public class Gun
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = "";
        public uint DamagePerSecond { get; private set; }
        public uint Price { get; private set; }
        public GunType Type { get; private set; }
        public ElementalEffects ElementalEffects { get;private set;}
        public DateTime RegistrationDateTime {  get; private set; }

        private const uint maxDmgValue = 1000;

        public Gun(string name, uint dmg, uint price, GunType gunType, ElementalEffects elementalEffects)
            : this(null, name, dmg, price, gunType, elementalEffects, null)
        {   }

        private Gun(string? id, string name, uint dmg, uint price, GunType gunType, ElementalEffects elementalEffects, DateTime? registrationDate)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Guns name can not be empty or null");
            }

            if (dmg > maxDmgValue)
            {
                throw new ArgumentOutOfRangeException($"dmg can not be more than {maxDmgValue}");
            }

            Id = String.IsNullOrEmpty(id) ? Guid.NewGuid() : Guid.Parse(id);
            
            Name = name;
            DamagePerSecond = dmg;
            Price = price;
            Type = gunType;
            ElementalEffects = elementalEffects;

            RegistrationDateTime = registrationDate.HasValue ? registrationDate.Value : DateTime.Now;
        }

        public static Gun FromPersistance (string id, string name, uint dmg, uint price, GunType gunType, ElementalEffects elementalEffects, DateTime registrationDate)
        {
            return new Gun(id, name, dmg, price, gunType, elementalEffects, registrationDate);
        }
    }
}