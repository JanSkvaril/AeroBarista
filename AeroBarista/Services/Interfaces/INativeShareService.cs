using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroBarista.Services.Interfaces
{
    public interface INativeShareService
    {
        public Task ShareFile(string title, FileResult file);
        public Task ShareURL(string uri);
    }
}
