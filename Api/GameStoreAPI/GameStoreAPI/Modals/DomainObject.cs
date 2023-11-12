using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameStoreAPi.Modals.SKU
{
    public class DomainObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid id { get; set; } = Guid.Empty;
        public DateTime? CreatedDate { get; set; } = null;
        public DateTime? UpdatedDate { get; set; } = null;
    }
}
