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
    public class Frigate : Starship
    {
        private int projectilesFired;

        public Frigate(string name, StarSystem location)
            : base(name, 0, 0, 0, 0.0, location)
        {
        }

        public override IProjectile ProduceAttack()
        {
            this.projectilesFired++;
            return new ShieldReaver(this.Damage);
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine(base.ToString());
            if (this.Health > 0)
                output.AppendLine(String.Format("-Projectiles fired: {0}", projectilesFired));

            return output.ToString();
        }
    }
}
