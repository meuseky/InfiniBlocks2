using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace InfiniBlocks2
{
	public class Config
	{
		public Config ()
		{
		}

		private static string GetSetting(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}

		private static void SetSetting(string key, string value)
		{
			Configuration configuration =
				ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			configuration.AppSettings.Settings[key].Value = value;
			configuration.Save(ConfigurationSaveMode.Full, true);
			ConfigurationManager.RefreshSection("appSettings");
		}
	}
}

