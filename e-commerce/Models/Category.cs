using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace e_commerce.Models
{
    public class Category : BaseEntity
    {
        [DisplayName("Category Name")]
        
        public string Name { get; set; }

        [DisplayName("Description")]
        
        public string Description { get; set; }
    }
}
