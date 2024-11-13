using CsvHelper.Configuration;
using FoodRegistrationToolSub1.Models.datasets;



public class FoodNutrientMap : ClassMap<FoodNutrient> {

    

    public FoodNutrientMap() {

        Map(m => m.Id).Name("id").Convert(args => 
        {
            int.TryParse(args.Row.GetField("id"), out int id);
            return id;
        });

        Map(m => m.FdcId).Name("fdc_id").Convert(args => 
        {
            int.TryParse(args.Row.GetField("fdc_id"), out int fdcId);
            return fdcId;
        });
        
        Map(m => m.NutrientId).Name("nutrient_id").Convert(args => 
        {
            int.TryParse(args.Row.GetField("nutrient_id"), out int nutrientId);
            return nutrientId;
        });
        
        Map(m => m.Amount).Name("amount").Convert(args => 
        {
            long.TryParse(args.Row.GetField("amount"), out long amount);
            return amount;
        });







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