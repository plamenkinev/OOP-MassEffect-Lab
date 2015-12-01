using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassEffect.GameObjects.Enhancements;
using MassEffect.GameObjects.Locations;
using MassEffect.Interfaces;

namespace MassEffect.GameObjects.Ships
{
    public abstract class Starship : Interfaces.IStarship
    {
        private string name;
        private int health;
        private int shields;
        private int damage;
        private double fuel;
        private StarSystem location;
        private IList<Enhancement> enhancements;

        protected Starship(string name, int health, int shields, int damage, double fuel, StarSystem location)
        {
            this.Name = name;
            this.Health = health;
            this.Shields = shields;
            this.Damage = damage;
            this.Fuel = fuel;
        }

        public string Name { get; set; }

        public int Health { get; set; }

        public int Shields { get; set; }

        public int Damage { get; set; }

        public double Fuel { get; set; }

        public StarSystem Location { get; set; }
   
        public IEnumerable<Enhancement> Enhancements { get; set; }

        public void AddEnhancement(Enhancement enhancement)
        {
            if (enhancement == null)
                throw new ArgumentNullException("Enhancement cannot be null!");

            this.enhancements.Add(enhancement);

            // applying enhancements to the starship
            this.Shields += enhancement.ShieldBonus;
            this.Damage += enhancement.DamageBonus;
            this.Fuel += enhancement.FuelBonus;
        }

        public abstract IProjectile ProduceAttack();

        public void RespondToAttack(IProjectile attack)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine(String.Format("--{0} - {1}", this.Name, this.GetType().Name));

            if (this.Health <= 0)
            {
                output.Append("(Destroyed)");
            }
            else
            {
                output.AppendLine(String.Format("-Location: {0}", this.Location.Name));
                output.AppendLine(String.Format("-Health: {0}", this.Health));
                output.AppendLine(String.Format("-Shields: {0}", this.Shields));
                output.AppendLine(String.Format("-Damage: {0}", this.Damage));
                output.AppendLine(String.Format("-Fuel: {0}", this.Fuel));
                output.Append("Enhacements: ");
                foreach (Enhancement enhancement in enhancements)
                    output.Append(enhancement + ", ");
            }

            return output.ToString();
        }
    }
}
