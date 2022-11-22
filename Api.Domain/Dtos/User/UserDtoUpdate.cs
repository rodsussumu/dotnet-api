using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Dtos.User
{
    public class UserDtoUpdate
    {
        [Required(ErrorMessage = "Id é obrigatorio")] 
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatorio")]
        [StringLength(60, ErrorMessage = "Nome deve ter no maximo {1} caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email é obrigatorio")]
        [StringLength(100, ErrorMessage = "Email deve ter no maximo {1} caracteres")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }
    }
}
