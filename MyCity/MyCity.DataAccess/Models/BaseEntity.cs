namespace MyCity.DataAccess.Models;

public class BaseEntity<T>
{
    public T Id { get; set; }
    public bool IsValid { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}