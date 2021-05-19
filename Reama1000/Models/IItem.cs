using System;

namespace Models
{
    public interface IItem
    {
        Guid Id { get; set; }
        string Navn { get; set; }
    }
}
