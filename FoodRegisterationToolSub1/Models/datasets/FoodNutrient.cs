using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FoodRegistrationToolSub1.Models.datasets;


[Table("FoodNutrient")]

public class FoodNutrient {

    [Key]
    [Column("id")]
    public int Id {get; set;}

    [Required]
    [Column("fdc_id")]
    public int FdcId {get; set;}

    [ForeignKey("FdcId")]
    public required Food Food {get; set;}

    [Column("data_points")]
    public int DataPoint {get; set;}

    [Column("derivation_id")]
    public int DerivationId {get; set;}
    [Column("min")]
    public float? Min {get; set;}
     [Column("max")]
    public float? Max {get; set;}
     [Column("median")]
    public float? Median {get; set;}

    [StringLength(255)]
     [Column("footnote")]
    public string? Footnote {get; set;}
    
    [Column("min_year_acquired")]
    public int? MinYearAcquired {get; set;}

    [Column("ntrient_id")]
    public int NutrientId {get; set;}

    [ForeignKey("NutrientId")]
    public required Nutrient Nutrient {get; set;}


}