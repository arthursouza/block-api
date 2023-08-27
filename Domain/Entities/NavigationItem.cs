using System.Collections.Generic;

namespace Domain.Entities;

public class NavigationItem
{
    public string Link { get; set; }
    public string Text { get; set; }
    public List<NavigationItem> Submenu { get; set; }

}
