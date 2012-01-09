using System.Data.Entity;

public class InvoiceDB : DbContext {

    public DbSet<Customer> Customers {get; set;}
    public DbSet<Provider> Providers { get; set; }
    public DbSet<Invoice> Invoices {get; set;}
    public DbSet<Deal> Deals { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<InvoiceDetails> InvoiceDetails { get; set; }

    public DbSet<User> Users { get; set; }
}