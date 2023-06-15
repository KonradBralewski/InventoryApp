using InventoryAppAPI.DAL.Procedures;
using InventoryAppAPI.Models.Procedures;
using IronPdf;

namespace InventoryAppAPI.BLL.Services.ReportGeneration
{
    public class ReportGenerator : IReportGenerator
    {
        private string _initialHtmlCode = @"<!DOCTYPE html>
<html>
  <head>
    <meta charset=""UTF-8"" />
    <title>Raport rozbieżności inwentaryzacji</title>

    <style>
      body {
        font-family: Arial, sans-serif;
        padding: 20px;
      }

      h1 {
        text-align: center;
      }

      h2 {
        margin-top: 30px;
      }

      table {
        width: 100%;
        border-collapse: collapse;
        margin-top: 10px;
      }

      th,
      td {
        padding: 8px;
        border: 1px solid #ddd;
      }

      th {
        background-color: #f2f2f2;
        font-weight: bold;
      }

      ul {
        margin: 0;
        padding: 0;
        list-style-type: none;
      }

      li {
        margin-bottom: 20px;
      }

      .header {
        display: flex;
        justify-content: space-between;
        margin-bottom: 20px;
      }

      .header-left {
        font-size: 14px;
      }

      .header-right {
        font-size: 14px;
        text-align: right;
      }

      .building-name {
        font-weight: bold;
      }

      .room-name {
        font-weight: bold;
        margin-top: 10px;
      }

      .item-name {
        font-weight: bold;
      }

      .item-description {
        font-style: italic;
      }

      .item-notes {
        color: #888;
        margin-top: 5px;
      }
      .item-notes-warning{
        color: #FF0000;
        margin-top: 5px;
    }
    </style>
  </head>
  <body>
    <div class=""header"">
      <div class=""header-left"">
        Raport wygenerowany przez aplikację InventoryApp
      </div>
      <div class=""header-right"">
        Data raportu: <span><b>{reportDate}</b></span>
      </div>
    </div>

    <h1>Raport inwentaryzacyjny nr. <b>{reportNumber}</b></h1>
    <h2>Miejsce procesu:</h2>
    <ul>
      <!-- Blok budynku -->
      <li>
        <p style=""margin-left: 50px;"" class=""building-name"">Nazwa budynku: <span style=""color:blue;""><b>{buildingName}</b></span></p>
        <ul>
          <!-- Blok pomieszczenia -->
          <li>
            <p style=""margin-left: 50px;"" class=""room-name"">
              Nazwa pomieszczenia: <span style=""color:blue;""><b>{roomName}</b></span>
            </p>
            <h3>Lista przedmiotów:</h3>
            <table>
              <thead>
                <tr>
                  <th>Nazwa przedmiotu</th>
                  <th>Opis</th>
                  <th>Uwagi</th>
                </tr>
              </thead>
              <tbody id=""table-body"">
              </tbody>
            </table>
          </li>
          <!-- Koniec bloku pomieszczenia -->
        </ul>
      </li>
      <!-- Koniec bloku budynku -->
    </ul>
  </body>
  {scriptPlace}
</html>
";
        private void FillReportDetails(ReportDetails reportDetails)
        {
            _initialHtmlCode = _initialHtmlCode.Replace("{reportNumber}", reportDetails.ReportNumber.ToString());
            _initialHtmlCode = _initialHtmlCode.Replace("{reportDate}", reportDetails.InventoryEndedAt.ToString());
            _initialHtmlCode = _initialHtmlCode.Replace("{buildingName}", reportDetails.BuildingName);
            _initialHtmlCode = _initialHtmlCode.Replace("{roomName}", reportDetails.RoomDescription);
        }

        private void FillItems(IEnumerable<GenerateReportProcedure> rawReports)
        {
            string buildedScript = @"<script>
const tableBody = document.getElementById(""table-body"")
";

            int id = 1;
            string note = "";
            string noteClass = "";

            foreach (GenerateReportProcedure report in rawReports)
            {
                note = report.IsScannedBool ? "Brak uwag" : "Przedmiot nie został zeskanowany";
                noteClass = report.IsScannedBool ? "item-notes" : "item-notes-warning";

                buildedScript += $@"const tr{id} = document.createElement(""tr"")
    var nameTd{id} = document.createElement(""td"")
    var descTd{id} = document.createElement(""td"")
    var notesTd{id} = document.createElement(""td"")
    
    nameTd{id}.classList.add(""item-name"")
    descTd{id}.classList.add(""item-description"")
    notesTd{id}.classList.add(""{noteClass}"")

    nameTd{id}.innerHTML = ""{report.ProductName}""
    descTd{id}.innerHTML = ""{report.Code}""
    notesTd{id}.innerHTML = ""{note}""

    tableBody.appendChild(tr{id})
    tr{id}.appendChild(nameTd{id})
    tr{id}.append(descTd{id})
    tr{id}.appendChild(notesTd{id}){'\n'}";
                id += 1;
            }

            buildedScript += "</script>";

            _initialHtmlCode = _initialHtmlCode.Replace("{scriptPlace}", buildedScript);
        }
        public async Task<PdfDocument> GenerateReportPDF(ReportDetails reportDetails, IEnumerable<GenerateReportProcedure> rawReports)
        {
            FillReportDetails(reportDetails);
            FillItems(rawReports);
            ChromePdfRenderOptions renderOptions = new ChromePdfRenderOptions();

            ChromePdfRenderer renderer = new ChromePdfRenderer { RenderingOptions = renderOptions };
            PdfDocument pdf = await renderer.RenderHtmlAsPdfAsync(_initialHtmlCode);
            return pdf;
        }
    }
}
