namespace CarDealer.DTO
{
    using System.Collections.Generic;

    public class CarPartsDTO
    {
        public CarDTO car { get; set; }

        public List<PartDTO> parts { get; set; }
    }
}
