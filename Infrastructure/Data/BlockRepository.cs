using Domain.Entities;
using Domain.Entities.Interfaces;
using Domain.Exceptions;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data;

public class BlockRepository : IBlockRepository
{
    JsonSerializerOptions _serializerOptions;

    public BlockRepository(JsonSerializerOptions jsonSerializerOptions)
    {
        _serializerOptions = jsonSerializerOptions;
    }

    public async Task CreateAsync(string key)
    {
        var filename = $"{key}.json";

        if (!IsKeyValid(key))
        {
            throw new InvalidCharactersInKeyException();
        }

        if (File.Exists(filename))
        {
            throw new DuplicateKeyException();
        }

        var data = new List<IBlock>
        {
            new WebsiteHeaderBlock(),
            new WebsiteHeroBlock(),
            new WebsiteServicesBlock(),
        };

        //var data = new List<IBlock>
        //{
        //    new WebsiteHeaderBlock
        //    {
        //        BlockOrder = 0,
        //        BusinessName = "Local Plumber",
        //        CTAButton = new CTAButton
        //        {
        //            Event = CTAButtonEvent.ClickToCall,
        //            Target = "(555) 555 555 555",
        //            Icon = "fa icon",
        //            Status = true,
        //            Text = "Call Now"
        //        },
        //        Id = BlockType.Header.Id,
        //        Logo = true,
        //        NavigationMenu = new List<NavigationItem>
        //        {
        //            new NavigationItem
        //            {
        //                Text = "Home",
        //                Link = "home url"
        //            },
        //            new NavigationItem
        //            {
        //                Text = "Submenu",
        //                Submenu = new List<NavigationItem>
        //                {
        //                    new NavigationItem
        //                    {
        //                        Text = "Link 1",
        //                        Link = "url"
        //                    },
        //                    new NavigationItem
        //                    {
        //                        Text = "Link 2",
        //                        Link = "url"
        //                    },
        //                    new NavigationItem
        //                    {
        //                        Text = "Link 3",
        //                        Link = "url"
        //                    }
        //                }
        //            }
        //        }
        //    },
        //    new WebsiteHeroBlock
        //    {
        //        Id = BlockType.Hero.Id,
        //        BlockOrder = 1,
        //        ContentAlignment = Alignment.Left,
        //        ImageAlignment = Alignment.Right,
        //        CTAButton = new CTAButton
        //        {
        //            Event = CTAButtonEvent.Navigate,
        //            Target = "#services",
        //            Icon = "fa icon",
        //            Status = true,
        //            Text = "Check services"
        //        },
        //        DescriptionText = "In depth description",
        //        HeadlineText = "Headline",
        //        HeroImage = "imageUrl"
        //    },
        //    new WebsiteServicesBlock
        //    {
        //        Id = BlockType.Services.Id,
        //        BlockOrder = 2,
        //        HeadlineText = "Services",
        //        ServiceCards = new List<ServiceCard>
        //        {
        //            new ServiceCard
        //            {
        //                CTAButton = new CTAButton
        //                {
        //                    Event = CTAButtonEvent.Navigate,
        //                    Target = "#call",
        //                    Icon = "fa icon",
        //                    Status = true,
        //                    Text = "Call"
        //                }
        //            }
        //        }
        //    },
        //};

        await SaveChanges(key, data);
    }

    private bool IsKeyValid(string key)
    {
        return key.Length > 0 && key.All(ch => char.IsLetterOrDigit(ch));
    }

    public async Task<IList<IBlock>> GetAsync(string key)
    {
        var filename = $"{key}.json";
        if (!File.Exists(filename))
        {
            throw new WebsiteKeyNotFoundException();
        }

        return JsonSerializer.Deserialize<List<IBlock>>(await File.ReadAllTextAsync(filename), _serializerOptions);
    }

    public async Task Update(string key, Guid sectionId, IBlock block)
    {
        var blocks = await GetAsync(key);

        block.Id = sectionId;
        ValidateBlock(block);

        var section = blocks.FirstOrDefault(b => b.Id == sectionId);
        if (section != null)
        {
            blocks.Remove(section);
        }

        blocks.Add(block);

        await SaveChanges(key, blocks);
    }

    public async Task RemoveSection(string key, Guid sectionId)
    {
        var blocks = await GetAsync(key);
        var section = blocks.FirstOrDefault(b => b.Id == sectionId);
        if (section != null)
        {
            blocks.Remove(section);
        }

        await SaveChanges(key, blocks);
    }

    private async Task SaveChanges(string key, IList<IBlock> data)
    {
        var json = JsonSerializer.Serialize(data, _serializerOptions);
        await File.WriteAllTextAsync($"{key}.json", json);
    }

    private void ValidateBlock(IBlock block)
    {
        if (block.Id == BlockType.Services.Id)
        {
            ValidateServicesBlock(block);
        }
        else if (block.Id == BlockType.Hero.Id)
        {
            ValidateHeroBlock(block);
        }
        else if (block.Id == BlockType.Header.Id)
        {
            ValidateHeaderBlock(block);
        }
    }

    private void ValidateHeaderBlock(IBlock block)
    {
        var headerBlock = block as WebsiteHeaderBlock;
        if (headerBlock == null)
        {
            throw new InvalidBlockException("Data does not match sectionId");
        }
    }

    private static void ValidateHeroBlock(IBlock block)
    {
        var heroBlock = block as WebsiteHeroBlock;
        if (heroBlock == null)
        {
            throw new InvalidBlockException("Data does not match sectionId");
        }

        if (heroBlock.CTAButton.Event == CTAButtonEvent.ClickToCall)
        {
            throw new InvalidBlockException("ClickToCall event is not valid as CTA for hero block");
        }

        if (heroBlock.ImageAlignment != null && heroBlock.ContentAlignment == heroBlock.ImageAlignment)
        {
            throw new InvalidBlockException("ContentAlignment and ImageAlignment must have different values");
        }
    }

    private static void ValidateServicesBlock(IBlock block)
    {
        var servicesBlock = block as WebsiteServicesBlock;
        if (servicesBlock == null)
        {
            throw new InvalidBlockException("Data does not match sectionId");
        }

        if (servicesBlock.ServiceCards.Any(c => c.CTAButton.Event == CTAButtonEvent.ClickToCall))
        {
            throw new InvalidBlockException("ClickToCall event is not valid as CTA for service cards");
        }
    }
}
