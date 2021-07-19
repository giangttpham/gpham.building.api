# Buildings API

## Run the API and the UI app together
- Check out the UI code project at https://github.com/giangttpham/gpham-buildings-ui. Follow the instructions in the UI app's README file to run this API and the UI app together.
## Projects

- Buildings.Entities
  - Class Library
  - Representations for all database entities
- Buildings.Web
  - ASP.NET Core Web Application
  - Main program, business logic and components necessary to run the web app (controllers, repositories, etc...)
- Buildings.IntegrationTests
  - Unit Test Project
  - Integration tests for the web app
    
## How to run
- **Option 1**: Open the solution file using an IDE (e.g Visual Studio, JetBrains Rider) and run through the IDE
- **Option 2**: Open a command line window on the Buildings.Web project folder, execute "dotnet run" command

## How to test
- **Option 1**: Run the app and the tests through an IDE
- **Option 2**: 
    - Open a command line window on the Buildings.Web project folder, execute "dotnet run" command.
    - Open a separate CMD window on the Buildings.IntegrationTests project folder, execute "dotnet test".