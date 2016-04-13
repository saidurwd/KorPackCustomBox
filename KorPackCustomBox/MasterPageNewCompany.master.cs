using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using Blumen;

public partial class MasterPageNewCompany : System.Web.UI.MasterPage
{
    string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["UserID"] == null)
        //{
        //    Response.Redirect(ResolveUrl("~/Logout.aspx"));
        //}
        //else
        //{
            //if (!IsPostBack)
            //{
            //    //Response.Write(Request.RawUrl);
            //    if (Request.QueryString["mid"] != null)
            //    {
            //        Session.Add("pidSession", Request.QueryString["mid"].ToString());
            //        //this.navigationMenu.Visible = true;
            //    }

            //    if (Session["dashBoardMenuTable"] == null)
            //    {
            //        BindFullTable();
            //    }
            //    if (Session["CompanyName"] != null)
            //    {
            //        Page.Title = Session["CompanyName"].ToString();
            //    }
            //    //BindTopHeader();


            //    this.hidCompanyID.Value = (Session["CompanyID"] == null) ? String.Empty : Session["CompanyID"].ToString();
                //this.lblCompanyName.Text = (Session["CompanyName"] == null) ? " No Company" : Session["CompanyName"].ToString();
                this.lblCompanyName.Text = "Install New Company";

                //this.HidBackDatedPosting.Value = Session["BACK_DATE_POSTING"].ToString();

                
            //}

    }


    private DataTable CreateDataTable()
    {
        DataTable myDataTable = new DataTable();

        DataColumn myDataColumn;

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "id";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "ParentID";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "name";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "links";
        myDataTable.Columns.Add(myDataColumn);

        return myDataTable;
    }

    private void BindFullTable()
    {
        SqlConnection connection = new SqlConnection(cnnString);
        SelectcmsmenuALL objDML = new SelectcmsmenuALL(connection);
        try
        {
            objDML.Connection.Open();
            objDML.Transaction = objDML.Connection.BeginTransaction("myTran");
            objDML.loginID = Session["LoginID"].ToString();
            objDML.CompanyID = Session["CompanyID"].ToString();
            DataTable dt = objDML.ExecuteDataSet().Tables[0];
            if (objDML.ReturnValue == 0)
            {
                objDML.Transaction.Commit();
                Session.Add("dashBoardMenuTable", dt);
            }
        }
        catch (SqlException exp)
        {
            objDML.Transaction.Rollback();

            string myScript = "showError('" + Resources.Resource.DataBaseError + "');";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
            myScript = "";

        }
        catch (Exception exp)
        {

            objDML.Transaction.Rollback();
            string myScript = "showError('" + Resources.Resource.GeneralError + "');";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
            myScript = "";

        }
        finally
        {
            if (objDML.Connection.State == ConnectionState.Open)
            {
                objDML.Connection.Close();
                objDML.Connection.Dispose();

            }
        }
    }


}