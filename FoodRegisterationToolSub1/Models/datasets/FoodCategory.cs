using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodRegistrationToolSub1.Models.datasets;


namespace FoodRegisterationToolSub1.Models.datasets {

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
    /// <summary>
    /// Check if the specific object are equals to other object in the table when submitted to database. 
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    /// <c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method overrides <see cref="object.Equals(object)" /> to provide equality comparison based on
    /// the unique properties of the <c>FoodCategory</c> entity. Two <c>FoodCategory</c> objects are considered
    /// equal if they have the same <c>Code</c>
    /// </remarks>

    public override bool Equals(object? obj)
    {
        if (obj is not FoodCategory other) { 
            return false;
        }

        return Code == other.Code && Description == other.Description;
    }
    /// <summary>
    /// Calculate the hash code of the current object. 
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    /// <remarks>
    /// This method overrides <see cref="object.GetHashCode" /> to generate a hash code based on the unique
    /// properties of the <c>FoodCategory</c> entity. The hash code is calculated using the <c>Code</c>  and <c>description<c> properties,
    /// ensuring that equal objects produce the same hash codem, where tha hash code are the same for the identical name.
    /// </remarks>
    public override int GetHashCode() { 
        return HashCode.Combine(Code, Description);
    }

}
}