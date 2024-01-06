using CaseStudyPW.Test_Data_Classes;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyPW.Utilities
{
    public class DataRead
        {
            public static List<ATData> ReadData(string excelFilePath, string sheetName)
            {
                List<ATData> DataList = new List<ATData>();
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                using (var stream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true,
                            }
                        });

                        var dataTable = result.Tables[sheetName];

                        if (dataTable != null)
                        {
                            foreach (DataRow row in dataTable.Rows)
                            {
                                ATData DataRead = new ATData
                                {
                                    SearchText = GetValueOrDefault(row, "searchText"),
                                };
                                DataList.Add(DataRead);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Sheet '{sheetName}' not found in the Excel file.");
                        }
                    }
                }
                return DataList;
            }

            static string? GetValueOrDefault(DataRow row, string columnName)
            {
                Console.WriteLine(row + "  " + columnName);
                return row.Table.Columns.Contains(columnName) ? row[columnName]?.ToString() : null;
            }
        }
    }