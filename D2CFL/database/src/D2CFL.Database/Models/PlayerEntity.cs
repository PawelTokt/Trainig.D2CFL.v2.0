using System;
using D2CFL.Database.Repository;

namespace D2CFL.Database.Models
{
    public class PlayerEntity : Entity<Guid>
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
