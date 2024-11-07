using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodRegisterationToolSub1.Models.datasets;

namespace FoodRegistrationToolSub1.Models.datasets;

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
    public int FoodCategoryId {get; set;}

    [ForeignKey("FoodCategoryId")]
    public required FoodCategory FoodCategory {get; set;}

    [Column("publication_date")]
    public DateTime PublicationDate {get; set;}

    public List<FoodNutrient> FoodNutrients {get; set;} = new List<FoodNutrient>();

    /// <summary>
    /// Check if the specific object are equals to other object in the table when submitted to database. 
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    /// <c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method overrides <see cref="object.Equals(object)" /> to provide equality comparison based on
    /// the unique properties of the <c>Food</c> entity. Two <c>Food</c> objects are considered
    /// equal if they have the same <c>FoodId</c>
    /// </remarks>
    public override bool Equals(object? obj)
    {

        if(obj is not Food other) { 
            return false;
        }

        return FoodId == other.FoodId;
    }
    /// <summary>
    /// Calculate the hash code of the current object. 
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    /// <remarks>
    /// This method overrides <see cref="object.GetHashCode" /> to generate a hash code based on the unique
    /// properties of the <c>Food</c> entity. The hash code is calculated using the <c>FoodId</c> properties,
    /// ensuring that equal objects produce the same hash codem, where tha hash code are the same for the identical FoodId.
    /// </remarks>
    public override int GetHashCode()
    {
        return HashCode.Combine(FoodId);
    }


}