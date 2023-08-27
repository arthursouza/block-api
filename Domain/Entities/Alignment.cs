using System;

namespace Domain.Entities;

public class Alignment
{
    public Guid Id { get; set; }

    public readonly static Alignment Left = new Alignment(Guid.Parse("81e7a0b9-9016-42e0-9647-dd86690c1443"));
    public readonly static Alignment Right = new Alignment(Guid.Parse("824e1d90-3eca-4a03-b2cf-8981a03b757f"));
    public readonly static Alignment Center = new Alignment(Guid.Parse("d0232e2d-e7da-4632-82e9-231f19c1b7a2"));

    public Alignment() { }

    public Alignment(Guid id)
    {
        Id = id;
    }
}
