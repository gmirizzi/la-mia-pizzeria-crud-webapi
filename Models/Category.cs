using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace la_mia_pizzeria_static.Models
{
	[Table("categories")]
	public class Category
	{
		[Key]
		public int CategoryId { get; set; }
		[Required]
		[StringLength(20)]
		public string Name { get; set; }
		[JsonIgnore]
		public List<Pizza>? PizzasList { get; set; }
	}
}
