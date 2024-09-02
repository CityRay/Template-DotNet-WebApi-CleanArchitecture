using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Common
{
    public class BaseEntity : OperateTimeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public Guid StockId { get; set; }
        public Stock Stock { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
