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

public partial class DashBoardNew : System.Web.UI.Page
{
    string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            if (!IsPostBack)
            {
                if (Session["dashBoardMenuTable"] == null)
                {
                    BindFullTable();
                }
                if (Session["CompanyName"] != null)
                {
                    Page.Title = Session["CompanyName"].ToString();
                }
                //BindTopHeader();

                Master.FindControl("navigationMenu").Visible = false;
            }
        }
        else
        {
            string logoutPath = ResolveUrl("~/Logout.aspx");
            Response.Redirect(logoutPath);
        }
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
                //this.ListView1.DataSource = dt;
                //this.ListView1.DataBind();
                //Cache.Insert("dashBoardMenuTable", dt, null, DateTime.Now.AddDays(1.00), TimeSpan.Zero);
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



    protected string MenuCreateMain()
    {
        StringBuilder subMenu = new StringBuilder();
        string subMenuChild = null;
        if (Session["dashBoardMenuTable"] != null)
        {
            DataTable dt = (DataTable)Session["dashBoardMenuTable"];

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ParentID"].ToString() == ConfigurationManager.AppSettings["MenuRootValue"])
                {
                    int i = 1;
                    subMenu.Append(String.Format("<li><a class=\"{2}\" href=DashBoard.aspx?mid={3}></a>", ConfigurationManager.AppSettings["AdminMenuPath"].ToString(), dr["links"], dr["name"],dr["id"]));
                    i++;

                    
                    //string funID = dr["id"].ToString();
                    //DataRow[] drArray = dt.Select("ParentID=" + funID);
                    //if (drArray.Length > 0)
                    //{
                    //    subMenuChild = BuildSubMenu(drArray);
                    //}
                    //if ((subMenuChild != null) && (subMenuChild.Length > 0))
                    //{
                    //    subMenu.Append(subMenuChild);
                    //}
                    //subMenu.Append("</li>");

                    //subMenu.Append(String.Format("<li><a runat=\"serve\" href=~admin/{1}>{2}</a></li>", ConfigurationManager.AppSettings["AdminMenuPath"].ToString(), dr["links"], dr["name"]));
                }
            }
        }
        if (subMenu.Length == 0)
        {
            return String.Empty;
        }
        else
        {
            //Response.Write("=========" + subMenu.ToString());
            return subMenu.ToString();
        }
    }
}
