namespace CatchMe.Domain.Entities
{
    public class VehicleEntity
    {
        public int Id { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public int Year { get; set; }
        
        public string Number { get; set; }

        public override string ToString()
        {
            return string.Format("Manufacturer: {0}\n, Model {1}\n, Year {2} in color {3}",
                Manufacturer,
                Model,
                Year,
                Color);
        }
    }
}
