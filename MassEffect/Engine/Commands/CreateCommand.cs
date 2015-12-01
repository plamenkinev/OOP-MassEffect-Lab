namespace MassEffect.Engine.Commands
{
    using System;
    using MassEffect.Interfaces;
    using System.Linq;
    using GameObjects.Ships;
    using Exceptions;
    using GameObjects.Enhancements;

    public class CreateCommand : Command
    {
        public CreateCommand(IGameEngine gameEngine) 
            : base(gameEngine)
        {
        }

        public override void Execute(string[] commandArgs)
        {
            string type = commandArgs[1];
            string shipName = commandArgs[2];
            string locationName = commandArgs[3];

            bool shipAlreadyExists = this.GameEngine.Starships
                .Any(s => s.Name == shipName);

            // validating that the ship does not exist already
            if (shipAlreadyExists)
                throw new ShipException(Messages.DuplicateShipName);

            var location = this.GameEngine.Galaxy.GetStarSystemByName(locationName);
            StarshipType shipType = (StarshipType)Enum.Parse(typeof(StarshipType), type);

            // creating the ship using the ShipFactory CreateShip method
            IStarship ship = this.GameEngine.ShipFactory.CreateShip(shipType, shipName, location);

            // creating the enhancements
            for (int i = 4; i < commandArgs.Length; i++)
            {
                var enhancementType = (EnhancementType)
                    Enum.Parse(typeof(EnhancementType), commandArgs[i]);

                Enhancement enhancement = null;
                enhancement = this.GameEngine.EnhancementFactory.Create(enhancementType);

                // adding the freshly created enhancement to the ship
                ship.AddEnhancement(enhancement);
            }
            // adding ship to starships
            this.GameEngine.Starships.Add(ship);

            Console.WriteLine(Messages.CreatedShip, shipType, shipName);
        }
    }
}
