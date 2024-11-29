using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FoodRegistrationToolSub1.Models.datasets {
/// <summary>
    /// Represents a nutrient entity, including its name, unit, and other related attributes.
    /// Maps to the "Nutrient" table in the database.
    /// </summary>

[Table("Nutrient")]

public class Nutrient {
   /// <summary>
        /// Gets or sets the unique identifier for the nutrient.
        /// This is the primary key in the "Nutrient" table.
        /// </summary>
    [Key]
    [Column("id")]
    public int Id {get; set;}
/// <summary>
        /// Gets or sets the name of the nutrient.
        /// This field is required and has a maximum length of 100 characters.
        /// </summary>
    [StringLength(100)]
    [Required]
    [Column("name")]
    public string? Name {get; set;}
/// <summary>
        /// Gets or sets the unit of measurement for the nutrient.
        /// This field is required and has a maximum length of 10 characters.
        /// </summary>
    [StringLength(10)]
    [Required]
    [Column("unit_name")]
    public string? UnitName {get; set;}
   /// <summary>
        /// Gets or sets the nutrient number, which is an optional field.
        /// </summary>
    [Column("nutrient_nbr")]
    public string? NutrientNbr {get; set;}
   /// <summary>
        /// Gets or sets the rank of the nutrient, indicating its order or priority in a dataset.
        /// </summary>
    [Column("rank")]
    public string? Rank {get; set;}
/// <summary>
        /// Navigation property for the list of relationships between foods and this nutrient.
        /// </summary>
    public List<FoodNutrient> FoodNutrients { get; set; } = new List<FoodNutrient>();
   

}

}
