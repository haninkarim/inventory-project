using System.ComponentModel.DataAnnotations;

namespace hanin.Entities
{
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        
        public ICollection<ProductEntity> Products { get; set; }
    }
}
