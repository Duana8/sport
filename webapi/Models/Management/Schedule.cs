using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapi.Models.Persons;

namespace webapi.Models.Management
{
	public class Schedule
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int id { get; set; }

		[Required(ErrorMessage = "Обязательно для заполнения.")]
		[Display(Name = "Вид занятия")]
		public int type_lesson_id { get; set; }

		[ForeignKey("type_lesson_id")]
		public TypeLesson type_lesson { get; set; }

		[Required(ErrorMessage = "Обязательно для заполнения.")]
		[Display(Name = "Персонал")]
		public int staff_id { get; set; }

		[ForeignKey("staff_id")]
		public Staff staff { get; set; }

		[DataType(DataType.Time)]
		[DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
		[Range(typeof(TimeSpan), "00:00:00", "23:59:59",
		ErrorMessage = "Время должно быть в диапазоне от 00:00 до 23:59.")]
		[Display(Name = "Длительность")]
		public TimeSpan length_time { get; set; }

		[DataType(DataType.DateTime)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
		//[DateAttr]
		[Display(Name = "Дата и Время")]
		public DateTime date_time { get; set; }

		public Schedule() { }

		public Schedule(int type_lesson_id, 
			int staff_id, TimeSpan length_time, 
			DateTime date_time)
		{
			this.type_lesson_id = type_lesson_id;
			this.staff_id = staff_id;
			this.length_time = length_time;
			this.date_time = date_time;
		}
	}
}