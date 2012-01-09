using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;

public class Category
{
    public int CategoryID { get; set; }

    [Required]
    public string Name { get; set; }

    public string Descr { get; set; }

    public virtual ICollection<Deal> Purchases { get; set; }
}