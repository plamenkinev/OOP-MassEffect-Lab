using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassEffect.Interfaces;

namespace MassEffect.GameObjects.Projectiles
{
    class Laser : Projectile
    {
        public Laser(int damage)
            : base(damage)
        {
        }

        public override void Hit(IStarship ship)
        {
            if (ship.Shields < this.Damage)
            {
                if (ship.Shields > 0)
                {
                    ship.Health -= this.Damage - ship.Shields;
                    ship.Shields = 0;
                }
                else
                {
                    ship.Health -= this.Damage;
                }
            }
            else
            {
                ship.Shields -= this.Damage;
            }
        }
    }
}
