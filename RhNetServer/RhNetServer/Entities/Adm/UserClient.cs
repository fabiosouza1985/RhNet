using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetServer.Entities.Adm
{
    [Table("UserClients")]
    public class UserClient
    {
        
        [Key()]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Column("UserId", Order = 0)]
        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }


        [Column("ClientId", Order = 1)]
        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [Column("ApplicationUser")]
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [Column("Client")]
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
    }
}
