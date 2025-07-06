using webapi.Models.Context._Interface_;

namespace webapi.Models.Management._Service_
{
	public interface IScheduleServ :IServ<Schedule>
	{
		Task<IEnumerable<ScheduleDTO>> GetScheduleOfWeek(IEnumerable<Visit> visits);
        Task<IEnumerable<Schedule>> ScheduleTrainer(int id);
	}
}