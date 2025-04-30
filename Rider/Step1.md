# Step 1: Create Project

In this step, you are going to create an empty ASP.NET Core project to base the server on.


## Fork the repository to your own GitHub
If you want to keep the sources you create during this workshop, I recommend forking the repository to your own GitHub environment.

## Create a new Empty Web project
In the folder for this workshop, you will have to create a project and solution. Because HotCholocate has a very good integration with ASP.NET Core, we are going to use the Empty template to start with. 

![project template](./images/Create%20empty%20app.png)

The description in this workshop uses the project name ShopAPI, so create a project with that name. You can give the solution the same name. Do make sure that you select the correct folder to create the solution and project in.

In the additional settings, you should select .NET 9 as the framework. We are not going to use containers or .NET Aspire in the workshop, so leave these settings unchecked.

## Generated code

The code in Program.cs should like to following image.

![Generated code](./images/Generated%20code.png)

## Run test project

To make sure that the code generation works, run the project and verify that Hello World is shown in the browser.

![Running code](./images/Running%20code.png)


[Next step](./Step2.md)
