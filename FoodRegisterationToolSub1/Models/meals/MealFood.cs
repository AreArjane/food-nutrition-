using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodRegistrationToolSub1.Models.datasets;


namespace FoodRegisterationToolSub1.Models.meals {
    /// <summary>
    /// Represents the relationship between a meal and its food items.
    /// Maps to a "MealFood" table in the database.
    /// </summary>
    public class MealFood { 
     /// <summary>
        /// Gets or sets the unique identifier for the meal-food relationship.
        /// This is the primary key in the "MealFood" table.
        /// </summary>
        [Key]
        [Column("meal_food_id")]
        public int MealFoodId {get; set;}
        /// <summary>
        /// Gets or sets the ID of the food associated with the meal.
        /// Establishes a foreign key relationship with the "Food" table.
        /// </summary>
        [ForeignKey("Meal")]
        [Column("food_id")]
        public int FoodId {get; set;}
        /// <summary>
        /// Navigation property for the food associated with the meal.
        /// Provides details about the specific food item.
        /// </summary>
        public Food Food {get; set;}
       /// <summary>
        /// Gets or sets the quantity of the food included in the meal.
        /// Represents the amount of the food item within the meal.
        /// </summary>
        [Column("quantity")]
        public double Quantity {get; set;}
    }
}
