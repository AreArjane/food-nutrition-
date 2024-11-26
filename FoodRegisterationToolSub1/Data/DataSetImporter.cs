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
    /// <summary>
    /// Importing each comparer function based on the class name and type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>ComparerHelper fixed function have two method Equals and GetHash<see cref="ComparatorHelper"/></returns>
    private IEqualityComparer<T> GetComparer<T>() where T : class {
        if (typeof(T) == typeof(Food)) {
            return (IEqualityComparer<T>)new FoodComparer();
        }
        if (typeof(T) == typeof(FoodCategory)) {
            return (IEqualityComparer<T>)new FoodCategoryComparer();
        }
        if (typeof(T) == typeof(Nutrient)) {
            return (IEqualityComparer<T>)new NutrientComparer();
        }
        if (typeof(T) == typeof(FoodNutrient)) {
            return (IEqualityComparer<T>)new FoodNutrientComparer();
        }
        throw new InvalidOperationException("No comparer avaliabe for type" + typeof(T).Name);

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

            var existingRecords = _dbContext.Set<T>().AsNoTracking().ToHashSet(GetComparer<T>());
            var records = csv.GetRecords<T>().ToList();
            var newRecords = records.Where(r => !existingRecords.Contains(r)).ToList();

            foreach (var record in records)
            {
                // Validate Food records
                if (record is Food foodRecord)
                {   //check for uncatagorized food and set the value to null in food table
                    if (foodRecord.FoodCategoryId == null || !validCategoryIds.Contains(foodRecord.FoodCategoryId.Value))
                    {
                        Console.WriteLine($"Invalid FoodCategoryId {foodRecord.FoodCategoryId} for FoodId {foodRecord.FoodId}. Setting FoodCategoryId to null.");
                        foodRecord.FoodCategoryId = null;
                    }
                }

                if (record is FoodNutrient foodNutrientRecord)
                {    //skiping the ecord in FoodNutients where the ID for food is missing. This is crucial record to have a valid foodID
                    if (!validFoodIds.Contains(foodNutrientRecord.FdcId))
                    {
                        Console.WriteLine($"Skipping FoodNutrient record with id={foodNutrientRecord.Id} due to missing Food with FdcId={foodNutrientRecord.FdcId}.");
                        continue;
                    }
                    //check some FoodNutients record that do not contains nutrientsId. Set this ID to null. 
                    //In dataset it could be some food been recorded in FoodNutrients but missing nutrientsId due to deletation of Nutrients in nutrients table where USD traited as no Cascade.
                    //Where som food contains many nutrients. This application can manupulate this missing record and superuser can added manually. 
                    if (!validNutrientIds.Contains(foodNutrientRecord.NutrientId.Value))
                    {
                        Console.WriteLine($"Invalid NutrientID for id={foodNutrientRecord.Id} due to missing Nutrient with NutrientId={foodNutrientRecord.NutrientId}. Setting to null");
                        foodNutrientRecord.NutrientId = null;
                    }
                }

                
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
