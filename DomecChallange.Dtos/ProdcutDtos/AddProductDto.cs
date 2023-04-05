using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomecChallange.Dtos.ProdcutDtos
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
    public class EditProductDto
    {
        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
