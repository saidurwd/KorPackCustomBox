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
using Blumen;

public partial class Index2 : System.Web.UI.Page
{
    string strUserID;
    static string strCompanyID;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["UserID"] == null)
        {
            //Response.Redirect("../Default.aspx");
        }
        else
        {
            
            strUserID = Session["UserID"].ToString();
            strCompanyID = Session["CompanyID"].ToString();
            this.txtHidUserID.Value = strUserID;
        }
        if (!IsPostBack)
        {
            GetTask();
            
        }

    }
    public void GetTask()
    {
        //UpdatePanel1.Update();
        //System.Threading.Thread.Sleep(5000);

        //Label2.Text = DateTime.Now.ToString();

        clsHomePage obHomePage = new clsHomePage();
        DataTable dt = new DataTable();
        this.HidtxtWhereCondition.Value = "";
        this.TextBox1.Text = "";
        string strWhereCondition = "";
        try
        {
            dt = obHomePage.GetTask(strUserID, this.cboTaskStatus.SelectedValue.ToString(), "", ref strWhereCondition, strCompanyID);
            this.txtHidTaskShowStatus.Value = this.cboTaskStatus.SelectedValue.ToString();
            if (dt.Rows.Count > 0)
            {
                this.TextBox1.Text = strWhereCondition;
                this.HidtxtWhereCondition.Value = strWhereCondition;
                grdTaskInfo.DataSource = dt;
                grdTaskInfo.DataBind();

                BindAlerts(dt);

            }
            else
            {

                grdTaskInfo.DataSource = null;
                grdTaskInfo.DataBind();

            }

            //01716 578150



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
    private void BindAlerts(DataTable _dt)
    {
        DataView dv = _dt.DefaultView;
        dv.RowFilter = "Reminder=1";
        this.grdAlerts.DataSource = dv;
        this.grdAlerts.DataBind();
        _dt.Dispose();
    }
}

    

