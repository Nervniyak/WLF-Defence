namespace Assets.Scripts
{
    public class DamagePackage
    {
        public int Damage;
        public string OwnerName;

        public DamagePackage(int damage, string ownerName)
        {
            Damage = damage;
            OwnerName = ownerName;
        }
    }
}
