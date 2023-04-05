using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomecChallange.Domain.Entities
{
    public class Product
    {
        public Product()
        {
            this.UniqueId = Guid.NewGuid();
        }
        [Key]
        public Guid UniqueId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
