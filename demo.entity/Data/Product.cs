using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Demo.Entity.Data;

[Table("products")]
public partial class Product
{
    [Key]
    [Column("productid")]
    public int Productid { get; set; }

    [Column("productname")]
    [StringLength(100)]
    public string Productname { get; set; } = null!;

    [Column("price")]
    [Precision(10, 2)]
    public decimal Price { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("categoryid")]
    public int? Categoryid { get; set; }

    [Column("brandid")]
    public int? Brandid { get; set; }

    [ForeignKey("Brandid")]
    [InverseProperty("Products")]
    public virtual Brand? Brand { get; set; }

    [ForeignKey("Categoryid")]
    [InverseProperty("Products")]
    public virtual Category? Category { get; set; }
}
