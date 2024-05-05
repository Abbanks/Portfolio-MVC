## Portfolio-MVC

## Project Overview
Portfolio is a website designed to offer insights into an individual's background and experiences as a software developer. It acts as a showcase for implementing the MVC architecture pattern using ASP.NET Core, along with various technologies like ASP.NET Identity for User Authentication and Authorization, dependency injection, middleware, routing, configuration, HTML, and CSS. The project also integrates working with databases using technologies like Entity Framework Core and LINQ for data access, along with adhering to software engineering principles such as SOLID principles and design patterns. Additionally, it includes services like an Email Service and a Generic Repository.

## Project Layout
**Guest's Page**: Consists of three main pages:
- Home Page
- Profile Page (Work History Page)
- Contact Page
  
**Admin's Page**: Consists of a single page where admins can edit their details and work history. This is where the data is inputted to be viewed on the Guest's page.

## Features
Porfolio includes the following key features:

- User details (name, email, phoneNumber, address, etc.) can be added using a form and are stored in the database.
- Users have the ability to edit their details.
- Work history can be collected using a form on the profile page and stored in the database.
- Users can add, update, and delete work history entries on the profile page.
- Users can send mail to the website owner through the contact page.
- Users can Signup / Register
- Confirm password
- Login.
- Forgot password.
- Reset password.
  
## Libraries and Framework used
The project leverages the following libraries and framework:
- ASP.NET Core
- Entity Framework Core
- MailKit
- Microsoft SQL Server

## Demo

## The Guest's page
![Demo](guest.gif)

## The Admin's page
![Demo](dashboard.gif)
