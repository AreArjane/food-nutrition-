using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodRegistrationToolSub1.Models.datasets;


namespace FoodRegisterationToolSub1.Models.datasets;

[Table("FoodCategories")]

public class FoodCategory {

    [Key]
    [Column("id")]
    public int Id {get; set;}

    [StringLength(10)]
    [Required]
    [Column("code")]
    public required string Code {get; set;}

    [StringLength(255)]
    [Required]
    [Column("description")]
    public required string Description {get; set;}

    public List<Food> Foods {get; set;} = new List<Food>();
}