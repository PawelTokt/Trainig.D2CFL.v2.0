using System;

namespace D2CFL.Database.Interfaces
{
    public interface IEntity<TType> : IEquatable<IEntity<TType>>
    {
        TType Id { get; set; }
    }
}
