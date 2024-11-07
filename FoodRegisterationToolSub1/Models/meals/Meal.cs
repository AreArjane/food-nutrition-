using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRegisterationToolSub1.Models { 
    public class Meal  { 

        [Key]
        [Column("meal_id")]
        public int MealId {get; set;}

        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name {get; set;} 

        public ICollection<MealFood> MealFoods {get; set;}
    }
}