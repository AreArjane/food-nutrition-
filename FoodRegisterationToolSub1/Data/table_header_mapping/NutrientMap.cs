using CsvHelper.Configuration;
using FoodRegistrationToolSub1.Models.datasets;

/// <summary>
/// Defines a mapping configuration for the `Nutrient` class to map its properties
/// to corresponding columns in a CSV file using CsvHelper.
/// </summary>

public class NutrientMap : ClassMap<Nutrient> {
/// <summary>
    /// Initializes a new instance of the <see cref="NutrientMap"/> class and configures the mappings
    /// between `Nutrient` properties and CSV column headers, including custom parsing for certain fields.
    /// </summary>
    public NutrientMap() {

               // Maps the Id property to the "id" column with custom conversion to parse it as an integer.

        Map(m => m.Id).Name("id").Convert(args => {

            int.TryParse(args.Row.GetField("id"), out int id);        
            return id;
            });

                // Maps the Id property to the "id" column in the CSV.
        Map(m => m.Id).Name("id");
                // Maps the Name property to the "name" column in the CSV.
        Map(m => m.Name).Name("name");
                // Maps the UnitName property to the "unit_name" column in the CSV.
        Map(m => m.UnitName).Name("unit_name");
                // Maps the NutrientNbr property to the "nutrient_nbr" column in the CSV.
        Map(m => m.NutrientNbr).Name("nutrient_nbr");
                // Maps the Rank property to the "rank" column in the CSV.
        Map(m => m.Rank).Name("rank");
    }
}
