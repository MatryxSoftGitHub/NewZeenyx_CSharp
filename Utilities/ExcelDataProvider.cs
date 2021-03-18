using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ExcelDataReader;//Use this extension to work with this excel reader code


namespace NewZeenyx_CSharp.Utilities
{
    class ExcelDataProvider
    {
        public static DataTable ExcelToDataTable(string fileName)
        {
            //open file and returns as Stream
            FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);

            //Createopenxmlreader via ExcelReaderFactory
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream); //.xlsx
                                                                                           //Set the First Row as Column Name
                                                                                           //excelReader.IsFirstRowAsColumnNames = true;It will not work anymore


            //Return as DataSet
            DataSet resultSet = excelReader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });


            //Get all the Tables
            DataTableCollection table = resultSet.Tables;
            //Store it in DataTable
            DataTable resultTable = table["Sheet1"];

            //return
            return resultTable;
        }

        public static List<DataCollection> dataCol = new List<DataCollection>();

        public static void PopulateInCollection(string fileName)

        {
            DataTable table = ExcelToDataTable(fileName);

            for (int row = 1; row <= table.Rows.Count; row++)

            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    DataCollection dtTable = new DataCollection()
                    {
                        rowNumber = row,
                        colName = table.Columns[col].ColumnName,
                        colVal = table.Rows[row - 1][col].ToString()

                    };

                    //Add all the  details  for each row
                    dataCol.Add(dtTable);

                }
            }
        }

        public static string ReadData(int rowNumber, string columnName)
        {
            try
            {
                //Retriving  Data using  LINQ to reduce much  of interactions
                string data = (from colData in dataCol
                               where colData.colName == columnName && colData.rowNumber == rowNumber
                               select colData.colVal).SingleOrDefault();
                return data.ToString();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;

            }
        }

        public class DataCollection

        {
            public int rowNumber { get; set; }
            public string colName { get; set; }
            public string colVal { get; set; }
        }
    }
}
