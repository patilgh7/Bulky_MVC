using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
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
        [MaxLength(30)]
        public string Name { get; set; }


        [DisplayName("Display Order")]
        [Range(1, 100)]
        public int DisplayOrder { get; set; }


    }
}
