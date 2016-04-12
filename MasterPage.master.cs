using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Data.SqlClient;
using System.Text;
using Blumen;

public partial class MasterPage : System.Web.UI.MasterPage
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
        if (!IsPostBack)
        {
            Page.Title = "KORPACK";
            this.hidCompanyID.Value = "01";
            idmenu.Visible = false;
            idchangepass.Visible = false;
            idlogout.Visible = false;
            idlogin.Visible = true;
            if (Session["UserID"] != null)
            {
                idchangepass.Visible = true;
                idlogout.Visible = true;
                idlogin.Visible = false;
                liSignUP.Visible = false;
                alogout.InnerText = "Logout (" + Session["UserName"] + ")";
                if (Session["dashBoardMenuTable"] == null)
                {
                    BindFullTable();
                }
                if (Session["UserID"].ToString() != "2")
                {
                    idmenu.Visible = false;
                }
                else
                { idmenu.Visible = true; }
                this.hidUserID.Value = Session["UserID"].ToString();
            }
        }

        //}
    }

    private void BindFullTable()
    {
        SqlConnection connection = new SqlConnection(cnnString);
        SelectRolemenuALL objDML = new SelectRolemenuALL(connection);
        try
        {

            objDML.Connection.Open();
            objDML.Transaction = objDML.Connection.BeginTransaction("myTran");
            objDML.loginID = Session["LoginID"].ToString();
            objDML.CompanyID = "01";
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

    protected void LnkLogout_Click(object sender, EventArgs e)
    {
        //Session.Remove("UserID");
        Session["UserID"] = null;

        string rootPathForAdmin = System.Configuration.ConfigurationManager.AppSettings["AdminMenuPath"].ToString();
        rootPathForAdmin += "Logout.aspx";
        Response.Redirect(rootPathForAdmin);

    }

    protected string SmallSubMenuSetting()
    {
        StringBuilder subMenu = new StringBuilder();
        DataTable dt = (DataTable)Session["dashBoardMenuTable"];

        DataRow[] drTestPermission = dt.Select(" Name ='System Tools'");
        if (drTestPermission.Length > 0)
        {
            subMenu.Append("<ul class=subSettings >");
            foreach (DataRow dr in dt.Rows)
            {

                if (
                    (dr["Name"].ToString() == "Manage User")
                    || (dr["Name"].ToString() == "Manage Customer") || (dr["Name"].ToString() == "Box Calculation Log")
                    || (dr["Name"].ToString() == "Contact US History")
                    )
                {
                    if (dr["ParentID"].ToString() == "85")
                    {
                        subMenu.Append(String.Format("<li><a href={0}><span>{1}</span></a></li>", ResolveUrl("~/" + dr["links"]), dr["name"]));
                    }
                }
            }

            subMenu.Append("</ul>");
        }

        if (subMenu.Length > 0)
            return subMenu.ToString();

        else
            return String.Empty;


    }
    protected string SmallSubMenuSetting2()
    {
        StringBuilder subMenu = new StringBuilder();
        if (Session["dashBoardMenuTable"] != null)
        {
            DataTable dt = (DataTable)Session["dashBoardMenuTable"];

            DataRow[] drTestPermission = dt.Select(" Name ='System Tools'");
            if (drTestPermission.Length > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {

                    if (
                        (dr["Name"].ToString() == "Manage User")
                        || (dr["Name"].ToString() == "Manage Customer") || (dr["Name"].ToString() == "Box Calculation Log")
                        || (dr["Name"].ToString() == "Contact US History")
                        || (dr["Name"].ToString() == "Manage Sales Person")
                        )
                    {
                        if (dr["ParentID"].ToString() == "85")
                        {
                            subMenu.Append(String.Format("<li><a href={0}>{1}</a></li>", ResolveUrl("~/" + dr["links"]), dr["name"]));
                        }
                    }
                }


            }
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
            return "Logout (<span style='color:#FF9C08;'>" + strUserID + "</span>)";

        else
            return String.Empty;


    }


}
