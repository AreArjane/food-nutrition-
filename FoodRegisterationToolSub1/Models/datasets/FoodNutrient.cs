using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FoodRegistrationToolSub1.Models.datasets {

/// <summary>
    /// Represents the relationship between food items and their associated nutrients.
    /// Maps to the "FoodNutrient" table in the database.
    /// </summary>
[Table("FoodNutrient")]

public class FoodNutrient {
   /// <summary>
        /// Gets or sets the unique identifier for this food-nutrient relationship.
        /// </summary>
    [Key]
    [Column("id")]
    public int Id {get; set;}
   /// <summary>
        /// Gets or sets the unique identifier of the food associated with this nutrient entry.
        /// This is a required field and establishes a foreign key relationship with the "Food" table.
        /// </summary>
    [Required]
    [Column("fdc_id")]
    public int FdcId {get; set;}
/// <summary>
        /// Navigation property for the related food entity.
        /// </summary>
    [ForeignKey("FdcId")]
    public Food? Food {get; set;}
/// <summary>
        /// Gets or sets the unique identifier of the nutrient associated with this food entry.
        /// This field establishes a foreign key relationship with the "Nutrient" table.
        /// </summary>
    [Column("nutrient_id")]
    public int? NutrientId {get; set;}
   /// <summary>
        /// Navigation property for the related nutrient entity.
        /// </summary>
    [ForeignKey("NutrientId")]
    public Nutrient? Nutrient {get; set;}
    /// <summary>
        /// Gets or sets the amount of the nutrient present in the food item.
        /// </summary>
    [Column("amount")]
    public long? Amount {get; set;}
  /// <summary>
        /// Gets or sets the data points associated with this food-nutrient relationship.
        /// </summary>
    [Column("data_points")]
    public string? DataPoint {get; set;}
/// <summary>
        /// Gets or sets the derivation ID for the nutrient measurement.
        /// </summary>
    [Column("derivation_id")]
    public string? DerivationId {get; set;}
   /// <summary>
        /// Gets or sets the minimum value for the nutrient measurement.
        /// </summary>
    [Column("min")]
    public string? Min {get; set;}
     /// <summary>
        /// Gets or sets the maximum value for the nutrient measurement.
        /// </summary>
     [Column("max")]
    public string? Max {get; set;}
     /// <summary>
        /// Gets or sets the median value for the nutrient measurement.
        /// </summary>
     [Column("median")]
    public string? Median {get; set;}
    /// <summary>
        /// Gets or sets the footnote describing additional information about this food-nutrient relationship.
        /// This field has a maximum length of 2555 characters.
        /// </summary>
    [StringLength(2555)]
     [Column("footnote")]
    public string? Footnote {get; set;}
    /// <summary>
        /// Gets or sets the minimum year in which the nutrient data was acquired.
        /// </summary>
    [Column("min_year_acquired")]
    public string? MinYearAcquired {get; set;}

   

}
}
