using System;

namespace Domain.Entities.Interfaces;

public interface IBlock
{
    Guid Id { get; set; }
    int BlockOrder { get; set; }
}
