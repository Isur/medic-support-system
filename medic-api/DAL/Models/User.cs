using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace medic_api.DAL.Models
{
    [Table("users")]
    public partial class User
    {
        [Column("id")]
        public Guid UserId { get; set; }
        [Column("firstname")]
        public string FirstName { get; set; }
        [Column("lastname")]
        public string LastName { get; set; }
        [Column("username")]
        public string UserName { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("role")]
        public string Role { get; set; }
        
        [InverseProperty("User")]
        public List<MedicalData> MedicalDataset { get; set; }
    }
}