using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DamageDetail
{
    [Key]
    public Guid DamageDetailId { get; set; }= Guid.NewGuid();

    public Guid DetectionId { get; set; }

    public string DamageType { get; set; }
    public string SeverityLevel { get; set; }
    public string DamagedAreaLocation { get; set; }

    public decimal ConfidenceScore { get; set; }
    public decimal EstimatedCost { get; set; }

    // Navigation
    [ForeignKey(nameof(DetectionId))]
    public Detection Detection { get; set; }
}
