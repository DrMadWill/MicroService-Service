using System.Data;
using ExcelDataReader;

namespace Application.Helpers;

public static class FileHelper
{
    /// <summary>
    /// Folder is not exist then create folder
    /// </summary>
    /// <param name="folder"></param>
    public static void CreateFolderIfNotExists(this string folder)
    {
        if (string.IsNullOrEmpty(folder)) return;
        bool folderExists = Directory.Exists(folder);
        if (!folderExists)
        {
            Directory.CreateDirectory(folder);
        }
    }
    
    /// <summary>
    /// Read All Data From Excel
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="sheetIndex"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>

    public static List<object> GetAllRowsFromExcelFile(this string filePath, int sheetIndex)
    {
        var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader;

        string extension = Path.GetExtension(filePath);
        if (extension == ".xls")
        {
            excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
        }
        else if (extension == ".xlsx")
        {
            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        }
        else
        {
            throw new NotSupportedException("Wrong file extension");
        }

        var conf = new ExcelDataSetConfiguration
        {
            ConfigureDataTable = _ => new ExcelDataTableConfiguration
            {
                UseHeaderRow = true
            }
        };
        DataSet dataSet = excelReader.AsDataSet(conf);
        DataRowCollection? rowList = null;
        if (sheetIndex > 0)
        {
            rowList = dataSet.Tables[0].Rows;
            for (int i = 1; i < sheetIndex; i++)
            {
                if (rowList is null)
                    rowList = dataSet.Tables[i].Rows;
                else
                    rowList.Add(dataSet.Tables[i].Rows);
            }
        }
        else
            rowList = dataSet.Tables[sheetIndex].Rows;

        List<object> allRowsList = new List<object>();
        foreach (DataRow item in rowList)
        {
            allRowsList.Add(item.ItemArray.ToList()); //adding the above list of each row to another list
        }

        stream.Close();
        return allRowsList;
    }
}