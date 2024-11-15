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
        ImportCsv<Nutrient, NutrientMap>("nutrient.csv");
        ImportCsv<FoodCategory, FoodCategoryMap>("food_category.csv");
        ImportCsv<Food, FoodMap>("food.csv");
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

           
            var validCategoryIds = new HashSet<int>(_dbContext.FoodCategories.Select(c => c.Id));
            var validFoodIds = new HashSet<int>(_dbContext.Foods.Select(f => f.FoodId));
            var validNutrientIds = new HashSet<int>(_dbContext.Nutrients.Select(n => n.Id));

            var existingRecords = _dbContext.Set<T>().AsNoTracking().ToList();
            var records = csv.GetRecords<T>().ToList();
            var newRecords = new List<T>();

            foreach (var record in records)
            {
                // Validate Food records
                if (record is Food foodRecord)
                {
                    if (foodRecord.FoodCategoryId == null || !validCategoryIds.Contains(foodRecord.FoodCategoryId.Value))
                    {
                        Console.WriteLine($"Skipping Food record with fdc_id={foodRecord.FoodId} due to invalid or missing FoodCategoryId.");
                        continue;
                    }
                }

                if (record is FoodNutrient foodNutrientRecord)
                {
                    if (!validFoodIds.Contains(foodNutrientRecord.FdcId))
                    {
                        Console.WriteLine($"Skipping FoodNutrient record with id={foodNutrientRecord.Id} due to missing Food with FdcId={foodNutrientRecord.FdcId}.");
                        continue;
                    }

                    if (!validNutrientIds.Contains(foodNutrientRecord.NutrientId))
                    {
                        Console.WriteLine($"Skipping FoodNutrient record with id={foodNutrientRecord.Id} due to missing Nutrient with NutrientId={foodNutrientRecord.NutrientId}.");
                        continue;
                    }
                }

                // Add only unique records that pass validation
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
