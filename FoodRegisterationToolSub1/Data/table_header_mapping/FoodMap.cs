using System.Globalization;
using CsvHelper.Configuration;

using FoodRegistrationToolSub1.Models.datasets;
/// <summary>
/// Defines a mapping configuration for the `Food` class to map its properties
/// to corresponding columns in a CSV file using CsvHelper.
/// </summary>

public class FoodMap : ClassMap<Food> {
    /// <summary>
    /// Initializes a new instance of the <see cref="FoodMap"/> class and configures the mappings
    /// between `Food` properties and CSV column headers.
    /// </summary>
    public FoodMap() {
        
        Map(m => m.FoodId).Name("fdc_id");
        Map(m => m.DataType).Name("data_type");
        Map(m => m.FoodCategoryId).Name("food_category_id");
        Map(m => m.Description).Name("description");
        Map(m => m.PublicationDate).Name("publication_date").TypeConverterOption.DateTimeStyles(DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
    }
}
