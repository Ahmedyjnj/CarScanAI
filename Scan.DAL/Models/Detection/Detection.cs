using Scan.DAL.Models.Car;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Detection
{
    [Key]
    public Guid DetectionId { get; set; } = Guid.NewGuid();

    public string UserId { get; set; }
    public Guid CarId { get; set; }

    public string ImagePath { get; set; }
    public DateTime DetectionDate { get; set; }

    public string OverallSeverity { get; set; }
    public decimal TotalCost { get; set; }
    public string AiModel { get; set; }

    // Navigation
    public User User { get; set; }
    public Car Car { get; set; }

    public ICollection<DamageDetail> DamageDetails { get; set; }
    public ICollection<DetectionRepairCenter> DetectionRepairCenters { get; set; }
}
