# Food Nutrition

## Models

### Models Architecture 


The models conatins three user type : 
1. NormalUser : has a minimal access to the application, can add new meals, edit the meals. Request new Food and Nutriation.
2. SuperUser : are the employess of the system who suport user in basic requset to add new Food and nutriation. SuperUser has permission on NormalUser registeret information and can update FirstName and LastName after UserRequest.
3. AdminUser : Are the owner for the system, have a full access to the system functionality and configuration. Can delete, remove user after NormalUser && SuperUser request. Manage the block and span on user after abuse case. Can manage dataset and add new dataset to the system


### Datasets

The dataset use with the application are from : 


[https://fdc.nal.usda.gov/download-datasets.html]

This dataset contain a light food with nutriation value. Part of the dataset are implemented in the system : 

Food, Nutriation, FoodNutriation, FoodCategory. Database models being defined respectfull to this models.

#### Purpose of the datasets
The purpose for implementing dataset in the system relay on : 
1. Support NormalUser when adding new meals in the system, as the food and nutriation are predefined.
2. NormalUser can request new Food they create to add to the system, which will help other user to add their meals. 
