using hanin.Controller;

namespace hanin.Entities
{
    public class ProductEntity
    {
    
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; }
    }
}
