using CsvHelper.Configuration;
using FoodRegistrationToolSub1.Models.datasets;

/// <summary>
/// Defines a mapping configuration for the `FoodNutrient` class to map its properties
/// to corresponding columns in a CSV file using CsvHelper.
/// </summary>

public class FoodNutrientMap : ClassMap<FoodNutrient> {
/// <summary>
    /// Initializes a new instance of the <see cref="FoodNutrientMap"/> class and configures the mappings
    /// between `FoodNutrient` properties and CSV column headers.
    /// </summary>
    

    public FoodNutrientMap() {
   /// <summary>
        /// Maps the `Id` property to the "id" column and applies a conversion to parse its value as an integer.
        /// </summary>
        Map(m => m.Id).Name("id").Convert(args => 
        {
            int.TryParse(args.Row.GetField("id"), out int id);
            return id;
        });
        /// <summary>
        /// Maps the `FdcId` property to the "fdc_id" column and applies a conversion to parse its value as an integer.
        /// </summary>
        Map(m => m.FdcId).Name("fdc_id").Convert(args => 
        {
            int.TryParse(args.Row.GetField("fdc_id"), out int fdcId);
            return fdcId;
        });
        /// <summary>
        /// Maps the `NutrientId` property to the "nutrient_id" column and applies a conversion to parse its value as an integer.
        /// </summary>
        Map(m => m.NutrientId).Name("nutrient_id").Convert(args => 
        {
            int.TryParse(args.Row.GetField("nutrient_id"), out int nutrientId);
            return nutrientId;
        });
         /// <summary>
        /// Maps the `Amount` property to the "amount" column and applies a conversion to parse its value as a long.
        /// </summary>
        Map(m => m.Amount).Name("amount").Convert(args => 
        {
            long.TryParse(args.Row.GetField("amount"), out long amount);
            return amount;
        });



// Standard mappings for additional properties
        /// <summary>
        /// Maps the `Id` property to the "id" column without additional conversion.
        /// </summary>

        Map(m => m.Id).Name("id");
        /// <summary>
        /// Maps the `FdcId` property to the "fdc_id" column without additional conversion.
        /// </summary>
        Map(m => m.FdcId).Name("fdc_id");
        /// <summary>
        /// Maps the `DataPoint` property to the "data_points" column.
        /// </summary>
        Map(m => m.DataPoint).Name("data_points");
        Map(m => m.DerivationId).Name("derivation_id");
        // Maps the Min property to the "min" column in the CSV.
        Map(m => m.Min).Name("min");
         // Maps the Max property to the "max" column in the CSV.
        Map(m => m.Max).Name("max");
        // Maps the Median property to the "median" column in the CSV.
        Map(m => m.Median).Name("median");
        // Maps the Footnote property to the "footnote" column in the CSV.
        Map(m => m.Footnote).Name("footnote");
        // Maps the MinYearAcquired property to the "min_year_acquired" column in the CSV.
        Map(m => m.MinYearAcquired).Name("min_year_acquired");
                // Maps the NutrientId property to the "ntrient_id" column in the CSV (note the potential typo in the name).
       Map(m => m.NutrientId).Name("ntrient_id");
      
    }
}
