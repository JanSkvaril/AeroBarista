using AeroBarista.Attributes;
using AeroBarista.Services.Interfaces;

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
