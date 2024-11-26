using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodRegisterationToolSub1.Models.users;


namespace FoodRegisterationToolSub1.Models.meals {
    public class Meal  { 

        [Key]
        [Column("meal_id")]
        public int MealId {get; set;}

        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name {get; set;} 

        [Column("user_id")]
        public int UserId {get; set;}
        [ForeignKey("UserId")]
        public User? User {get; set;}

        public ICollection<MealFood> MealFoods {get; set;}
    }
}