# Food Nutrition

## Overview
The **Food Nutrition** application is designed to facilitate the management and tracking of food and nutritional data. Users have various roles, each with specific permissions to manage meals and nutrition records. This README provides an overview of the model architecture and datasets integrated into the system. It developed with .NET and follow MVC architecture.
# Food Registration Tool API

This API allows users to interact with food and meal data, including searching, creating, reading, updating, and deleting food items and meals, along with associated nutrient information.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- PostgreSQL database configured

## Setup

### 1. Install Entity Framework CLI Tool

```bash
dotnet tool install --global dotnet-ef
export PATH="$PATH:/Users/username/.dotnet/tools"
```

### Apply Migrations and Update Database

Generate migrations and update the database schema using the following commands:

```bash
dotnet ef migrations add <MigrationName> --context ApplicationDbContext
dotnet ef database update --context ApplicationDbContext
```

## Models

### Model Architecture

The application supports three types of users, each with distinct roles and permissions:

- **NormalUser**
  - Basic access to the application.
  - Can add and edit new meals.
  - Can request new food items and nutrition data to be added to the system.

- **SuperUser**
  - System employees who support NormalUser requests to add new food items and nutrition data.
  - Can access and update certain NormalUser details (e.g., first name and last name) following a UserRequest.

- **AdminUser**
  - System owners with full access to all functionalities and configurations.
  - Can manage user data, including deleting or removing users after requests from NormalUsers or SuperUsers.
  - Has authority to manage user blocking and suspension in cases of misuse or abuse.
  - Oversees dataset management and can add new datasets to the system.

## Datasets

The datasets used in the application are sourced from:
[USDA FoodData Central](https://fdc.nal.usda.gov/download-datasets.html)

This dataset includes essential food items and nutrition values. The following models are implemented in the system in alignment with these datasets:

- **Food**
- **Nutrition**
- **FoodNutrition** (linking foods with their nutritional values)
- **FoodCategory**

### Purpose of the Datasets

The integration of datasets serves the following purposes:

1. **User Support**: 
   - Predefined food and nutrition data assist NormalUsers when adding new meals.
   
2. **Requesting New Foods**: 
   - NormalUsers can request new foods they create to be added to the system, making them available for other users as well.

This setup provides a structured, user-friendly approach to managing and accessing food and nutrition data within the application.


## Controller 

## Functional Controllers

### 1. SearchController

The `SearchController` handles user queries, allowing users to search for foods and meals by entering a keyword. It filters the results based on the query to return matching `Food` and `Meal` entries.

### 2. FoodController

The `FoodController` provides endpoints for manipulating individual food items. It supports creating, reading, updating, and deleting food records, along with managing associated nutrient data for each food item.

### 3. MealController

The `MealController` allows manipulation of individual meal records. Each meal can contain multiple food items and nutrients, and this controller provides functionality to create, read, update, and delete meal entries along with their associated food and nutrient details.


## User Controller

### NormalUserController 

controller the profile of the user, user access to own data, and CRUD for Meals. Make request to oeprate new food items. 

### SuperUserController

### AdminUserController