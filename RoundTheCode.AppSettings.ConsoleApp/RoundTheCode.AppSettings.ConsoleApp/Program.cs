using Microsoft.Extensions.Configuration;
using RoundTheCode.AppSettings.ConsoleApp.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RoundTheCode.AppSettings.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
			var config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json").Build();

			var roundTheCodeSync = config.GetSection("RoundTheCodeSync").Get<RoundTheCodeSync>();

			if (!Validator.TryValidateObject(roundTheCodeSync, new ValidationContext(roundTheCodeSync), new List<ValidationResult>(), true))
			{
				throw new Exception("Unable to find all settings");
			}

			Console.WriteLine(roundTheCodeSync.Title);
			Console.WriteLine(roundTheCodeSync.Interval);
			Console.WriteLine(roundTheCodeSync.ConcurrentThreads);
		}
    }
}
