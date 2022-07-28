using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    [Table("messages")]
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [StringLength(25, ErrorMessage = "Il campo non può avere più di 25 caratteri")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(25, ErrorMessage = "L'oggetto del messaggio non può avere più di 25 caratteri")]
        public string Oggetto { get; set; }

        [Required(ErrorMessage = "Il testo del messaggio non può essere vuoto")]
        [Column(TypeName = "text")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
