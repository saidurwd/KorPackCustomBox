
//******************************************
// Author : Faruk Ahmed
// Development Date : 30th of May 2012
// Module : User Authentication
//******************************************

using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Blumen;

namespace StringEncodeDecode
{
    public partial class UserAuthentication_PrmUserInfo : System.Web.UI.Page
    {
        string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
        private string strLoginUserID = null;
        private string myScript = "";
        private string strcompanyID = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("../Logout.aspx");
            }
            else
            {
                this.hidMenuID.Value = "70";
                strcompanyID = "01";
                strLoginUserID = Session["UserID"].ToString();
                if (!IsPostBack)
                {
                    mblnUserPermissionMaster();
                }


            }
        }


        protected void FillUsers()
        {
            SqlConnection connection = new SqlConnection(cnnString);


            try
            {

                grdEmpInfo.DataSource = "SqlDataSource1";
                grdEmpInfo.DataBind();
                if (this.grdEmpInfo.Rows.Count > 0)
                {
                    //Gets or sets a value indicating whether a System.Web.UI.WebControls.GridView
                    //control renders its header in an accessible format. This property is provided
                    //to make the control more accessible to users of assistive technology devices.
                    grdEmpInfo.UseAccessibleHeader = true;
                    grdEmpInfo.HeaderRow.TableSection = TableRowSection.TableHeader;

                }

            }
            catch (SqlException exp)
            {
                Response.Write(exp.Message);
            }
            finally
            {

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            FillUsers();
        }

        protected void grdEmpInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           

        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            try
            {

                FillUsers();
            }


            catch (SqlException exp)
            {
                myScript = "alert('" + exp.Message + "');";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                myScript = "";
            }
        }

        protected void grdEmpInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {




        }

        protected void grdEmpInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
          

        }

        protected void grdEmpInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
        protected void btn_update_Click(object sender, EventArgs e)
        {

        }
        public void Page_LoadComplete()
        {
            //if (Request.Browser.Browser == "Firefox") Response.Cache.SetNoStore();
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (this.grdEmpInfo.Rows.Count > 0)
            {
                //Gets or sets a value indicating whether a System.Web.UI.WebControls.GridView
                //control renders its header in an accessible format. This property is provided
                //to make the control more accessible to users of assistive technology devices.
                grdEmpInfo.UseAccessibleHeader = true;
                grdEmpInfo.HeaderRow.TableSection = TableRowSection.TableHeader;

            }

            string myScript = "$('#loading').hide();";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
            myScript = "";


        }

        private void mblnUserPermissionMaster()
        {
            SqlConnection connectionUA = new SqlConnection(cnnString);
            SelectUserPermissionMaster objDMLUA = new SelectUserPermissionMaster(connectionUA);
            objDMLUA.companyID = "01";
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

    }
}