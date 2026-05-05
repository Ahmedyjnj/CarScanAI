using System;
using System.ComponentModel.DataAnnotations.Schema;

public class DetectionRepairCenter
{
    public Guid DetectionId { get; set; }
    public Guid CenterId { get; set; }

    public bool IsContacted { get; set; }
    public DateTime? ContactDate { get; set; }

    // Navigation
    public Detection Detection { get; set; }
    public RepairCenter RepairCenter { get; set; }
}
