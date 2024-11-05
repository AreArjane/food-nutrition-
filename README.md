# Food Nutrition

## Overview
The **Food Nutrition** application is designed to facilitate the management and tracking of food and nutritional data. Users have various roles, each with specific permissions to manage meals and nutrition records. This README provides an overview of the model architecture and datasets integrated into the system.

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
