using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Demo.Entity.Data;

[Table("brands")]
public partial class Brand
{
    [Key]
    [Column("brandid")]
    public int Brandid { get; set; }

    [Column("brandname")]
    [StringLength(255)]
    public string Brandname { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [InverseProperty("Brand")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
