﻿namespace MyCity.DataAccess.Models;

public class Location : BaseEntity<Guid>
{
    public string Name { get; set; }
    
    double X_Position { get; set; }
    
    double Y_Position { get; set; }
    
    string Description { get; set; }
}