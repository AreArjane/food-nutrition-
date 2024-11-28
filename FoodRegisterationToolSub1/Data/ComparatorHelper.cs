using System.Diagnostics.CodeAnalysis;
using FoodRegisterationToolSub1.Models.datasets;
using FoodRegistrationToolSub1.Models.datasets;
/// <summary>
/// The comparator interface equality implemented to check for Id for each entity when importing data from CSV files.
/// This ensure to not add dublicated value add by agencies, as the CSV could be update it over time.
/// <see cref="https://fdc.nal.usda.gov/download-datasets"/>
/// The 
/// Overriding method like Equals and GetHashCode are suitable for immutable classes.
/// The best approach to distingush data tabel created for datasets food and our application mutable table wich will have CRUD operation. 
/// Due to time consuming this approach with IEqualityComparer are more suitable for data table holding both food data with CRUD operation and importing from datasets provided by fdc.nal
/// 
/// <seealso Simple immutable classes with overriding such methods cref="https://learn.microsoft.com/en-us/ef/core/modeling/value-comparers?tabs=ef5"/>
/// 
/// </summary>
/// <example>
///  public override bool Equals(object? obj)
/// {
///        if(obj is not FoodNutrient other) { 
///            return false;
///        }
///        return Id == other.Id;
///    }
/// </example>
public class FoodComparer : IEqualityComparer<Food>
{
    public bool Equals(Food? x, Food? y)
    {
        return x.FoodId == y.FoodId;
    }

    public int GetHashCode([DisallowNull] Food obj)
    {
        return obj.FoodId.GetHashCode();
    }
}

public class FoodCategoryComparer : IEqualityComparer<FoodCategory>
{
    public bool Equals(FoodCategory? x, FoodCategory? y)
    {
        return x.Id == y.Id;
    }

    public int GetHashCode([DisallowNull] FoodCategory obj)
    {
        return obj.Id.GetHashCode();
    }
}

public class NutrientComparer : IEqualityComparer<Nutrient>
{
    public bool Equals(Nutrient? x, Nutrient? y)
    {
        return x.Id == y.Id;
    }

    public int GetHashCode([DisallowNull] Nutrient obj)
    {
        return obj.Id.GetHashCode();
    }
}

public class FoodNutrientComparer : IEqualityComparer<FoodNutrient>
{
    public bool Equals(FoodNutrient? x, FoodNutrient? y)
    {
        return x.Id == y.Id;
    }

    public int GetHashCode([DisallowNull] FoodNutrient obj)
    {
        return obj.Id.GetHashCode();
    }
}