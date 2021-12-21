using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CoreLayer.Entities;

namespace EntityLayer.Concrete
{
    [Table("Cities")]
    public class City : IEntity
    {
        [Key]
        public long CityId { get; set; }
        public string CityNumber { get; set; }
        public string CityName { get; set; }
    }
}
