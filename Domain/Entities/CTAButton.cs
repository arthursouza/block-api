namespace Domain.Entities;

public class CTAButton
{
    public string Text { get; set; }
    public string Icon { get; set; }
    public bool Status { get; set; }

    public string Target { get; set; }
    public CTAButtonEvent Event { get; set; }
}
