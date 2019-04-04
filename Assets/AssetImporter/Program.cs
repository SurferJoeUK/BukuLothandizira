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
			List<Tuple<string, int>> files = new List<Tuple<string, int>>();
			files.Add(new Tuple<string, int>(
				@"C:\OraDocDrive\Oracle Content\Cabinet\HelperFiles\Handover2\Customer examples\Asset lists examples\AU1\Aconex Handover-RMH O&M-2017-07-06-00-27-01-551\RHM Asset List.xlsx",
				2));


			DataTable dt = null;

			foreach (var file in files)
			{
				string filetype = file.Item1.Substring(file.Item1.LastIndexOf(".", StringComparison.CurrentCulture) + 1);

				switch (filetype)
				{
					case "xlsx":
					{
						dt = ImportFromExcel(file.Item1, file.Item2);
						break;
					}
				}

			}

			if (dt != null)
			{
				object ar = CreateAssetRegister(dt);
			}
		}

		private static object CreateAssetRegister(DataTable dt)
		{

			AssetCollection ac = new AssetCollection(dt);



			return ac;
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
