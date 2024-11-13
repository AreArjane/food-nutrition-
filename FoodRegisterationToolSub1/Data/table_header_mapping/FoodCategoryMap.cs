using CsvHelper.Configuration;
using FoodRegisterationToolSub1.Models.datasets;
/// <summary>
/// CSV-mapping for <see cref="FoodCategory"/>-klassen for å lette import og eksport av data.
/// Definerer hvordan <see cref="FoodCategory"/>-objekter skal kobles til CSV-filkolonner.
/// </summary>

public class FoodCategoryMap : ClassMap<FoodCategory> {
/// <summary>
    /// Initialiserer en ny instans av <see cref="FoodCategoryMap"/>-klassen og spesifiserer CSV-kolonnenavn
    /// for hver egenskap i <see cref="FoodCategory"/>.
    /// </summary>
    public FoodCategoryMap() {
        Map(m => m.Id).Name("id");
        Map(m => m.Code).Name("code");
        Map(m => m.Description).Name("description");
    }
}
