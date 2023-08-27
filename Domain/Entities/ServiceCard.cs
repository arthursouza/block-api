namespace Domain.Entities;

public class ServiceCard
{
    public string ServiceName { get; set; }
    public string ServiceDescription { get; set; }
    public string ServiceImage { get; set; }
    public CTAButton CTAButton { get; set; }
}