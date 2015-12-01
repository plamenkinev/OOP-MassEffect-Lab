using MassEffect.GameObjects.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassEffect.Interfaces;
using MassEffect.GameObjects.Projectiles;

namespace MassEffect.GameObjects.Ships
{
    public class Dreadnought : Starship
    {
        public Dreadnought(string name, StarSystem location)
            : base(name, 0, 0, 0, 0.0, location)
        {
        }

        public override IProjectile ProduceAttack()
        {
            return new Laser((this.Shields / 2) + this.Damage);
        }
    }
}
