﻿using Microsoft.EntityFrameworkCore;
namespace loyaltycard.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Brand { get; set; }
    public int Price { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
}
