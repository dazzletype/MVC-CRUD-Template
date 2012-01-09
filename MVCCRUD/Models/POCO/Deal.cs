using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

public class Deal
{
    public int DealID { get; set; }

    [Required]
    public string Description {get;set;}

    public int ProviderID { get; set; }
    public virtual Provider Provider { get; set; }

    public string Notes { get; set; }

    [DisplayName("Image Link")]
    public string ImageURL { get; set; }

    [DisplayName("Created")]
    public DateTime CreatedDate { get; set; }


    [DisplayName("Expires")]
    public DateTime ExpiryDate { get; set; }
    
    public int PurchaseTypeID { get; set; }

    [DisplayName("Category")]
    public virtual Category Category { get; set; }


    [DisplayName("Facebook Link")]
    public string FacebookURL { get; set; }

    [DisplayName("Twitter Share Link")]
    public string TwitterURL { get; set; }

 }