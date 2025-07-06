using webapi.Models.Management.Dao;

namespace webapi.Models.Management._Service_
{
	public class ScheduleServ : IScheduleServ
	{
		private readonly IScheduleRepo _repo;

		public ScheduleServ(IScheduleRepo repo)
		{
			_repo = repo;
		}

		public async Task <Schedule> GetById(int id) 
			=> await _repo.GetById(id);

		public async Task <IEnumerable<Schedule>> GetAll() 
			=> await _repo.GetAll();

		public async Task<bool> Update(int id, Schedule s)
		{
			if (s == null || id <= 0)
				return false;
			try
			{
				await _repo.Update(s);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<bool> Delete(int id)
		{
			if (id <= 0)
				return false;
			try
			{
				await _repo.Delete(id);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		...
	}
}