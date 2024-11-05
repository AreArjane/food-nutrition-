using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FoodRegistrationToolSub1.Models.datasets;


[Table("Nutrient")]

public class Nutrient {

    [Key]
    [Column("id")]
    public int Id {get; set;}

    [StringLength(100)]
    [Required]
    public required string Name {get; set;}

    [StringLength(10)]
    [Required]
    public required string UnitName {get; set;}

    [Column("nutrient_nbr")]
    public int NutrientNbr {get; set;}
    [Column("rank")]
    public int Rank {get; set;}

    public List<FoodNutrient> FoodNutrients { get; set; } = new List<FoodNutrient>();

}