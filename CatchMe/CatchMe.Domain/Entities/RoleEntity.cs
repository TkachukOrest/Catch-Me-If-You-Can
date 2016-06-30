using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CatchMe.Domain.Entities
{
    [Table("Role")]
    public class RoleEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        #region Navigation properties 
        [JsonIgnore]
        public virtual ICollection<UserEntity> User { get; set; }        
        #endregion
    }
}
