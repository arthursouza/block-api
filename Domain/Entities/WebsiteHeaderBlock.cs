using Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace Domain.Entities;

public class WebsiteHeaderBlock : IBlock
{
    public Guid Id { get; set; }
    public int BlockOrder { get; set; }
    public string BusinessName { get; set; }
    public bool Logo { get; set; }
    public List<NavigationItem> NavigationMenu { get; set; }
    public CTAButton CTAButton { get; set; }

    public WebsiteHeaderBlock()
    {
        Id = BlockType.Header.Id;
    }
}
