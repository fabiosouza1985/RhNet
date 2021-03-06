﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Entities.Adm
{
    [Table("ApplicationMenus")]
    public class ApplicationMenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }
        
        [Column("Header")]
        [Required]
        [StringLength(100)]
        public string Header { get; set; }

        [Column("Path")]
        [Required]
        [StringLength(100)]
        public string Path { get; set; }

        [Column("Role_Name", TypeName ="nvarchar(256)")]
        [Required]
        public string Role_Name { get; set; }

        [Column("Permission_Name")]
        [Required]
        [StringLength(100)]
        public string Permission_Name { get; set; }

        [Column("Quick_Access")]
        public bool? Quick_Access { get; set; }
    }
}
