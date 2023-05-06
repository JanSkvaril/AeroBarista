using AeroBarista.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroBarista.Services
{
    [ExportSingleton]
    public class SettingsProvider
    {
        public readonly string APP_URL = "https://aeropress/recipe";
    }
}
