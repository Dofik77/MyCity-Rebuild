using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCity.DataAccess.Models;

public class BaseEntity<TType> : IBaseEntity
{
    public BaseEntity()
    {
        DateCreated = DateTime.UtcNow;
    }

    /// <summary>
    ///     Идентификатор записи
    /// </summary>
    [Key]
    public TType Id { get; set; }

    /// <summary>
    ///     Признак активности записи
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    ///     Дата создания записи
    /// </summary>
    [Column(TypeName = "timestamp without time zone")]
    public DateTime DateCreated { get; set; }
}
