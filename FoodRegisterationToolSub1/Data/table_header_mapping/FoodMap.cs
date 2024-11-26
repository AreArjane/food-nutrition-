using System.Globalization;
using CsvHelper.Configuration;

using FoodRegistrationToolSub1.Models.datasets;


public class FoodMap : ClassMap<Food> {

    public FoodMap() {
        Map(m => m.FoodId).Name("fdc_id");
        Map(m => m.DataType).Name("data_type");
        Map(m => m.FoodCategoryId).Name("food_category_id");
        Map(m => m.Description).Name("description");
        Map(m => m.PublicationDate).Name("publication_date").TypeConverterOption.DateTimeStyles(DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
    }
}