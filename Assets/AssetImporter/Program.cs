using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using AssetRegister;


namespace AssetImporter
{
	class LineData
	{
		public LineData()
		{
			//ColName = v;
			//Value = dynamic;
		}

		public string ColName { get; set; }
		public dynamic Value { get; set; }
	}
	class Program
	{
		static void Main(string[] args)
		{
			List<Tuple<string, int>> files = new List<Tuple<string,int>>();
			//files.Add(new Tuple<string, int>(@"C:\OraDocDrive\Oracle Content\Cabinet\HelperFiles\Handover2\Customer examples\Asset lists examples\AU1\Aconex Handover-RMH O&M-2017-07-06-00-27-01-551\RHM Asset List.xlsx", 2));
			//files.Add(new Tuple<string, int>(@"C:\OraDocDrive\Oracle Content\Cabinet\HelperFiles\Handover2\Customer examples\Asset lists examples\AU1\Aconex Handover-100 Waymouth Street-2017-07-06-00-23-36-226\Asset List - 100 Waymouth Signarama.xlsx", 1));
			//files.Add(new Tuple<string, int>(@"C:\OraDocDrive\Oracle Content\Cabinet\HelperFiles\Handover2\Customer examples\Asset lists examples\AU1\Aconex Handover-100 Waymouth Street-2017-07-06-00-23-36-226\Asset List 100 Waymouth Street Adelaide.xlsx", 1));
			//files.Add(new Tuple<string, int>(@"C:\OraDocDrive\Oracle Content\Cabinet\HelperFiles\Handover2\Customer examples\Asset lists examples\AU1\Aconex Handover-100 Waymouth Street-2017-07-06-00-23-36-226\Asset List -Tiles.xls", 1));
			//files.Add(new Tuple<string, int>(@"C:\OraDocDrive\Oracle Content\Cabinet\HelperFiles\Handover2\Customer examples\Asset lists examples\AU1\Aconex Handover-100 Waymouth Street-2017-07-06-00-23-36-226\Asset List -Titan Interiors.xls", 1));
			//files.Add(new Tuple<string, int>(@"C:\OraDocDrive\Oracle Content\Cabinet\HelperFiles\Handover2\Customer examples\Asset lists examples\AU1\Aconex Handover-100 Waymouth Street-2017-07-06-00-23-36-226\Asset List- Waymouth St. from Estilo.xls",	1));
			//files.Add(new Tuple<string, int>(@"C:\OraDocDrive\Oracle Content\Cabinet\HelperFiles\Handover2\Customer examples\Asset lists examples\AU1\Aconex Handover-100 Waymouth Street-2017-07-06-00-23-36-226\Asset List-HN.xls",	1));
			//files.Add(new Tuple<string, int>(@"C:\OraDocDrive\Oracle Content\Cabinet\HelperFiles\Handover2\Customer examples\Asset lists examples\AU1\Aconex Handover-100 Waymouth Street-2017-07-06-00-23-36-226\Contract Carpets - Asset Register.xlsx",2));
			//files.Add(new Tuple<string, int>(@"C:\OraDocDrive\Oracle Content\Cabinet\HelperFiles\Handover2\Customer examples\Asset lists examples\AU1\Aconex Handover-100 Waymouth Street-2017-07-06-00-23-36-226\Copy of Asset List 2-METRO JOINERY (2).xls",2));
			//files.Add(new Tuple<string, int>(@"C:\OraDocDrive\Oracle Content\Cabinet\HelperFiles\Handover2\Customer examples\Asset lists examples\AU1\Aconex Handover-100 Waymouth Street-2017-07-06-00-23-36-226\Koreana Tiles - Asset List.xls", 1));
			//files.Add(new Tuple<string, int>(@"C:\OraDocDrive\Oracle Content\Cabinet\HelperFiles\Handover2\Customer examples\Asset lists examples\AU1\Aconex Handover-100 Waymouth Street-2017-07-06-00-23-36-226\Otis Lifts - Asset List - GB.xls", 2));
			//files.Add(new Tuple<string, int>(@"C:\OraDocDrive\Oracle Content\Cabinet\HelperFiles\Handover2\Customer examples\Asset lists examples\AU1\Aconex Handover-100 Waymouth Street-2017-07-06-00-23-36-226\RIS Roof Safe - Asset List.xls", 1));
			//	files.Add(new Tuple<string, int>(@"C:\OraDocDrive\Oracle Content\Cabinet\HelperFiles\Handover2\Customer examples\Asset lists examples\AU1\Aconex Handover-100 Waymouth Street-2017-07-06-00-23-36-226\SA Commercial Blinds_Asset List_100 Waymouth_Nov 2012.xls", 1));
			files.Add(new Tuple<string, int>(@"C:\OraDocDrive\Oracle Content\Cabinet\HelperFiles\Handover2\Customer examples\Asset lists examples\AU1\Aconex Handover-100 Waymouth Street-2017-07-06-00-23-36-226\Tyrone Electrical - Asset List.xls", 1));


			DataTable dt = null;

			foreach (var file in files)
			{
				Console.WriteLine("Loading file " + file.Item1);
				string filetype = file.Item1.Substring(file.Item1.LastIndexOf(".", StringComparison.CurrentCulture) + 1);

				switch (filetype)
				{
					case "xls":
					case "xlsx":
					{
						dt = ImportFromExcel(file.Item1, file.Item2);
						break;
					}
				}


				if (dt != null)
				{
					AssetCollection ar = CreateAssetRegister(dt);


					var xmlSerializer = new XmlSerializer(ar.GetType());
					var stringBuilder = new StringBuilder();
					var xmlTextWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings {NewLineChars = "\r\n", Indent = true});
					xmlSerializer.Serialize(xmlTextWriter, ar);
					var finalXml = stringBuilder.ToString();
					Console.WriteLine("Creating xml");
					File.WriteAllText(file.Item1 + ".xml", finalXml);
					Console.WriteLine("Xml Created");
					dt = null;
				}
			}
		}

		private static AssetCollection CreateAssetRegister(DataTable dt)
		{
			return new AssetCollection(dt);
		}

		private static DataTable ImportFromExcel(string filepath, int headerRow)
		{

			Excel.Application xlApp = new Excel.Application();
			Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filepath);
			Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
			Excel.Range xlRange = xlWorksheet.UsedRange;



			int rowCount = xlRange.Rows.Count;
			int colCount = xlRange.Columns.Count;

			DataTable dt = new DataTable();

			for (int i = 1; i <= colCount; i++)
			{
				var cellContents = xlRange.Cells[headerRow, i].Value2.ToString();
				dt.Columns.Add(cellContents);
			}
			DataRow row;
			for (int j = headerRow + 1; j <= rowCount; j++)
			{
				row = dt.NewRow();

				for (int ii = 1; ii <= colCount; ii++)
				{
					var cellContents = xlRange.Cells[j, ii].Value2;
					row[ii - 1] = cellContents;
				}

				if (row.ItemArray.Count() != dt.Columns.Count)
				{
					throw new Exception("Not enough rows");
				}

				if (j % 10 == 0)
				{
					Console.WriteLine("Loading " + j + " of " + rowCount);
				}

				dt.Rows.Add(row);

			}

			Console.WriteLine("Loaded " + rowCount + " rows.");




			//cleanup
			GC.Collect();
			GC.WaitForPendingFinalizers();

			//rule of thumb for releasing com objects:
			//  never use two dots, all COM objects must be referenced and released individually
			//  ex: [somthing].[something].[something] is bad

			//release com objects to fully kill excel process from running in the background
			Marshal.ReleaseComObject(xlRange);
			Marshal.ReleaseComObject(xlWorksheet);

			//close and release
			xlWorkbook.Close();
			Marshal.ReleaseComObject(xlWorkbook);

			//quit and release
			xlApp.Quit();

			return dt;
		}

	
	}
}
