using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EntityLayer.Abstract;

namespace EntityLayer.Concrete
{
    [Table("Posts")]
    public class Post : IEntity
    {
        [Key]
        public long PostId { get; set; }
        public long CategoryId { get; set; }
        public bool Balcony { get; set; }
        public int Floor { get; set; }
        public string RoomCount { get; set; }
        public int BuildingAge { get; set; }
        public bool InSite { get; set; }
        public bool HasFurniture { get; set; }
        public int BathroomCount { get; set; }
        public int SquareMeters { get; set; }
        public DateTime PostDate { get; set; }
        public bool Vacancy { get; set; }
        public string HeatId { get; set; }
        public int Price { get; set; }
        public int OldPrice { get; set; }
        public int Viewing { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public virtual User User { get; set; }
        public virtual Heat Heat { get; set; }
        public virtual List<Image> Images { get; set; }
        public virtual Category Categories { get; set; }
    }
}
