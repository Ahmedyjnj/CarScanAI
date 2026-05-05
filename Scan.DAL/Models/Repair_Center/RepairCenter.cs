using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class RepairCenter
{
    [Key]
    public Guid CenterId { get; set; } = Guid.NewGuid();

    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }

    public string Location { get; set; }
    //public string SeverityLevel { get; set; }
    public string SupportedBrand { get; set; }

    public bool IsActive { get; set; }

    // Navigation
    public ICollection<DetectionRepairCenter> DetectionRepairCenters { get; set; }
}
