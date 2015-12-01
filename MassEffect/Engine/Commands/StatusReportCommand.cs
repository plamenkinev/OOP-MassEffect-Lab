namespace MassEffect.Engine.Commands
{
    using MassEffect.Interfaces;
    using System;
    using System.Linq;

    public class StatusReportCommand : Command
    {
        public StatusReportCommand(IGameEngine gameEngine)
            : base(gameEngine)
        {
        }

        public override void Execute(string[] commandArgs)
        {
            string shipName = commandArgs[1];
            IStarship ship = null;

            bool shipAlreadyExists = this.GameEngine.Starships
                .Any(s => s.Name == shipName);

            if (!shipAlreadyExists)
                throw new ArgumentOutOfRangeException("Starship {0} does not exist!", shipName);

            foreach (IStarship starship in this.GameEngine.Starships)
            {
                if (starship.Name == shipName)
                    ship = starship;
            }

            Console.WriteLine(ship.ToString());
        }
    }
}
