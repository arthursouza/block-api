using System;

namespace Domain.Entities;

public class CTAButtonEvent
{
    public Guid Id { get; set; }

    public readonly static CTAButtonEvent ClickToCall = new CTAButtonEvent(Guid.Parse("8c39aa33-f32b-4ac4-9153-2704537abd94"));
    public readonly static CTAButtonEvent Navigate = new CTAButtonEvent(Guid.Parse("07beb7ba-461c-4bc6-9872-4bae4ecf05e5"));

    public CTAButtonEvent() { }

    public CTAButtonEvent(Guid id)
    {
        Id = id;
    }
}
