using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroBarista.Services.Interfaces
{
    public interface INativeShareService
    {
        public void ShareFile(string title, FileResult file);
    }
}
