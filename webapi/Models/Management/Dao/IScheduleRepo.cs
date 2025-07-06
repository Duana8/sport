using webapi.Models.Context._Interface_;

namespace webapi.Models.Management.Dao
{
	public interface IScheduleRepo :IRepo<Schedule>
	{
		Task<IEnumerable<Schedule>> ScheduleTrainer(int staff_id);
		Task<IEnumerable<ScheduleDTO>> GetScheduleOfWeek(IEnumerable<Visit> visits);
    }
}