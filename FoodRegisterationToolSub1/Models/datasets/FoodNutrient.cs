using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FoodRegistrationToolSub1.Models.datasets;


[Table("FoodNutrient")]

public class FoodNutrient {

    [Key]
    [Column("id")]
    public int Id {get; set;}

    [Required]
    [Column("fdc_id")]
    public int FdcId {get; set;}

    [ForeignKey("FdcId")]
    public required Food Food {get; set;}

    [Column("data_points")]
    public int DataPoint {get; set;}

    [Column("derivation_id")]
    public int DerivationId {get; set;}
    [Column("min")]
    public float? Min {get; set;}
     [Column("max")]
    public float? Max {get; set;}
     [Column("median")]
    public float? Median {get; set;}

    [StringLength(255)]
     [Column("footnote")]
    public string? Footnote {get; set;}
    
    [Column("min_year_acquired")]
    public int? MinYearAcquired {get; set;}

    [Column("ntrient_id")]
    public int NutrientId {get; set;}

    [ForeignKey("NutrientId")]
    public required Nutrient Nutrient {get; set;}

    /// <summary>
    /// Check if the specific object are equals to other object in the table when submitted to database. 
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    /// <c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method overrides <see cref="object.Equals(object)" /> to provide equality comparison based on
    /// the unique properties of the <c>FoodNutrient</c> entity. Two <c>FoodNutrient</c> objects are considered
    /// equal if they have the same <c>Id</c>
    /// </remarks>
    /// 

    public override bool Equals(object? obj)
    {
        if(obj is not FoodNutrient other) { 
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
    /// properties of the <c>FoodNutrient</c> entity. The hash code is calculated using the <c>Id</c> and <c>FdcId</c> properties,
    /// ensuring that equal objects produce the same hash codem, where tha hash code are the same for the identical name.
    /// </remarks>
    /// 
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, FdcId);
    }


}