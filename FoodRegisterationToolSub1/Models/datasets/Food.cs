using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodRegisterationToolSub1.Models.datasets;



namespace FoodRegistrationToolSub1.Models.datasets {

[Table("Food")]
public class Food { 

    [Key]
    [Column("fdc_id")]
    public int FoodId {get; set;}
    
    [StringLength(50)]
    [Required]
    [Column("data_type")]
    public required string DataType {get; set;}

    [StringLength(255)]
    [Required]
    [Column("description")]
    public required string Description {get; set;}

    [Column("food_category_id")]
    public int? FoodCategoryId {get; set;}

    [ForeignKey("FoodCategoryId")]
    public FoodCategory? FoodCategory {get; set;}

    [Column("publication_date")]
    public string? PublicationDate {get; set;}

    public List<FoodNutrient> FoodNutrients {get; set;} = new List<FoodNutrient>();

    

}

}