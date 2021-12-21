using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CoreLayer.Entities;


namespace EntityLayer.Concrete
{
    [Table("Images")]
    public class Image : IEntity
    {
        [Key]
        public long ImageId { get; set; }
        public long PostId { get; set; }
        public string ImageFile { get; set; }
        public virtual Post Posts { get; set; }
    }
}
