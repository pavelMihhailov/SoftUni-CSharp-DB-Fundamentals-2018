using System.Collections.Generic;

namespace ProductShop.Models
{
    public class Category
    {
        public Category()
        {
            CategoryProducts = new List<CategoryProduct>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}
