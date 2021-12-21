using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EntityLayer.Abstract;

namespace EntityLayer.Concrete
{
    [Table("Districts")]
    public class District : IEntity
    {
        [Key]
        public long DistrictId { get; set; }
        public string Title { get; set; }
        public long CityId { get; set; }
        public virtual City City { get; set; }
    }
}
