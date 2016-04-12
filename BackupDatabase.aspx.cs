using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using Blumen;
using System.Text;
using System.IO;
using Ionic.Zip;

public partial class BackupDatabase : System.Web.UI.Page
{
    string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            this.hidMenuID.Value = "183";
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
        if (!IsPostBack)
        {
            mblnUserPermissionMaster();
            this.dbPath.Text = ConfigurationManager.AppSettings["DataBaseFilePath"];
            this.dbPath.Text = "App_Data";
        }
    }
    protected void btnbackUP_Click(object sender, EventArgs e)
    {
        string strAppVersion = "";
        string strSaveFileAs = null;
        clsCommon objComAddministrative = new clsCommon();
        StringBuilder sbGenerateFileName = new StringBuilder();
        
        strAppVersion = Resources.Resource.AppVersion.ToString();

        
        
        string app_data_folder;
        app_data_folder = HttpContext.Current.Server.MapPath(null);
        app_data_folder += "\\App_Data\\";
        ExecuteSQL onjExecuteSQL = new ExecuteSQL();
        DataTable dt = null;
        dt = new DataTable();

        string strSQL = "";
        strSQL = @"SELECT DB_NAME() AS DataBaseName";
        dt = onjExecuteSQL.SearchReportRecordFromSQLQuery(strSQL);

        string db = dt.Rows[0]["DataBaseName"].ToString();

        string date = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        //string date = DateTime.Now.ToString("yyyyMMdd");
        string backup_file = app_data_folder + "db_BlumenSoft_" + strAppVersion + "_" + Session["CompanyName"].ToString().Replace(" ","_") + "_" + date + ".bak";
        //string backup_file = app_data_folder + "db_bs_" + date + ".bak";

        strSaveFileAs = "db_BlumenSoft_" + strAppVersion + "_" + Session["CompanyName"].ToString().Replace(" ", "_") + "_" + date + ".bak";
        //strSaveFileAs = "db_bs_" + date + ".bak";
        //string pathToSave = ConfigurationManager.AppSettings["DataBaseFilePath"];
        //string dataBaseName = ConfigurationManager.AppSettings["DataBaseName"];
        //string dtToday = DateTime.Now.ToShortDateString();
        //string backUpDescription = "Creating Backup for DataBase on " + DateTime.Now.ToShortDateString();

        //sbGenerateFileName.Append(ConfigurationManager.AppSettings["DataBaseNameAlias"].ToString());
        //sbGenerateFileName.Append("_");

        //sbGenerateFileName.Append(strAppVersion);
        //sbGenerateFileName.Append("_");
        //sbGenerateFileName.Append(Session["CompanyName"].ToString());
        //sbGenerateFileName.Append("_");
        //sbGenerateFileName.Append(dtToday);
        //sbGenerateFileName.Append("_");
        //sbGenerateFileName.Append(DateTime.Now.ToShortTimeString().Replace(":", "-").Replace(".","")); // = File system does not except : character
        //sbGenerateFileName.Append(".bak");

        //pathToSave += sbGenerateFileName.ToString().Replace(" ", "_");
        //pathToSave = pathToSave.Replace("/", "-");
        try
        {

            //strSaveFileAs = pathToSave + sbGenerateFileName.ToString();
            //object objRec = objComAddministrative.CreateDatabaseBackup(dataBaseName, pathToSave, backUpDescription);
            //this.lblMsg.Visible = false;

            string sql = "backup database " + db + " to disk = '" + backup_file + "'";
            onjExecuteSQL.ExecuteQuery(sql);

            string myScript = "showInfo('Database backup taken successfully');";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
            myScript = "";



            //string fName = backup_file;
            //FileInfo fi = new FileInfo(backup_file);
            //long sz = fi.Length;

            //Response.ClearContent();
            //Response.ContentType = MimeType(Path.GetExtension(fName));
            //Response.AddHeader("Content-Disposition", string.Format("attachment; filename = {0}", System.IO.Path.GetFileName(fName)));
            //Response.AddHeader("Content-Length", sz.ToString("F0"));
            //Response.TransmitFile(fName);
            //Response.End();

            
            Response.Clear();
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "filename=" + strSaveFileAs + ".zip");

            using (ZipFile zip = new ZipFile())
            {
                zip.AddEntry(strSaveFileAs, File.ReadAllBytes(Server.MapPath(".") + @"\App_data\" + strSaveFileAs + ""));
                zip.Save(Response.OutputStream);
            }
            //mDownloadFile(backup_file, strSaveFileAs);
        }
        catch (SqlException exp)
        {
            //Response.Write(exp.Message);
            this.lblMsg.Visible = true;
            this.lblMsg.Text = exp.Message;
        }
        catch (Exception exp)
        {
            //Response.Write(exp.Message);
            this.lblMsg.Visible = true;
            this.lblMsg.Text = exp.Message;
        }
        finally
        {
            //objComAddministrative.Dispose();
        }
    }
    public static string MimeType(string Extension)
    {
        string mime = "application/octetstream";
        if (string.IsNullOrEmpty(Extension))
            return mime;
        string ext = Extension.ToLower();
        Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
        if (rk != null && rk.GetValue("Content Type") != null)
            mime = rk.GetValue("Content Type").ToString();
        return mime;
    } 
    private void mblnUserPermissionMaster()
    {
        SqlConnection connectionUA = new SqlConnection(cnnString);
        SelectUserPermissionMaster objDMLUA = new SelectUserPermissionMaster(connectionUA);
        objDMLUA.companyID = Session["CompanyID"].ToString();
        objDMLUA.strMenuID = this.hidMenuID.Value.ToString();
        objDMLUA.UserID = Session["UserID"].ToString();

        objDMLUA.Connection.Open();
        objDMLUA.Transaction = objDMLUA.Connection.BeginTransaction("myTran");
        DataTable dt = objDMLUA.ExecuteDataSet().Tables[0];


        if ((objDMLUA.ReturnValue == 0) && (dt.Rows.Count > 0))
        {
            objDMLUA.Transaction.Commit();
            string strpermission;
            strpermission = dt.Rows[0]["val"].ToString();
            if (strpermission == "0")
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        else
        {
            objDMLUA.Transaction.Rollback();
        }
    }
    //private void mDownloadFile(string backup_file, string strSaveFileAs)
    //{
       
    //        // Create the ZIP file that will be downloaded. Need to name the file something unique ...
    //        string strNow = String.Format("{0:MMM-dd-yyyy_hh-mm-ss}", System.DateTime.Now);
    //        ICSharpCode.SharpZipLib.Zip.ZipOutputStream zipOS = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(File.Create(Server.MapPath("~/TempFile/") + strSaveFileAs + ".zip"));
    //        zipOS.SetLevel(5); // ranges 0 to 9 ... 0 = no compression : 9 = max compression

    //        // Loop through the dataset to fill the zip file
           
    //            //byte[] files = (byte[])("\\App_Data\\" + strSaveFileAs);



    //            FileStream fs = new FileStream(backup_file, FileMode.Open,FileAccess.Read);
 
    //       // Create a byte array of file stream length
    //       byte[] files = new byte[fs.Length];
 
    //       //Read block of bytes from stream into the byte array
    //       fs.Read(files,0,System.Convert.ToInt32(fs.Length));
 
    //       //Close the File Stream
    //       fs.Close();
           


    //            //FileStream strim = new FileStream(Server.MapPath("~/TempFile/" + dr["FileName"]), FileMode.Create);
    //            //strim.Write(files, 0, files.Length);
    //            //strim.Close();
    //            //strim.Dispose();
    //       ICSharpCode.SharpZipLib.Zip.ZipEntry zipEntry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(backup_file);
    //            zipOS.PutNextEntry(zipEntry);
    //            zipOS.Write(files, 0, files.Length);
            
    //        zipOS.Finish();
    //        zipOS.Close();

    //        FileInfo file = new FileInfo(Server.MapPath("~/TempFile/") + strSaveFileAs + ".zip");
    //        if (file.Exists)
    //        {
    //            Response.Clear();
    //            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
    //            Response.AddHeader("Content-Length", file.Length.ToString());
    //            Response.ContentType = "application/zip";
    //            Response.WriteFile(file.FullName);
    //            Response.TransmitFile(file.FullName);
    //            Response.Flush();
    //            file.Delete();
    //            Response.End();
    //        }
        
    //}

}