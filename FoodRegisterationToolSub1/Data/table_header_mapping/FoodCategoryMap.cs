using CsvHelper.Configuration;
using FoodRegisterationToolSub1.Models.datasets;


public class FoodCategoryMap : ClassMap<FoodCategory> {

    public FoodCategoryMap() {
        Map(m => m.Id).Name("id");
        Map(m => m.Code).Name("code");
        Map(m => m.Description).Name("description");
    }
}