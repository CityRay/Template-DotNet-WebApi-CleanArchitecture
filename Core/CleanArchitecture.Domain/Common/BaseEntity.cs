using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Common
{
    public class BaseEntity : OperateTimeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public required string UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser? User { get; set; }

        [Required]
        public Guid StockId { get; set; }

        [ForeignKey("StockId")]
        public Stock? Stock { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
