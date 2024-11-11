using CsvHelper.Configuration;
using FoodRegistrationToolSub1.Models.datasets;



public class FoodNutrientMap : ClassMap<FoodNutrient> {

    public FoodNutrientMap() {
        Map(m => m.Id).Name("id");
        Map(m => m.FdcId).Name("fdc_id");
        Map(m => m.DataPoint).Name("data_points");
        Map(m => m.DerivationId).Name("derivation_id");
        Map(m => m.Min).Name("min");
        Map(m => m.Max).Name("max");
        Map(m => m.Median).Name("median");
        Map(m => m.Footnote).Name("footnote");
        Map(m => m.MinYearAcquired).Name("min_year_acquired");
        Map(m => m.NutrientId).Name("ntrient_id");
      
    }
}