using AeroBarista.Attributes;
using AeroBarista.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroBarista.Services
{

    [ExportTransientAs(nameof(INativeShareService))]
    public class NativeShareService : INativeShareService
    {
        public async void ShareFile(string title, FileResult file)
        {
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = title,
                File = new ShareFile(file.FullPath)
            });
        }
    }
}
