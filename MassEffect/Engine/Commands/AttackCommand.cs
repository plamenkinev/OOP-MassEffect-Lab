namespace MassEffect.Engine.Commands
{
    using System;
    using MassEffect.Interfaces;
    using System.Linq;
    using Exceptions;

    public class AttackCommand : Command
    {
        public AttackCommand(IGameEngine gameEngine)
            : base(gameEngine)
        {
        }

        public override void Execute(string[] commandArgs)
        {
            string attackerName = commandArgs[1];
            string targetName = commandArgs[2];
            IStarship attackingShip = null, targetShip = null;

            bool attackerShipAlreadyExists = this.GameEngine.Starships
                .Any(s => s.Name == attackerName);

            bool targetShipAlreadyExists = this.GameEngine.Starships
                .Any(s => s.Name == targetName);

            if (!attackerShipAlreadyExists)
                throw new ArgumentOutOfRangeException("Starship {0} does not exist!", attackerName);

            if (!targetShipAlreadyExists)
                throw new ArgumentOutOfRangeException("Starship {0} does not exist!", targetName);

            foreach (IStarship starship in this.GameEngine.Starships)
            {
                if (starship.Name == attackerName)
                    attackingShip = starship;

                if (starship.Name == targetName)
                    targetShip = starship;
            }

            this.ProcessStarShipBattle(attackingShip, targetShip);
        }

        private void ProcessStarShipBattle(IStarship attackingShip, IStarship targetShip)
        {
            base.ValidateAlive(attackingShip);
            base.ValidateAlive(targetShip);

            if (attackingShip.Location.Name != targetShip.Location.Name)
                throw new LocationOutOfRangeException(String.Format("{0} and {1} are not in the same galaxy!",
                    attackingShip.Name, targetShip.Name));

            IProjectile attack = attackingShip.ProduceAttack();
            targetShip.RespondToAttack(attack);

            Console.WriteLine(Messages.ShipAttacked, attackingShip.Name, targetShip.Name);

            if (targetShip.Shields < 0)
            {
                targetShip.Shields = 0;
            }

            if (targetShip.Health <= 0)
            {
                targetShip.Health = 0;
                Console.WriteLine(Messages.ShipDestroyed, targetShip.Name);
            }
        }

    }
}
