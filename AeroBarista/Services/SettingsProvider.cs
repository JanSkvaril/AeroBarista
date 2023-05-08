using AeroBarista.Attributes;
using AeroBarista.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroBarista.Services
{
    [ExportSingletonAs(nameof(ISettingsProvider))]
    public class SettingsProvider : ISettingsProvider
    {
        public readonly string APP_URL = "https://aeropress/recipe";
        public string GetAppUrl()
        {
            return APP_URL;
        }
    }
}
