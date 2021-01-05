using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodChallange
{
    public partial class ReadCSV : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void ImportCSV1(object sender, EventArgs e)
        {
            try
            {
                labeldownload.Visible = false;
                btndownlaod.Visible = false;
                btndownlaodmerge.Visible = false;
                // ImportFileA();
                GridView1.DataSource = ImportFileA();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                var a = ex.ToString();
                Common c = new Common();
                c.writeLog("ImportCSV1: " + a);
            }

        }
        protected void ImportCSV2(object sender, EventArgs e)
        {
            try
            {
                labeldownload.Visible = false;
                btndownlaodmerge.Visible = false;
                btndownlaod.Visible = false;
                //ImportFileB();
                GridView1.DataSource = ImportFileB();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                var a = ex.ToString();
                Common c = new Common();
                c.writeLog("ImportCSV2: " + a);
            }

        }
        protected DataTable ImportFileA()
        {
            try
            {
                //Upload and save the file
                string csvPath_barcode = Server.MapPath("~/Files/barcodesA.csv");

                //Create a DataTable.
                DataTable dt_barcode = new DataTable();
                dt_barcode.Columns.AddRange(new DataColumn[3] { new DataColumn("SupplierID", typeof(string)),
            new DataColumn("SKU", typeof(string)),

            new DataColumn("Barcode",typeof(string)) });

                //Read the contents of CSV file.
                string csvData_barcode = File.ReadAllText(csvPath_barcode);

                //Execute a loop over the rows.
                foreach (string row in csvData_barcode.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        dt_barcode.Rows.Add();
                        int i = 0;

                        //Execute a loop over the columns.
                        foreach (string cell in row.Split(','))
                        {
                            dt_barcode.Rows[dt_barcode.Rows.Count - 1][i] = cell;
                            i++;
                        }
                    }
                }

                //Upload Supplier Table
                string csvPath_supplier = Server.MapPath("~/Files/suppliersA.csv");

                //Create a DataTable.
                DataTable dt_supplier = new DataTable();
                dt_supplier.Columns.AddRange(new DataColumn[2] { new DataColumn("ID", typeof(string)),
            new DataColumn("Name", typeof(string)) });

                //Read the contents of CSV file.
                string csvData_supplier = File.ReadAllText(csvPath_supplier);

                //Execute a loop over the rows.
                foreach (string row in csvData_supplier.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        dt_supplier.Rows.Add();
                        int i = 0;

                        //Execute a loop over the columns.
                        foreach (string cell in row.Split(','))
                        {
                            dt_supplier.Rows[dt_supplier.Rows.Count - 1][i] = cell;
                            i++;
                        }
                    }
                }

                //Upload Catalog Table
                string csvPath_catalog = Server.MapPath("~/Files/catalogA.csv");

                //Create a DataTable.
                DataTable dt_catalog = new DataTable();
                dt_catalog.Columns.AddRange(new DataColumn[2] { new DataColumn("SKU", typeof(string)),
            new DataColumn("Description", typeof(string)) });

                //Read the contents of CSV file.
                string csvData_catalog = File.ReadAllText(csvPath_catalog);

                //Execute a loop over the rows.
                foreach (string row in csvData_catalog.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        dt_catalog.Rows.Add();
                        int i = 0;

                        //Execute a loop over the columns.
                        foreach (string cell in row.Split(','))
                        {
                            dt_catalog.Rows[dt_catalog.Rows.Count - 1][i] = cell;
                            i++;
                        }
                    }
                }

                // Create merged data
                DataTable Table_MergeUserData = new DataTable("MergeUserData");

                DataColumn[] colsMerge ={
                      new DataColumn("SupplierID",typeof(String)),
                      new DataColumn("SKU",typeof(String)),
                      new DataColumn("Barcode",typeof(String)),
                      new DataColumn("Name",typeof(String)),
                      new DataColumn("Description",typeof(String)),

                  };
                Table_MergeUserData.Columns.AddRange(colsMerge);

                var mergedData = (from b in dt_barcode.AsEnumerable()
                                  join s in dt_supplier.AsEnumerable() on b.Field<string>("SupplierID") equals s.Field<string>("ID")
                                  join c in dt_catalog.AsEnumerable() on b.Field<string>("SKU") equals c.Field<string>("SKU")
                                  select new { barcode = b, supplier = s, catalog = c }).ToList();

                foreach (var data in mergedData)
                {
                    Table_MergeUserData.Rows.Add(new object[] {
                    data.barcode.Field<string>("SupplierID"),
                    data.barcode.Field<string>("SKU"),
                    data.barcode.Field<string>("Barcode"),
                    data.supplier.Field<string>("Name"),
                    data.catalog.Field<string>("Description")
                });

                }

                DataTable dt1 = new DataTable();
                DataColumn[] colsMerge_new ={
                      new DataColumn("No"),
                      new DataColumn("SupplierID",typeof(String)),
                      new DataColumn("SKU",typeof(String)),
                      new DataColumn("Barcode",typeof(String)),
                      new DataColumn("Name",typeof(String)),
                      new DataColumn("Description",typeof(String)),
                      new DataColumn("Source",typeof(string))
                  };
                dt1.Columns.AddRange(colsMerge_new);
                int n = 1;
                //Execute a loop over the rows.
                foreach (DataRow row in Table_MergeUserData.Rows)
                {
                    dt1.Rows.Add();
                    int i = 0;
                    dt1.Rows[dt1.Rows.Count - 1][0] = n;
                    foreach (DataColumn col in Table_MergeUserData.Columns)
                    {
                        dt1.Rows[dt1.Rows.Count - 1][i + 1] = row[col];
                        dt1.Rows[dt1.Rows.Count - 1][6] = "A";
                        i++;
                    }
                    n++;

                }
                //Bind the DataTable.
                //GridView1.DataSource = dt1;
                //GridView1.DataBind();
                return dt1;
            }
            catch (Exception ex)
            {
                var a = ex.ToString();
                Common c = new Common();
                c.writeLog("ImportCSV1: " + a);
                return null;
            }
        }

        protected DataTable ImportFileB()
        {
            try
            {
                //Upload and save the file
                string csvPath_barcode = Server.MapPath("~/Files/barcodesB.csv");

                //Create a DataTable.
                DataTable dt_barcode = new DataTable();
                dt_barcode.Columns.AddRange(new DataColumn[3] { new DataColumn("SupplierID", typeof(string)),
            new DataColumn("SKU", typeof(string)),

            new DataColumn("Barcode",typeof(string)) });

                //Read the contents of CSV file.
                string csvData_barcode = File.ReadAllText(csvPath_barcode);

                //Execute a loop over the rows.
                foreach (string row in csvData_barcode.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        dt_barcode.Rows.Add();
                        int i = 0;

                        //Execute a loop over the columns.
                        foreach (string cell in row.Split(','))
                        {
                            dt_barcode.Rows[dt_barcode.Rows.Count - 1][i] = cell;
                            i++;
                        }
                    }
                }

                //Upload Supplier Table
                string csvPath_supplier = Server.MapPath("~/Files/suppliersB.csv");

                //Create a DataTable.
                DataTable dt_supplier = new DataTable();
                dt_supplier.Columns.AddRange(new DataColumn[2] { new DataColumn("ID", typeof(string)),
            new DataColumn("Name", typeof(string)) });

                //Read the contents of CSV file.
                string csvData_supplier = File.ReadAllText(csvPath_supplier);

                //Execute a loop over the rows.
                foreach (string row in csvData_supplier.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        dt_supplier.Rows.Add();
                        int i = 0;

                        //Execute a loop over the columns.
                        foreach (string cell in row.Split(','))
                        {
                            dt_supplier.Rows[dt_supplier.Rows.Count - 1][i] = cell;
                            i++;
                        }
                    }
                }

                //Upload Catalog Table
                string csvPath_catalog = Server.MapPath("~/Files/catalogB.csv");

                //Create a DataTable.
                DataTable dt_catalog = new DataTable();
                dt_catalog.Columns.AddRange(new DataColumn[2] { new DataColumn("SKU", typeof(string)),
            new DataColumn("Description", typeof(string)) });

                //Read the contents of CSV file.
                string csvData_catalog = File.ReadAllText(csvPath_catalog);

                //Execute a loop over the rows.
                foreach (string row in csvData_catalog.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        dt_catalog.Rows.Add();
                        int i = 0;

                        //Execute a loop over the columns.
                        foreach (string cell in row.Split(','))
                        {
                            dt_catalog.Rows[dt_catalog.Rows.Count - 1][i] = cell;
                            i++;
                        }
                    }
                }

                // Create merged data
                DataTable Table_MergeUserData = new DataTable("MergeUserData");

                DataColumn[] colsMerge ={
                      new DataColumn("SupplierID",typeof(String)),
                      new DataColumn("SKU",typeof(String)),
                      new DataColumn("Barcode",typeof(String)),
                      new DataColumn("Name",typeof(String)),
                      new DataColumn("Description",typeof(String)),

                  };
                Table_MergeUserData.Columns.AddRange(colsMerge);

                var mergedData = (from b in dt_barcode.AsEnumerable()
                                  join s in dt_supplier.AsEnumerable() on b.Field<string>("SupplierID") equals s.Field<string>("ID")
                                  join c in dt_catalog.AsEnumerable() on b.Field<string>("SKU") equals c.Field<string>("SKU")
                                  select new { barcode = b, supplier = s, catalog = c }).ToList();

                foreach (var data in mergedData)
                {
                    Table_MergeUserData.Rows.Add(new object[] {
                    data.barcode.Field<string>("SupplierID"),
                    data.barcode.Field<string>("SKU"),
                    data.barcode.Field<string>("Barcode"),
                    data.supplier.Field<string>("Name"),
                    data.catalog.Field<string>("Description")
                });

                }

                DataTable dt1 = new DataTable();
                DataColumn[] colsMerge_new ={
                      new DataColumn("No"),
                      new DataColumn("SupplierID",typeof(String)),
                      new DataColumn("SKU",typeof(String)),
                      new DataColumn("Barcode",typeof(String)),
                      new DataColumn("Name",typeof(String)),
                      new DataColumn("Description",typeof(String)),
                      new DataColumn("Source",typeof(string))
                  };
                dt1.Columns.AddRange(colsMerge_new);

                int n = 1;
                foreach (DataRow row in Table_MergeUserData.Rows)
                {
                    dt1.Rows.Add();
                    int i = 0;
                    dt1.Rows[dt1.Rows.Count - 1][0] = n;
                    foreach (DataColumn col in Table_MergeUserData.Columns)
                    {
                        // dt1.Rows[n][0] = n;
                        dt1.Rows[dt1.Rows.Count - 1][i + 1] = row[col];
                        dt1.Rows[dt1.Rows.Count - 1][6] = "B";
                        i++;
                        //n++;
                    }
                    n++;


                }
                //Bind the DataTable.
                //GridView2.DataSource = dt1;
                //GridView2.DataBind();
                return dt1;
            }
            catch (Exception ex)
            {
                var a = ex.ToString();
                Common c = new Common();
                c.writeLog("ImportCSV2: " + a);
                return null;
            }
        }
        protected void MergeFiles(object sender, EventArgs e)
        {
            try
            {
                labeldownload.Visible = true;
                btndownlaodmerge.Visible = true;
                btndownlaod.Visible = false;
                DataTable dtmerge = new DataTable();
                dtmerge.Merge(ImportFileA());
                dtmerge.Merge(ImportFileB());

                GridView1.DataSource = dtmerge;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                var a = ex.ToString();
                Common c = new Common();
                c.writeLog("MergeFiles: " + a);

            }
        }

        protected void btndownlaod_Click(object sender, EventArgs e)
        {
            try
            {
                labeldownload.Visible = true;
                btndownlaodmerge.Visible = false;
                btndownlaod.Visible = true;
                //DataTable dt = new DataTable();
                //dt = (DataTable)GridView1.DataSource;
                // string downloadpath = Server.MapPath("");
                labeldownload.Visible = true;
                btndownlaod.Visible = true;
                btndownlaodmerge.Visible = false;
                DataTable dtfileA = ImportFileA();
                DataTable dtfileB = ImportFileB();
                DataTable dtmerge = new DataTable();
                dtmerge.Merge(ImportFileA());
                dtmerge.Merge(ImportFileB());

                DataTable Table_MergeUserData = new DataTable("MergeUserData");


                DataColumn[] colsMerge ={
                      new DataColumn("No",typeof(String)),
                      new DataColumn("SupplierID",typeof(String)),
                      new DataColumn("SKU",typeof(String)),
                      new DataColumn("Barcode",typeof(String)),
                      new DataColumn("Name",typeof(String)),
                      new DataColumn("Description",typeof(String)),
                      new DataColumn("Source",typeof(String))
                  };
                Table_MergeUserData.Columns.AddRange(colsMerge);

                var allDuplicates = dtmerge.AsEnumerable()
        .GroupBy(dr => dr.Field<string>("Barcode"))
        .Where(g => g.Count() > 1)
        .SelectMany(g => g)
        .ToList();

                //var mergedData = (from a in dtfileA.AsEnumerable()
                //                  join b in dtfileB.AsEnumerable() on a.Field<string>("Barcode") equals b.Field<string>("Barcode")
                //                  select new { filea = a, fileb = b }).ToList();

                foreach (var data in allDuplicates)
                {
                    Table_MergeUserData.Rows.Add(new object[] {
                    data.Field<string>("No"),
                    data.Field<string>("SupplierID"),
                    data.Field<string>("SKU"),
                    data.Field<string>("Barcode"),
                    data.Field<string>("Name"),
                    data.Field<string>("Description"),
                    data.Field<string>("Source")
                });

                }

                DataTable dt1 = new DataTable();
                DataColumn[] colsMerge_new ={
                      new DataColumn("No"),
                      new DataColumn("SupplierID",typeof(String)),
                      new DataColumn("SKU",typeof(String)),
                      new DataColumn("Barcode",typeof(String)),
                      new DataColumn("Name",typeof(String)),
                      new DataColumn("Description",typeof(String)),
                      new DataColumn("Source",typeof(string))
                  };
                dt1.Columns.AddRange(colsMerge_new);
                int n = 1;
                //Execute a loop over the rows.
                foreach (DataRow row in Table_MergeUserData.Rows)
                {
                    dt1.Rows.Add();
                    int i = 0;
                    //dt1.Rows[dt1.Rows.Count - 1][0] = n;
                    foreach (DataColumn col in Table_MergeUserData.Columns)
                    {
                        dt1.Rows[dt1.Rows.Count - 1][i] = row[col];
                        // dt1.Rows[dt1.Rows.Count - 1][6] = "A";
                        i++;
                    }
                    n++;

                }
                string downloadpath = Server.MapPath("~/Files/export.csv");

                ToCSV(dt1, downloadpath);
            }
            catch (Exception ex)
            {
                var a = ex.ToString();
                Common c = new Common();
                c.writeLog("Download: " + a);

            }

            //ToCSV()
            //DataTable dt = OperationsUtlity.createDataTable();
            //string filename = OpenSavefileDialog();
            //dt.ToCSV(filename);
        }
        protected void ToCSV(DataTable dtDataTable, string strFilePath)
        {
            try
            {
                //WebClient req = new WebClient();
                //HttpResponse response = HttpContext.Current.Response;
                string csv = string.Empty;

                foreach (DataColumn column in dtDataTable.Columns)
                {
                    //Add the Header row for CSV file.
                    csv += column.ColumnName + ',';
                }

                //Add new line.
                csv += "\r\n";

                foreach (DataRow row in dtDataTable.Rows)
                {
                    foreach (DataColumn column in dtDataTable.Columns)
                    {
                        //Add the Data rows.
                        csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                    }

                    //Add new line.
                    csv += "\r\n";
                }

                //Download the CSV file.
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=SqlExport.csv");
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.Output.Write(csv);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                var a = ex.ToString();
                Common c = new Common();
                c.writeLog("TOCSV: " + a);

            }

        }

        protected void btnresult_Click(object sender, EventArgs e)
        {
            try
            {
                labeldownload.Visible = true;
                btndownlaod.Visible = true;
                btndownlaodmerge.Visible = false;
                DataTable dtfileA = ImportFileA();
                DataTable dtfileB = ImportFileB();
                DataTable dtmerge = new DataTable();
                dtmerge.Merge(ImportFileA());
                dtmerge.Merge(ImportFileB());

                DataTable Table_MergeUserData = new DataTable("MergeUserData");


                DataColumn[] colsMerge ={
                      new DataColumn("No",typeof(String)),
                      new DataColumn("SupplierID",typeof(String)),
                      new DataColumn("SKU",typeof(String)),
                      new DataColumn("Barcode",typeof(String)),
                      new DataColumn("Name",typeof(String)),
                      new DataColumn("Description",typeof(String)),
                      new DataColumn("Source",typeof(String))
                  };
                Table_MergeUserData.Columns.AddRange(colsMerge);

                var allDuplicates = dtmerge.AsEnumerable()
        .GroupBy(dr => dr.Field<string>("Barcode"))
        .Where(g => g.Count() > 1)
        .SelectMany(g => g)
        .ToList();

                //var mergedData = (from a in dtfileA.AsEnumerable()
                //                  join b in dtfileB.AsEnumerable() on a.Field<string>("Barcode") equals b.Field<string>("Barcode")
                //                  select new { filea = a, fileb = b }).ToList();

                foreach (var data in allDuplicates)
                {
                    Table_MergeUserData.Rows.Add(new object[] {
                    data.Field<string>("No"),
                    data.Field<string>("SupplierID"),
                    data.Field<string>("SKU"),
                    data.Field<string>("Barcode"),
                    data.Field<string>("Name"),
                    data.Field<string>("Description"),
                    data.Field<string>("Source")
                });

                }

                DataTable dt1 = new DataTable();
                DataColumn[] colsMerge_new ={
                      new DataColumn("No"),
                      new DataColumn("SupplierID",typeof(String)),
                      new DataColumn("SKU",typeof(String)),
                      new DataColumn("Barcode",typeof(String)),
                      new DataColumn("Name",typeof(String)),
                      new DataColumn("Description",typeof(String)),
                      new DataColumn("Source",typeof(string))
                  };
                dt1.Columns.AddRange(colsMerge_new);
                int n = 1;
                //Execute a loop over the rows.
                foreach (DataRow row in Table_MergeUserData.Rows)
                {
                    dt1.Rows.Add();
                    int i = 0;
                    //dt1.Rows[dt1.Rows.Count - 1][0] = n;
                    foreach (DataColumn col in Table_MergeUserData.Columns)
                    {
                        dt1.Rows[dt1.Rows.Count - 1][i] = row[col];
                        // dt1.Rows[dt1.Rows.Count - 1][6] = "A";
                        i++;
                    }
                    n++;

                }
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                var a = ex.ToString();
                Common c = new Common();
                c.writeLog("downlaod: " + a);
            }
        }

        protected void btndownlaodmerge_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtmerge = new DataTable();
                dtmerge.Merge(ImportFileA());
                dtmerge.Merge(ImportFileB());

                DataTable da = new DataTable();
                DataColumn[] colsMerge ={
                      new DataColumn("SupplierID",typeof(String)),
                      new DataColumn("SKU",typeof(String)),
                      new DataColumn("Barcode",typeof(String)),
                      new DataColumn("Name",typeof(String)),
                      new DataColumn("Description",typeof(String)),
                      new DataColumn("Source",typeof(String)),

                  };
                da.Columns.AddRange(colsMerge);
                dtmerge.Columns.Remove("No");
                foreach (DataRow row in dtmerge.Rows)
                {
                    da.Rows.Add();
                    int i = 0;
                    // da.Rows[dt1.Rows.Count - 1][0] = n;
                    foreach (DataColumn col in dtmerge.Columns)
                    {
                        // dt1.Rows[n][0] = n;

                        da.Rows[da.Rows.Count - 1][i] = row[col];

                        i++;
                        //n++;
                    }
                }
                string downloadpath = Server.MapPath("~/Files/exportResult.csv");

                ToCSV(da, downloadpath);
            }
            catch (Exception ex)
            {
                var a = ex.ToString();
                Common c = new Common();
                c.writeLog("downlaodmerge: " + a);
            }
        }
    }
}