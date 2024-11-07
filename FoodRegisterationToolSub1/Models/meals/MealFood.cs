using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FoodRegisterationToolSub1.Models { 

    public class MealFood { 

        [Key]
        [Column("meal_food_id")]
        public int MealFoodId {get; set;}

        [ForeignKey("Meal")]
        [Column("food_id")]
        public int FoodId {get; set;}
        public Food Food {get; set;}

        [Column("quantity")]
        public double Quantity {get; set;}
    }
}