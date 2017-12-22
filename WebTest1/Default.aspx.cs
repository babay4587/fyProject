using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.POIFS;
using NPOI.Util;
using System.IO;
using System.Data;
using System.Text;
using WebTest1.SqlFunc;

namespace WebTest1
{
    public partial class _Default : Page
    {
        SQLFunc sqlHandle = new WebTest1.SqlFunc.SQLFunc();
        private string fileName = @"d:\工作表.xlsx"; //文件名
        private IWorkbook workbook = null;
        private FileStream fs = null;
        private bool disposed;
        


        protected void Page_Load(object sender, EventArgs e)
        {
           

        }

        protected void btn_test_Click(object sender, EventArgs e)
        {
            grid1.DataSource = ExcelToDataTable(fileName, true,true);
            grid1.DataBind();
        }

       

        public DataTable ExcelToDataTable(string sheetname,bool isFirstRowColumn,bool HaveHeader)
        {
            ISheet sheet = null;
            DataTable dt = new DataTable();
            int startRow = 0;

            try
            {

                //fs = System.IO.File.OpenRead(sheetname);
                fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                               

                if (fileName.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(fs);
                    sheet = workbook.GetSheet("Sheet1");
                                        
                }
                else if (fileName.IndexOf(".xls") > 0)
                    workbook = new HSSFWorkbook(fs);
                                
                if (sheetname != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    
                    IRow HeaderRow = sheet.GetRow(0);
                    int cellInt = HeaderRow.LastCellNum;

                    if (isFirstRowColumn)
                    {
                        for (int i = HeaderRow.FirstCellNum; i < cellInt; i++)
                        {
                            
                            DataColumn column = new DataColumn(HeaderRow.GetCell(i).StringCellValue);
                            dt.Columns.Add(column);
                        }
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }


                    int rowCount = sheet.LastRowNum;
                    int RowStart = (HaveHeader == true) ? sheet.FirstRowNum + 1 : sheet.FirstRowNum;
                    for (int i = RowStart; i < rowCount+1; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        DataRow datarow = dt.NewRow();

                        if (row == null) continue;
                                                
                        
                        for (int j = row.FirstCellNum; j < cellInt; j++)
                        {
                            if (row.GetCell(j) != null)
                            {
                                datarow[j] = row.GetCell(j).ToString();
                            }
                                                       
                        }

                        dt.Rows.Add(datarow);
                    }
                }
                return dt;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        protected void btn_Sql_Click(object sender, EventArgs e)
        {
            DataTable tb = new DataTable();
            tb = sqlHandle.getDisMsg("2");

            grid1.DataSource = tb;
            grid1.DataBind();
        }
    }
}