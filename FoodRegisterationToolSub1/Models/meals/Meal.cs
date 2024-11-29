using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodRegisterationToolSub1.Models.users;


namespace FoodRegisterationToolSub1.Models.meals {
    /// <summary>
    /// Represents a meal entity, including its name, associated user, and foods.
    /// Maps to a "Meal" table in the database.
    /// </summary>
    public class Meal  { 
    /// <summary>
        /// Gets or sets the unique identifier for the meal.
        /// This is the primary key in the "Meal" table.
        /// </summary>
        [Key]
        [Column("meal_id")]
        public int MealId {get; set;}
        /// <summary>
        /// Gets or sets the name of the meal.
        /// This field is required and has a maximum length of 100 characters.
        /// </summary>
        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name {get; set;} 
        /// <summary>
        /// Gets or sets the ID of the user associated with the meal.
        /// Maps to the "user_id" column in the database.
        /// </summary>
        [Column("user_id")]
        public int UserId {get; set;}
       /// <summary>
        /// Navigation property for the user associated with the meal.
        /// Establishes a foreign key relationship with the "User" entity.
        /// </summary>
        [ForeignKey("UserId")]
        public User? User {get; set;}
        /// <summary>
        /// Navigation property for the collection of foods included in the meal.
        /// Represents a one-to-many relationship with the "MealFood" entity.
        /// </summary>
        public ICollection<MealFood> MealFoods {get; set;}
    }
}
