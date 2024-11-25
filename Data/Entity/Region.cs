using System.ComponentModel.DataAnnotations.Schema;

namespace SerhendKumbara.Data.Entity
{
    public class Region
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegionID { get; set; }
        [Column(TypeName = "varchar")]
        public string Name { get; set; }
    }
}
