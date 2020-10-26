using GrapeCity.Documents.Excel;
using System.IO;

namespace DioDocsBlazorWasmApp1.Data
{
    public class DDExcelService
    {
        public void Create(string platformname, string key, string connectionstring)
        {
            // ライセンスキー設定
            //Workbook.SetLicenseKey(key);

            // ワークブックの作成
            Workbook workbook = new Workbook();

            // ワークシートの取得
            IWorksheet worksheet = workbook.Worksheets[0];

            // セル範囲を指定して文字列を設定
            worksheet.Range["B2"].Value = "Hello DioDocs!";
            worksheet.Range["B3"].Value = "from " + platformname;

            // メモリストリームに保存
            MemoryStream ms = new MemoryStream();
            workbook.Save(ms, SaveFileFormat.Xlsx);
            ms.Seek(0, SeekOrigin.Begin);

            // BLOBストレージにアップロード
            AzStorage storage = new AzStorage(connectionstring);
            storage.UploadExcelAsync(ms);
        }
    }
}
