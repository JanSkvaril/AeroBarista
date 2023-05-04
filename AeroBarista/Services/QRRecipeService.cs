using AeroBarista.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static QRCoder.PayloadGenerator;

namespace AeroBarista.Services
{
    class QRRecipeService
    {


        public byte[] CreateQRCodeForRecipe(RecipeModel recipe, string url)
        {

            Url generator = new Url(url+ "/" + recipe.Id);
            string payload = generator.ToString();

         

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.L);
            PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);

          
            return qRCode.GetGraphic(20);

        }


    }
}
