# MyList

A simple web application for creating and managing lists. 

## Features

* Create multiple lists
* Add items to lists with names, descriptions, and "done" status
* View and manage lists
* Delete individual items or entire lists
* Uses a SQLite db with a Blazor front end.
* ASP.net API with swagger documentation.
* Local Appdata logging.

## How to Run the Application

1. **Prerequisites:**
   - Make sure you have the .NET 8 SDK installed on your system.

2. **Build and Run:**
   - Open the project in your IDE (e.g., Visual Studio, Rider).
   - Build the solution.
   - Run the application.

3. **Access the Application:**
   - Open a web browser and navigate to the application's URL (e.g., `https://localhost:5001`).

## Publishing the Application

1. **Publish:**
   - In your IDE, publish the application to a folder.

2. **Run:**
   - Navigate to the publish folder and run the `.exe` file.

3. **Access:**
   - Open a web browser and navigate to the application's URL (usually `https://localhost:5001` or similar).

## Using the Application

1. **Home Page:**
   - The home page provides a brief overview of the application.
   - Click the "Create a New List" button to add a new list.

2. **Add List Page (`/AddGift`)**
   - Enter a name for your new list in the "List Name" field and click "Add List".
   - To add items to an existing list:
     - Select the list from the "Select List" dropdown.
     - Enter the item name, description, and check the "Is Done" box if needed.
     - Click "Add Item to List".

3. **View and Manage Lists Page (`/Gifts`)**
   - This page displays all your lists and their items.
   - You can mark items as "Done" by checking the checkbox next to them.

4. **Delete Lists Page (`/DeleteList`)**
   - This page allows you to delete entire lists or individual items.
   - To delete a list, select it from the dropdown and click "Delete List".
   - To delete individual items, check the boxes next to them and click "Delete Selected Items".

## API Documentation

* The application also provides an API for managing lists and items.
* You can access the API documentation through Swagger UI at `/swagger`.

## Contributing

* If you find any issues or have suggestions for improvement, please feel free to open an issue or submit a pull request on the GitHub repository.
