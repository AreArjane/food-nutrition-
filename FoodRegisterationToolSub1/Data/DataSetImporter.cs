using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using FoodRegistrationToolSub1.Models.datasets;
using FoodRegisterationToolSub1.Models.datasets;

public class DataSetImporter
{
    private readonly ApplicationDbContext _dbContext;
    private readonly string dataDir = Path.Combine(AppContext.BaseDirectory, "../../../Data/datasets_tmp_files");

    public DataSetImporter(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void ImportData()
    {
        ImportCsv<FoodCategory, FoodCategoryMap>("food_category.csv");
        ImportCsv<Food, FoodMap>("food.csv");
        ImportCsv<Nutrient, NutrientMap>("nutrient.csv");
        ImportCsv<FoodNutrient, FoodNutrientMap>("food_nutrient.csv");
    }

    public void ImportCsv<T, TMap>(string filename) where T : class where TMap : ClassMap<T>, new()
{
    string filePath = Path.Combine(dataDir, filename);

    if (!File.Exists(filePath))
    {
        Console.WriteLine($"File {filePath} does not exist.");
        return;
    }

    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        MissingFieldFound = null,
        HeaderValidated = null,
        BadDataFound = null,
        PrepareHeaderForMatch = args => args.Header.ToLower(),
        IgnoreBlankLines = true,
    };

    using (var reader = new StreamReader(filePath))
    using (var csv = new CsvReader(reader, config))
    {
        csv.Context.RegisterClassMap<TMap>();

        // Load all existing FoodCategory IDs into a HashSet for quick lookups
        var validCategoryIds = new HashSet<int>(_dbContext.FoodCategories.Select(c => c.Id));

        // Load existing records to check for duplicates
        var existingRecords = _dbContext.Set<T>().AsNoTracking().ToList();
        var records = csv.GetRecords<T>().ToList();
        var newRecords = new List<T>();

        foreach (var record in records)
        {
            if (record is Food foodRecord)
            {
                // Skip records with null or invalid FoodCategoryId
                if (foodRecord.FoodCategoryId == null || !validCategoryIds.Contains(foodRecord.FoodCategoryId.Value))
                {
                    Console.WriteLine($"Skipping Food record with fdc_id={foodRecord.FoodId} due to invalid or missing FoodCategoryId.");
                    continue;
                }
            }

            // Add only unique records
            if (!existingRecords.Contains(record))
            {
                newRecords.Add(record);
            }
        }

        if (newRecords.Any())
        {
            _dbContext.Set<T>().AddRange(newRecords);
            _dbContext.SaveChanges();
            Console.WriteLine($"{newRecords.Count} new records added to {typeof(T).Name}.");
        }
        else
        {
            Console.WriteLine($"No new records to add for {typeof(T).Name}.");
        }
    }

    File.Delete(filePath);
    Console.WriteLine($"File {filePath} imported to database and successfully deleted.");
}
}