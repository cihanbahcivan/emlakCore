using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Abstract;

namespace EntityLayer.Concrete
{
    public class Heat : IEntity
    {
        [Key] 
        public long HeadId { get; set; }
        public string Title { get; set; }

    }
}
