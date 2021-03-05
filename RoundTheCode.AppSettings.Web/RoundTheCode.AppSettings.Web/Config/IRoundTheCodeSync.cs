using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTheCode.AppSettings.Web.Config
{
	public interface IRoundTheCodeSync
	{
		string Title { get; set; }

		TimeSpan Interval { get; set; }

		int ConcurrentThreads { get; set; }
	}
}
