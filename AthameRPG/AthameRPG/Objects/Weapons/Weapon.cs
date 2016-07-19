namespace AthameRPG.Objects.Weapons
{
    public abstract class Weapon
    {
        private int damage;

        protected Weapon()
        {
            
        }

        public virtual int Damage
        {
            get { return this.damage; }
            protected set { this.damage = value; }
        }


    }
}
