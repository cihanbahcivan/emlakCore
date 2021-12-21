using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CoreLayer.Entities;


namespace EntityLayer.Concrete
{
    [Table("Categories")]
    public class Category : IEntity
    {
        [Key]
        public long CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
