using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {
        // Key annotation used for this is Id
        // Required annotation Specifies that a data field value is required.
        // DisplayName annotation used to show that name on UI

        [Key]     
        public int Id { get; set; }

        [Required]
        [DisplayName("Category Name")]    
        public string Name { get; set; }

        [DisplayName("Display Order")]   
        public int DisplayOrder { get; set; }


    }
}
