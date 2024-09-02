using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAAI0731.Models
{
    public abstract class Abstract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreateAt { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? UpdateAt { get; set; }

        [StringLength(100)]
        public string UpdatedBy { get; set; }
    }
}