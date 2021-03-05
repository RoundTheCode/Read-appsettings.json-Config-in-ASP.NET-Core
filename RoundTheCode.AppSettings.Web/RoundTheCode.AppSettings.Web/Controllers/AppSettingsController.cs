using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RoundTheCode.AppSettings.Web.Config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTheCode.AppSettings.Web.Controllers
{

	[Route("appsettings")]
	public class AppSettingsController : Controller
	{
		protected readonly IConfiguration _configuration;
		protected readonly IOptions<RoundTheCodeSync> _roundTheCodeSyncOptions;
		protected readonly IRoundTheCodeSync _roundTheCodeSync;

		public AppSettingsController(IConfiguration configuration, IOptions<RoundTheCodeSync> roundTheCodeSyncOptions, IRoundTheCodeSync roundTheCodeSync)
		{
			_configuration = configuration;
			_roundTheCodeSyncOptions = roundTheCodeSyncOptions;
			_roundTheCodeSync = roundTheCodeSync;
		}

		[Route("first-way")]
		public IActionResult FirstWay()
		{
			return Content(_configuration.GetSection("RoundTheCodeSync").GetChildren().FirstOrDefault(config => config.Key == "Title").Value + "\r\n" + _configuration.GetValue<string>("RoundTheCodeSync:Title"), "text/plain");
		}

		[Route("second-way")]
		public IActionResult SecondWay()
		{
			return Content(_roundTheCodeSyncOptions.Value.Title, "text/plain");
		}

		[Route("third-way")]
		public IActionResult ThirdWay()
		{
			return Content(_roundTheCodeSync.Title, "text/plain");
		}

		[Route("database")]
		public async Task<IActionResult> Database()
		{
			var name = string.Empty;

			using (var conn = new SqlConnection(_configuration.GetConnectionString("SyncDb")))
			{
				await conn.OpenAsync();

				using (var cmd = new SqlCommand("SELECT * FROM dbo.Sync WHERE ID = 1", conn))
				{
					using (var reader = await cmd.ExecuteReaderAsync())
					{
						if (await reader.ReadAsync())
						{
							name = reader["Name"].ToString();
						}
					}
				}
			}

			return Content(name, "text/plain");
		}
	}
	
}
