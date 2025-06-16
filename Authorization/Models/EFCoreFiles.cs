using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authorization.Models;
public class EFCoreFiles;

[Table("UserDatas")]
public class UserData
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Column("Login")]
    public string Login { get; set; }
    [Column("Password")]
    public string Password { get; set; }
}

