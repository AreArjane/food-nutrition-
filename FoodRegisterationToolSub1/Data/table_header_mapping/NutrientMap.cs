using CsvHelper.Configuration;
using FoodRegistrationToolSub1.Models.datasets;



public class NutrientMap : ClassMap<Nutrient> {

    public NutrientMap() {
        Map(m => m.Id).Name("id");
        Map(m => m.Name).Name("name");
        Map(m => m.UnitName).Name("unit_name");
        Map(m => m.NutrientNbr).Name("nutrient_nbr");
        Map(m => m.Rank).Name("rank");
    }
}