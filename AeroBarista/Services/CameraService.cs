using AeroBarista.Attributes;
using AeroBarista.Services.Interfaces;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroBarista.Services
{
    [ExportTransientAs(nameof(ICameraService))]
    public class CameraService : ICameraService
    {
        public async Task<FileResult?> ObtainPhoto()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
                return photo;
            }
            return null;
        }
    }
}
