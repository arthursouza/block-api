using Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace Domain.Entities;

public class WebsiteServicesBlock : IBlock
{
    public Guid Id { get; set; }
    public int BlockOrder { get; set; }
    public string HeadlineText { get; set; }
    public List<ServiceCard> ServiceCards { get; set; }

    public WebsiteServicesBlock()
    {
        Id = BlockType.Services.Id;
    }
}
