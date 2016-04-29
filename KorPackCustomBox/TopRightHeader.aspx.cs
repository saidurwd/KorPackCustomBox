using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using Blumen;

public partial class TopRightHeader : System.Web.UI.Page
{
    string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect(ResolveUrl("~/Logout.aspx"));
        }
        else
        {
            if (!IsPostBack)
            {
                GetTask();
                //hlnkInbox.Text = "Inbox (" + System.DateTime.Now.ToString() + ")"; 
                //if (Request.Cookies["culture"]!= null) ddlCulture.SelectedValue = Request.Cookies["culture"].Value; 
                if (Session["dashBoardMenuTable"] == null)
                {
                    BindFullTable();
                }
            }
        }
    }
    public void GetTask()
    {
        clsHomePage obHomePage = new clsHomePage();
        DataTable dt = new DataTable();

        string strWhereCondition = "";
        try
        {
            dt = obHomePage.GetTaskCount(Session["UserID"].ToString(), "All", "", ref strWhereCondition, Session["CompanyID"].ToString());

            if (dt.Rows.Count > 0)
            {
                hlnkInbox.Text = "<a id='An' class='thickbox' href='http://localhost/blumensoft/Alerts.aspx?TB_iframe=true&height=480&width=700'>Inbox (<span style='color:Red'>" + dt.Rows[0][0].ToString() + "</span>)</a>";
            }
            else
            {
                hlnkInbox.Text = "Inbox (0)";
            }

        }
        catch (SqlException exp)
        {
            Response.Write(exp.Message);
        }
        finally
        {
            dt.Dispose();
            obHomePage.Dispose();
        }



    }
    protected void ddlCulture_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Response.Cookies["culture"].Value
        //  = ddlCulture.SelectedValue; 
        //Response.Redirect(Request.Path);
    }
    protected string RenderMenu()
    {
        var result = new StringBuilder();
        RenderMenuItem("HOME", "Default.aspx", result);
        RenderMenuItem("DASHBOARD", "About.aspx", result);
        RenderMenuItem("CRM", "Default.aspx", result);
        RenderMenuItem("PURCHASE", "About.aspx", result);
        RenderMenuItem("INVENTORY MANAGEMENT", "About.aspx", result);
        return result.ToString();
    }

    void RenderMenuItem(string title, string address, StringBuilder output)
    {
        output.AppendFormat("<li><a href=\"{0}\" ", address);

        var requestUrl = HttpContext.Current.Request.Url;
        if (requestUrl.Segments[requestUrl.Segments.Length - 1].Equals(address, StringComparison.OrdinalIgnoreCase)) // If the requested address is this menu item.
            output.Append("class=\"ActiveMenuButton\"");
        else
            output.Append("class=\"MenuButton\"");

        output.AppendFormat("><span>{0}</span></a></li>|", title);
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
    protected string MenuCreate()
    {
        StringBuilder subMenu = new StringBuilder();
        string strMenuRootParent = "1";
        string subMenuChild = null;


        strMenuRootParent = (ConfigurationManager.AppSettings["MenuRootValue"] == null) ? "1" : (ConfigurationManager.AppSettings["MenuRootValue"].ToString());



        if (Session["dashBoardMenuTable"] != null)
        {
            DataTable dt = (DataTable)Session["dashBoardMenuTable"];

            foreach (DataRow dr in dt.Rows)
            {
                if ((dr["ParentID"].ToString() == strMenuRootParent) && (dr["Name"].ToString() != "System Tools")) //Session["pidSession"].ToString()
                {
                    subMenu.Append(String.Format("<li class=top><a class=top_link href={0}><span>{1}</span></a>", ResolveUrl("~/" + dr["links"]), dr["name"]));
                    string funID = dr["id"].ToString();
                    string fileterExpression = " ParentID =" + funID;
                    DataRow[] drArray = dt.Select(fileterExpression); //+ funID
                    if (drArray.Length > 0)
                    {
                        subMenu.Append("<ul class=sub>");
                        BuildSubMenu(drArray, dt, subMenu);
                        subMenu.Append("</ul>");
                    }

                    subMenu.Append("</li>");

                }
            }
        }



        if (subMenu.Length == 0)
        {
            return String.Empty;
        }
        else
        {

            return subMenu.ToString();
        }
    }

    protected void BuildSubMenu(DataRow[] drSub, DataTable mainDT, StringBuilder strSubMenu)
    {

        foreach (DataRow dr in drSub)
        {
            DataRow[] drArray2 = mainDT.Select("ParentID=" + dr[0].ToString());
            if (drArray2.Length > 0)
            {
                //if (dr["ParentID"].ToString() != "85")
                //{
                strSubMenu.Append(String.Format("<li><a class=fly href={0}>{1}</a>", ResolveUrl("~/" + dr["links"]), dr["name"]));
                strSubMenu.Append("<ul>");
                BuildSubMenu(drArray2, mainDT, strSubMenu);
                strSubMenu.Append("</ul>");
                strSubMenu.Append("</li>");
                //}
            }
            else
            {

                strSubMenu.Append(String.Format("<li><a href={0}>{1}</a></li>", ResolveUrl("~/" + dr["links"]), dr["name"]));

            }
        }

    }

    protected string SmallSubMenuSetting()
    {
        StringBuilder subMenu = new StringBuilder();
        DataTable dt = (DataTable)Session["dashBoardMenuTable"];

        DataRow[] drTestPermission = dt.Select(" Name ='System Tools'");
        if (drTestPermission.Length > 0)
        {
            subMenu.Append("<ul style=display:none; class=subSettings >");
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ParentID"].ToString() == "85")
                {
                    subMenu.Append(String.Format("<li><a href={0}><span>{1}</span></a>", ResolveUrl("~/" + dr["links"]), dr["name"]));
                }
            }

            subMenu.Append("</ul>");
        }

        if (subMenu.Length > 0)
            return subMenu.ToString();

        else
            return String.Empty;


    }
    protected string logoutusername()
    {

        string strUserID = Session["LoginID"].ToString();


        if (strUserID.Length > 0)
            //return "Logout (<span style='color:#FF9C08;'>" + strUserID + "</span>)";
            return "" + strUserID + "";

        else
            return String.Empty;


    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        //UpdateLabel();
        GetTask();
    }

    private void UpdateLabel()
    {
        //hidTimerValue.Value = (Int32.Parse(hidTimerValue.Value) + 1).ToString();
        hlnkInbox.Text = hidTimerValue.Value;
        hlnkInbox.Text = "Inbox (" + System.DateTime.Now.ToString() + ")";

    }
}