using Entities.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
//para usar atributos como [MaxLength], [Display]
using System.ComponentModel.DataAnnotations.Schema; 
//para mapear nomes de colunas no banco com [Column]
namespace Entities.Entities
{
    //classe ApplicationUser herda de IdentityUser (classe do Identity que já contém Email, Password, etc.)
    public class ApplicationUser : IdentityUser
    {
        //mapeia a propriedade "CPF" para a coluna "USR_CPF" no banco
        [Column("USR_CPF")]
        [MaxLength(50)]
        //define limite máximo de caracteres
        [Display(Name = "CPF")]         
        //define nome amigável para exibição
        public string CPF { get; set; }

        [Column("USR_IDADE")]
        [Display(Name = "Idade")]
        public int Idade { get; set; }

        [Column("USR_NOME")]
        [MaxLength(255)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Column("USR_CEP")]
        [MaxLength(15)]
        [Display(Name = "CEP")]
        public string CEP { get; set; }

        [Column("USR_ENDERECO")]
        [MaxLength(255)]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Column("USR_COMPLEMENTO_ENDERECO")]
        [MaxLength(450)]
        [Display(Name = "Complemento de Endereço")]
        public string ComplementoEndereco { get; set; }

        [Column("USR_CELULAR")]
        [MaxLength(20)]
        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [Column("USR_TELEFONE")]
        [MaxLength(20)]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Column("USR_ESTADO")]
        [Display(Name = "Estado")]
        public bool Estado { get; set; }
        //indica se o usuário está ativo (true/false)

        [Column("USR_TIPO")]
        [Display(Name = "Tipo")]
        public TipoUsuario? Tipo { get; set; } 
        //eum que define o tipo de usuário (admin, comum, etc.)

    }
}
