using System;

namespace Domain.Entities;

public class BlockType
{
    public Guid Id { get; set; }

    public readonly static BlockType Header = new BlockType(Guid.Parse("c95d4772-8fd3-4e23-98a6-3f04534a7afc"));
    public readonly static BlockType Hero = new BlockType(Guid.Parse("7efaa0cd-1b5e-4fad-88ce-0df4c2de8c54"));
    public readonly static BlockType Services = new BlockType(Guid.Parse("3778c6c0-7b35-4e68-8a4a-389e5e34908f"));

    public BlockType() { }

    public BlockType(Guid id)
    {
        Id = id;
    }
}
