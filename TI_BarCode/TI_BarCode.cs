using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ZXing;
using ZXing.Common;
//using ZXing.QrCode;

namespace TI_BarCode
{
    public class TI_BarCode
    {
        [SqlFunction(IsDeterministic = true, DataAccess = DataAccessKind.None)]
        public static SqlBinary GenBarCode(string source, int barcodeFormat, int width, int height, bool pureBarCode)
        {
            EncodingOptions options = new EncodingOptions
            {
                PureBarcode = pureBarCode,
                Width = width,
                Height = height
            };
            BarcodeWriter write = new BarcodeWriter();
            write.Format = (BarcodeFormat)barcodeFormat;
            write.Options = options;
            Bitmap bitmap = write.Write(source);

            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Jpeg);
            byte[] arr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length);
            ms.Close();
            return arr;
        }
    }
}
