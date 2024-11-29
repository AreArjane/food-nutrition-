using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodRegistrationToolSub1.Models.datasets;


namespace FoodRegisterationToolSub1.Models.datasets {
/// <summary>
    /// Represents a category of food items in the system.
    /// Maps to the "FoodCategories" table in the database.
    /// </summary>
[Table("FoodCategories")]

public class FoodCategory {
  // <summary>
        /// Gets or sets the unique identifier for the food category.
        /// This value is not auto-generated.
        /// </summary>
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id {get; set;}
    /// <summary>
        /// Gets or sets the code for the food category.
        /// This is a required field with a maximum length of 10 characters.
        /// </summary>
    [StringLength(10)]
    [Required]
    [Column("code")]
    public required string Code {get; set;}
   /// <summary>
        /// Gets or sets the description of the food category.
        /// This is a required field with a maximum length of 255 characters.
        /// </summary>
    [StringLength(255)]
    [Required]
    [Column("description")]
    public required string Description {get; set;}
   /// <summary>
        /// Gets or sets the list of food items associated with this category.
        /// This property establishes a one-to-many relationship with the "Food" entity.
        /// </summary>
    public List<Food> Foods {get; set;} = new List<Food>();   

}
}
