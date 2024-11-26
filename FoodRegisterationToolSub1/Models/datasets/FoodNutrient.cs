using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FoodRegistrationToolSub1.Models.datasets {


[Table("FoodNutrient")]

public class FoodNutrient {

    [Key]
    [Column("id")]
    public int Id {get; set;}

    [Required]
    [Column("fdc_id")]
    public int FdcId {get; set;}

    [ForeignKey("FdcId")]
    public Food? Food {get; set;}

    [Column("nutrient_id")]
    public int? NutrientId {get; set;}

    [ForeignKey("NutrientId")]
    public Nutrient? Nutrient {get; set;}
    
    [Column("amount")]
    public long? Amount {get; set;}

    [Column("data_points")]
    public string? DataPoint {get; set;}

    [Column("derivation_id")]
    public string? DerivationId {get; set;}
    [Column("min")]
    public string? Min {get; set;}
     [Column("max")]
    public string? Max {get; set;}
     [Column("median")]
    public string? Median {get; set;}

    [StringLength(2555)]
     [Column("footnote")]
    public string? Footnote {get; set;}
    
    [Column("min_year_acquired")]
    public string? MinYearAcquired {get; set;}

   

}
}