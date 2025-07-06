using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Runtime.CompilerServices;
using webapi.Models.Context;

namespace webapi.Models.Management.Dao
{
	public class ScheduleRepo : IScheduleRepo
	{
		private readonly ApplicContext _context;

		public ScheduleRepo(ApplicContext context)
		{
			_context = context;
		}
	
	        public async Task<Schedule> GetById(int id)
		{
			return await _context.schedule
				.Include(s => s.staff)
				.Include(s => s.type_lesson)
				.FirstAsync(s => s.id == id);
		}

		public async Task<IEnumerable<Schedule>> GetAll()
		{
			return await _context.schedule
				.Include(s => s.staff)
				.Include(s => s.type_lesson)
				.ToListAsync();
		}

			public async Task Add(Schedule s)
		{
			using var connection = _context.Database.GetDbConnection();
			await connection.OpenAsync();

			using var command = connection.CreateCommand();
			command.CommandText = "CALL public.addschedule(@type_lesson_id, @staff_id, @length_time, @date_time)";
			command.CommandType = CommandType.Text;

			command.Parameters.Add(new Npgsql.NpgsqlParameter("type_lesson_id", NpgsqlTypes.NpgsqlDbType.Integer) { Value = s.type_lesson_id });
			command.Parameters.Add(new Npgsql.NpgsqlParameter("staff_id", NpgsqlTypes.NpgsqlDbType.Integer) { Value = s.staff_id });
			command.Parameters.Add(new Npgsql.NpgsqlParameter("length_time", NpgsqlTypes.NpgsqlDbType.Time) { Value = s.length_time });
			command.Parameters.Add(new Npgsql.NpgsqlParameter("date_time", NpgsqlTypes.NpgsqlDbType.Timestamp) { Value = s.date_time });

			await command.ExecuteNonQueryAsync();
		}

		public async Task Update(Schedule s)
		{
			using var connection = _context.Database.GetDbConnection();
			await connection.OpenAsync();

			using var command = connection.CreateCommand();
			command.CommandText = "CALL public.updschedule(@id, @type_lesson_id, @staff_id, @length_time, @date_time)";
			command.CommandType = CommandType.Text;

			command.Parameters.Add(new Npgsql.NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Integer) { Value = s.id });
			command.Parameters.Add(new Npgsql.NpgsqlParameter("type_lesson_id", NpgsqlTypes.NpgsqlDbType.Integer) { Value = s.type_lesson_id });
			command.Parameters.Add(new Npgsql.NpgsqlParameter("staff_id", NpgsqlTypes.NpgsqlDbType.Integer) { Value = s.staff_id });
			command.Parameters.Add(new Npgsql.NpgsqlParameter("length_time", NpgsqlTypes.NpgsqlDbType.Time) { Value = s.length_time });
			command.Parameters.Add(new Npgsql.NpgsqlParameter("date_time", NpgsqlTypes.NpgsqlDbType.Timestamp) { Value = s.date_time });

			await command.ExecuteNonQueryAsync();
		}

		public async Task Delete(int id)
		{
			await _context.Database.ExecuteSqlInterpolatedAsync(
				FormattableStringFactory.Create($@"CALL public.delschedule
				({{0}})", id));
		}
		...
	}
}