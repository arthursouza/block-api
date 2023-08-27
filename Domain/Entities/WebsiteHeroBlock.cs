using Domain.Entities.Interfaces;
using System;

namespace Domain.Entities;

public class WebsiteHeroBlock : IBlock
{
    public Guid Id { get; set; }
    public int BlockOrder { get; set; }
    public string HeadlineText { get; set; }
    public string DescriptionText { get; set; }
    public string HeroImage { get; set; }
    public CTAButton CTAButton { get; set; }
    public Alignment ImageAlignment { get; set; }
    public Alignment ContentAlignment { get; set; }

    public WebsiteHeroBlock()
    {
        Id = BlockType.Hero.Id;
    }
}
