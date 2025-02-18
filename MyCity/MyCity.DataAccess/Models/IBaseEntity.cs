namespace MyCity.DataAccess.Models;

public interface IBaseEntity
{
    bool IsActive { get; set; } 
    DateTime DateCreated { get; set; }
}