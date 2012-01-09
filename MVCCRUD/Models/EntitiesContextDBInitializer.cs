using System.Configuration;
using System.Data.Entity;
using System.Collections.Generic;
using System;
using System.Data.Entity.Validation;

public class EntitiesContextInitializer : DropCreateDatabaseIfModelChanges<InvoiceDB>
{
    protected override void Seed(InvoiceDB context)
    {
        #region Add Users
        List<User> users = new List<User>{
            new User { Name="Default user", Login="user", Password="pass", Email="hello2@user.com", Enabled=true}
        };
        foreach (User u in users)
        {
            context.Users.Add(u);
        }
        #endregion

        #region Add sample customer data:
        List<Customer> customers = new List<Customer>
        {
            new Customer {Name="ACME International S.L", ContactPerson="Miguel Pérez", Address="12 Stree NY", CP="232323", CompanyNumber="3424324342", City="New York", Phone1="223-23232323", Fax="233-333333", Email="hello@hello.com"},
            new Customer {Name="Apple Inc.", ContactPerson="Juan Rodriguez", Address="1233 Street NY", CP="232323", CompanyNumber="23232323", City="NN CA", Phone1="343-23232323", Fax="233-333333", Email="apple@hello.com"},
            new Customer {Name="Zaragoza Activa", ContactPerson="José Ángel García", Address="Edificio: Antigua Azucarera, Mas de las Matas, 20 Planta B", CP="50015", CompanyNumber="BBBBBB", City="Zaragoza", Phone1="343-23232323", Fax="233-333333", Email="zaragozaactiva@hello.com"},
            new Customer {Name="Conecta S.L", ContactPerson="Rocío Ruíz", Address="C/ San Flores 213", CP="50800", CompanyNumber="BBBBBB", City="Zaragoza", Phone1="343-23232323", Fax="233-333333", Email="contacta@hello.com"},
            new Customer {Name="VitaminasDev", ContactPerson="Antonio Roy", Address="C/ San Pedro 79 2", CP="50800", CompanyNumber="29124609", City="Zuera, Zaragoza", Phone1="654 249068", Fax="", Email="hola@vitaminasdev.com"}
        };
        for (int i = 0; i < 5; i++)
        {
            customers.Add(new Customer()
            {
                ContactPerson="Contact person for " + i,
                Notes ="Notes for "+ i,
                Name = "Extra customer " + i,
                Address = "Address for customer" + i,
                City = "Zaragoza",
                CompanyNumber = "212121212" + i,
                CP = "50800",
                Phone1 = "2323-2222" + i,
                Email = "email@customer" + i + ".com"
            });
        }
        foreach (Customer c in customers)
        {
            context.Customers.Add(c);
        }
        #endregion

        #region Add sample providers
        List<Provider> providers=new List<Provider>();

        providers.Add(new Provider()
        {
            Name = "American Eagle Outfitters",
            Address = "Chinook Centre",
            City = "Calgary",
            CompanyNumber = "212121212",
            CP = "50800",
            Phone1 = "2323-2222",
            Email = "aeo@chinookcentre.com"
        });


        providers.Add(new Provider()
        {
            Name = "Sears",
            Address = "Market Mall",
            City = "Calgary",
            CompanyNumber = "212121212",
            CP = "50800",
            Phone1 = "2323-2222",
            Email = "sears@marketmall.com"
        });

        
        foreach (Provider p in providers)
        {
            context.Providers.Add(p);
        }
        #endregion

        #region Add sample categories
        var expense_cats = new string[] { "Men's Fashion", "Women's Fashion", "Furniture" };

        List<Category> expenseCats = new List<Category>();
        for (int ec = 0; ec < expense_cats.Length; ec++)
        {
            expenseCats.Add(new Category() { Name = expense_cats[ec], Descr = expense_cats[ec] });
        }

        foreach (Category pt in expenseCats)
        {
            context.Categories.Add(pt);
        }
        #endregion

        #region Add random deals
        var randomDescriptionPool = new string[] { "Save $100 on a new shirt", "Save $300 on a new couch", "Save $50 on a new tie", "Save $20 on a new recliner" };
        var randomImagePool = new string[] {"watches.jpeg", "jewels.jpeg", "waterbottle.jpeg", "tomatoes.jpeg"};
        
        for (int m = 1; m < DateTime.Now.Month; m++)
        {
            int expenses_count_per_month = new Random(m).Next(5, 15);
            for (int i = 0; i < expenses_count_per_month; i++)
            {
                context.Deals.Add(new Deal()
                {
                    Provider = providers[new Random(i).Next(0, providers.Count - 1)],
                    Description = randomDescriptionPool[new Random(i).Next(0, randomDescriptionPool.Length - 1)],
                    ImageURL = randomImagePool[new Random(i).Next(0, randomImagePool.Length - 1)],
                    Category = expenseCats[new Random(i).Next(0, expenseCats.Count - 1)],
                    CreatedDate = new DateTime (DateTime.Now.Year, DateTime.Now.Month, 1),
                    ExpiryDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, new Random(i).Next(2, 28))
                });
            }
        }
        #endregion

        // add data into context and save to db
        try
        {
            context.SaveChanges();
        }
        catch (DbEntityValidationException dbEx) //debug errors
        {
            foreach (var validationErrors in dbEx.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    Console.Write("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                }
            }
        }
    }
}