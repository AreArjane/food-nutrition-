using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FoodRegistrationToolSub1.Models.datasets {


[Table("Nutrient")]

public class Nutrient {

    [Key]
    [Column("id")]
    public int Id {get; set;}

    [StringLength(100)]
    [Required]
    [Column("name")]
    public string? Name {get; set;}

    [StringLength(10)]
    [Required]
    [Column("unit_name")]
    public string? UnitName {get; set;}

    [Column("nutrient_nbr")]
    public string? NutrientNbr {get; set;}
    [Column("rank")]
    public string? Rank {get; set;}

    public List<FoodNutrient> FoodNutrients { get; set; } = new List<FoodNutrient>();
   

}

}