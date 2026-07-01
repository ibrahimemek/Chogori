using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }
        public Guid? ParentCategoryId { get; private set; }

        private Category() { }
        public Category(Guid id, string name, Guid? parentCategoryId = null)
        {
            if (string.IsNullOrWhiteSpace(name)) 
                throw new ArgumentNullException(nameof(name), "Category name connet be empty.");

            this.Id = id;
            this.Name = name;
            this.ParentCategoryId = parentCategoryId;
        }
    }
}
