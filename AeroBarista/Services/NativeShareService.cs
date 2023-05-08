using AeroBarista.Attributes;
using AeroBarista.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AeroBarista.Services
{

    [ExportTransientAs(nameof(INativeShareService))]
    public class NativeShareService : INativeShareService
    {
        public async Task ShareFile(string title, FileResult file)
        {
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = title,
                File = new ShareFile(file.FullPath)
            });
        }

        public async Task ShareURL(string uri)
        {
            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = "Share your recipe"
            });
        }
    }
}
