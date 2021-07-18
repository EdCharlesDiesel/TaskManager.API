using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.API.Models
{
    public class ClientLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientLocationID { get; set; }
        public string ClientLocationName { get; set; }
    }
}




