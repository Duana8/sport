using webapi.Models.Persons;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using webapi.Models.Attribute;

namespace webapi.Models.Management
{
	public enum AccessRights
	{
		[Display(Name = "Не указано")]
		Не_указано = 0,
		[Display(Name = "Владелец")]
		Владелец,
		[Display(Name = "Администратор")]
		Администратор,
		[Display(Name = "Тренер")]
		Тренер,
		[Display(Name = "Клиент")]
		Клиент
	}
	...
}