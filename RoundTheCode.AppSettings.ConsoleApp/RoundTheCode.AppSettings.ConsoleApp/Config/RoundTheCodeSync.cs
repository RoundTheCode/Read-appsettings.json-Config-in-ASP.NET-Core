using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTheCode.AppSettings.ConsoleApp.Config
{
	public class RoundTheCodeSync : IRoundTheCodeSync
	{
		[Required]
		public string Title { get; set; }

		public TimeSpan Interval { get; set; }

		[Range(5, 15)]
		public int ConcurrentThreads { get; set; }
	}
}
