# OnBoardingTask2018Jun   
On Boarding Task of MVP Studio, Jun 2018   

Learning conclusion:   
•	ASP.NET MVC Entity Framework application with Visual Studio and connect it to SQL Server    
•	Use bootstrap modals in views     
•	Add data annotations to the models   
•	Use jQuery, JavaScript and AJAX calls to communicate with controllers and support functions on the page    
•	Learn to use knockout with knockout frontend validation   
   
Instructions:    
•	According to model diagram, set up an ASP.NET MVC Entity Framework project that is connected to SQL Server (database first approach)   
•	Data annotations applied to each data model (Customer/Product/Store name is required, Sale date is required)   
•	Create pages for Product, Customer, Store and Sales that have CRUD (create read update and delete) functionality   
•	Use Bootstrap theme and Bootstrap modals for each different function (Customer, Store and Sale)   
•	Use JavaScript, jQuery, AJAX to accomplish this project (Customer, Store and Sale)   
•	The Sale page has 3 drop down lists that have the customers, stores and products in and an input for the date the sale occurred (must be current date or previous date)   
•	Apply Knockout data-binding & Knockout frontend validation (KO data-binding:Sale, KO validation: sold date)   
•	Upload this finished project to GitHub   
•	Publish this project in Azure   
   
Entity Framework data model diagram:   
![](https://github.com/Z-Zhao/OnBoardingTask2018Jun/raw/master/Data%20Model%20Diagram.jpg)
   
Technical summary:   
Note: X - technic used  
<table>
   <thead>
      <tr> <th >Name:</th> <th >Product</th><th >Customer</th><th >Store</th><th >Sale</th> </tr>
   </thead>
   <tbody>
      <tr> <th >ASP.NET MVC</th>     <th >X</th><th >X</th><th >X</th><th >X</th> </tr>
      <tr> <th >MVVM</th>            <th > </th><th > </th><th > </th><th >X</th> </tr>
      <tr> <th >Entity Framework</th><th >X</th><th >X</th><th >X</th><th >X</th> </tr>
      <tr> <th >Data annotation</th> <th >X</th><th >X</th><th >X</th><th >X</th> </tr>
      <tr> <th >CRUD</th>            <th >X</th><th >X</th><th >X</th><th >X</th> </tr>
      <tr> <th >Bootstrap</th>       <th > </th><th >X</th><th >X</th><th >X</th> </tr>
      <tr> <th >JavaScript</th>         <th > </th><th >X</th><th >X</th><th >X</th> </tr>
      <tr> <th >jQuery</th>          <th > </th><th >X</th><th >X</th><th >X</th> </tr>
      <tr> <th >@Model pass data</th><th >X</th><th >X</th><th > </th><th > </th> </tr>
      <tr> <th >AJAX/Json</th>       <th > </th><th > </th><th >X</th><th >X</th> </tr>
      <tr> <th >Knockout</th>        <th > </th><th > </th><th > </th><th >X</th> </tr>
   </tbody>
</table>   
   
Herb Z Zhao   
9-Jul-2018   
