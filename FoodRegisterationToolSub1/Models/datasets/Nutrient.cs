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

    /// <summary>
    /// Check if the specific object are equals to other object in the table when submitted to database. 
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    /// <c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method overrides <see cref="object.Equals(object)" /> to provide equality comparison based on
    /// the unique properties of the <c>Nutrient</c> entity. Two <c>Nutrient</c> objects are considered
    /// equal if they have the same <c>Id</c>
    /// </remarks>
    /// 
    public override bool Equals(object? obj)
    {
        if(obj is not Nutrient other)  {
            return false;

        }
        return Id == other.Id;
    }
    /// <summary>
    /// Calculate the hash code of the current object. 
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    /// <remarks>
    /// This method overrides <see cref="object.GetHashCode" /> to generate a hash code based on the unique
    /// properties of the <c>Nutrient</c> entity. The hash code is calculated using the <c>Id</c> and <c>Name</c> properties,
    /// ensuring that equal objects produce the same hash codem, where tha hash code are the same for the identical name.
    /// </remarks>
    /// 

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }

}