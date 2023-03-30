using FastFood.Common;

namespace FastFood.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public Category()
        {
            this.Items = new HashSet<Item>();
        }
        [Key]
        public int Id { get; set; }

        [StringLength(ValidationConstants.CategoryMaxLength, MinimumLength = 3)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Item> Items { get; set; }
    }
}
