using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;

using System.Reflection;
using System.ComponentModel;



namespace Blumensoft.Utility
{
    /// <summary>
    /// This class contains general utility functions that are used frequentely in the project
    /// ::: Inherited by PAGE Class.
    /// </summary>
    /// <Author>Harpreet Singh</Author>
    public class BasePage : Page
    {
        public SqlConnection conn = new SqlConnection();
        public BasePage()
        {
           
        }

        /// <summary>
        /// Action wether save,update or delete
        /// </summary>
        public enum RPOStatus
        {
            save = 1,
            Submitted = 2,
            Approved = 3,
            PartialAprroved = 4,
            Rejected = 5

        }
        /// <summary>
        /// Action wether save,update or delete
        /// </summary>
        public enum Action
        {
            save = 1,
            update = 2,
            delete = 3
        }

        //Enumeration containing months name
        public enum Months
        {
            January,
            Febuary,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        };

        /// <summary>
        /// Enum containing messages for displaying 
        /// </summary>
        public enum Messages
        {
            /// <summary>
            /// Message for Save
            /// </summary>
            [Description("Record saved successfully !")]
            save = 1,
            /// <summary>
            /// Message for Update
            /// </summary>
            [Description("Record updated successfully !")]
            update = 2,
            /// <summary>
            /// Message for Delete
            /// </summary>
            [Description("Record deleted successfully !")]
            delete = 3,
            /// <summary>
            /// Message after email sent
            /// </summary>
            [Description("Email Sent Successfully !")]
            Error = 3,
            /// <summary>
            /// Error message
            /// </summary>
            [Description("Sorry,Operation could not be completed!")]
            MailSent = 4
        }

        /// <summary>
        /// Method Used to fetch enum desription 
        /// </summary>
        /// <param name="value">Enum</param>
        /// <returns>Description of enum in string</returns>
        public string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }


        public void Reset(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c.Controls.Count > 0)
                {
                    Reset(c);
                }
                else
                {
                    switch (c.GetType().ToString())
                    {
                        case "System.Web.UI.WebControls.TextBox":
                            ((TextBox)c).Text = "";
                            break;
                        case "System.Web.UI.WebControls.CheckBox":
                            ((CheckBox)c).Checked = false;
                            break;
                        case "System.Web.UI.WebControls.RadioButton":
                            ((RadioButton)c).Checked = false;
                            break;
                        case "System.Web.UI.WebControls.DropDownList":
                            //((DropDownList)c).SelectedIndex = 0;
                            break;

                    }
                }
            }
        }

        /// <summary>
        /// Function converts date into mm/dd/YYYY format from dd/mm/YYYY format
        /// </summary>
        /// <param name="s">Date in string(dd/mm/YYYY) format</param>
        /// <returns>Date in string (mm/dd/YYYY) format</returns>
        public string ConvertToDateString(string s)
        {
            string dt1 = "0";
            try
            {
                string[] tmps;
                tmps = s.Split('/');

                string tm = "", td = "", ty = "";
                if (tmps[0].Length < 2)
                { td = "0" + tmps[0]; }
                else
                {
                    td = tmps[0];
                }

                if (tmps[1].Length < 2)
                { tm = "0" + tmps[1]; }
                else
                {
                    tm = tmps[1];
                }
                ty = tmps[2];
                s = td + "/" + tm + "/" + ty;


                if (s.Length == 9 & s.Substring(1, 1) == "/" & s.Substring(4, 1) == "/")
                {
                    s = s.Substring(0, 9);

                    Int32 d = 0, m = 0, y = 400000, index, index1;
                    index = s.IndexOf('/');
                    index1 = s.IndexOf('/', index + 1);

                    d = Convert.ToInt32(s.Substring(0, index));
                    m = Convert.ToInt32(s.Substring(index + 1, index1 - index - 1));
                    y = Convert.ToInt32(s.Substring(index1 + 1));

                    dt1 = m + "/" + d + "/" + y;
                }



                if (s.Length == 10 & s.Substring(2, 1) == "/" & s.Substring(5, 1) == "/")
                {
                    s = s.Substring(0, 10);

                    Int32 d = 0, m = 0, y = 400000, index, index1;
                    index = s.IndexOf('/');
                    index1 = s.IndexOf('/', index + 1);

                    d = Convert.ToInt32(s.Substring(0, index));
                    m = Convert.ToInt32(s.Substring(index + 1, index1 - index - 1));
                    y = Convert.ToInt32(s.Substring(index1 + 1));

                    dt1 = m + "/" + d + "/" + y;
                }
                return dt1;

            }
            catch (Exception formatDate)
            {

                dt1 = "0";
            }
            return (dt1);
        }

        /// <summary>
        /// Returns client IP address
        /// </summary>
        /// <returns>IP address of the client</returns>
        public string ClientIPAddress()
        {
            // return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            string _IP = null;
            System.Net.IPHostEntry _IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (System.Net.IPAddress _IPAddress in _IPHostEntry.AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    _IP = _IPAddress.ToString();
                }
            }
            return _IP;
        }


        /// <summary>
        /// This function is Used to convert datatable to  HTML table 
        /// </summary>
        /// <param name="thisTable">datatable</param>
        /// <returns>HTML Table markup in String format</returns>
        public string GetDataTableAsHTML(DataTable thisTable)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat(@"<caption> Total Rows =");
            sb.AppendFormat(thisTable.Rows.Count.ToString());
            sb.AppendFormat(@"  </caption>");
            sb.Append("<TABLE BORDER=1>");
            sb.Append("<TR ALIGN='CENTER' style='background-color: #3d68d0;color:White;font-size:smaller;font-family:Tahoma '>");
            //first append the column names.
            foreach (DataColumn column in thisTable.Columns)
            {
                sb.Append("<TD><B>");
                sb.Append(column.ColumnName);
                sb.Append("</B></TD>");
            }

            sb.Append("</TR>");

            // next, the column values.
            foreach (DataRow row in thisTable.Rows)
            {
                sb.Append("<TR ALIGN='CENTER' style='font-size:smaller;font-family:Tahoma'>");

                foreach (DataColumn column in thisTable.Columns)
                {
                    sb.Append("<TD>");
                    if (row[column].ToString().Trim().Length > 0)
                        sb.Append(row[column]);
                    else
                        sb.Append("&nbsp;");
                    sb.Append("</TD>");
                }

                sb.Append("</TR>");
            }
            sb.Append("</TABLE>");

            return sb.ToString();
        }

        /// <summary>
        /// This method is used for filling dataset with procedure
        /// </summary>
        /// <param name="Procedure">Name of stored procedure</param>
        /// <param name="values">parameters in string format seprated by comma</param>
        /// <returns>Dataset containing Data</returns>
        public DataSet FillDataset(string Procedure, params string[] values)
        {

            DataSet ds = new DataSet();
            int i;
            System.Data.SqlClient.SqlDataAdapter adp;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(Procedure);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = conn;
                SqlCommandBuilder.DeriveParameters(cmd);
                for (i = 1; i <= cmd.Parameters.Count - 1; i++)
                {
                    cmd.Parameters[i].Value = values[i - 1];
                }
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
            }

            finally
            {
                conn.Close();

            }
            return ds;
        }

        /// <summary>
        /// This method is used for filling datatable with procedure
        /// </summary>
        /// <param name="Procedure">Name of stored procedure</param>
        /// <param name="values">parameters in string format seprated by comma</param>
        /// <returns>Datatable containing Data</returns>
        public DataTable FillDataTable(string Procedure, params string[] values)
        {

            DataTable dt = new DataTable();
            int i;
            SqlDataAdapter adp;
            try
            {
                //conn.Open();
                SqlCommand cmd = new SqlCommand(Procedure);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = conn;
                SqlCommandBuilder.DeriveParameters(cmd);
                for (i = 1; i <= cmd.Parameters.Count - 1; i++)
                {
                    cmd.Parameters[i].Value = values[i - 1];
                }
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
            }
            finally
            {
                conn.Close();

            }
            return dt;
        }

        /// <summary>
        /// Sets the Title of the page on the label on master page
        /// </summary>
        /// <param name="page">page</param>
        /// <param name="Name">Name</param>
        public void SetPageTitle(Page page, string Name)
        {
            //Label lblTitle = ((Label)(page.Master.FindControl("lblPageTitle")));
            //lblTitle.Text = Name;
        }


        /// <summary>
        /// Display the Help of the page on the master page
        /// </summary>
        /// <param name="page">page</param>
        /// <param name="Name">Name</param>
        public void ShowPageHelp(Page page)
        {
            //((HtmlGenericControl)(page.Master.FindControl("DivHelpContent")));
            //lblTitle.Text = Name;
            //(HtmlGenericControl)c.FindControl("t1");
        }

        /// <summary>
        /// Displays the Alert.
        /// </summary>
        /// <param name="message">The message to display.</param>
        protected virtual void DisplayAlert(string message)
        {
            ClientScript.RegisterStartupScript(
                            GetType(),
                            Guid.NewGuid().ToString(),
                            string.Format("alert('{0}');", message.Replace("'", @"\'")),
                            true
                        );
        }



        /// <summary>
        /// Function to send the E-mail
        /// </summary>
        /// <param name="message">message/Body of Email</param>
        /// <param name="Reciever">Receiver Email address</param>
        /// <param name="Sender">Sender Email address</param>
        /// <param name="Password">Password of sender</param>
        /// <param name="Subject">Subject of E-Mail</param>
        /// <param name="IsHtml">wether email is HTML or Plain Text : true if HTML- false if Text</param>
        public void SendMail(string message, string Reciever, string Sender, string Password, string Subject, Boolean IsHtml)
        {
            try
            {
                string strFinal = string.Empty;
                String FromEmailId = Sender;
                String EmailPwd = Password;

                if (Reciever != string.Empty)
                {
                    if (CheckEmail(Reciever).Equals(true))
                    {
                        System.Net.Mail.MailMessage objEmail = new System.Net.Mail.MailMessage(FromEmailId, Convert.ToString(Reciever));
                        objEmail.From = new System.Net.Mail.MailAddress(FromEmailId);
                        objEmail.Subject = Subject;
                        strFinal = message;
                        objEmail.IsBodyHtml = IsHtml;
                        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.googlemail.com", 587);
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential(FromEmailId, EmailPwd);
                        objEmail.Priority = System.Net.Mail.MailPriority.High;
                        client.EnableSsl = true;
                        objEmail.Body = strFinal;
                        client.Send(objEmail);
                    }

                }

            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Function for validating the email address by regular expression
        /// </summary>
        /// <param name="EmailAddress">Email address</param>
        /// <returns>True/False</returns>
        /// <Author>Harpreet Singh</Author>
        public static bool CheckEmail(string EmailAddress)
        {

            string strPattern = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            if (System.Text.RegularExpressions.Regex.IsMatch(EmailAddress, strPattern))
            { return true; }
            return false;
        }


        /// <summary>
        /// Function to export data to excel.
        /// </summary>
        /// <param name="Control">Gridview</param>
        /// <param name="St">String Header on excel file</param>
        public void ExportData(GridView Control, String St)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=FileName.xls");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            Control.RenderControl(htmlWrite);
            HttpContext.Current.Response.Write(St + stringWrite.ToString());
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// Function assisting the ExportData() function for rendering the control
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        /// <summary>
        /// Convert string to Title Case
        /// </summary>
        /// <param name="inputString">String as Input</param>
        /// <returns>string in title case</returns>
        public string TitleCase(string inputString)
        {
            TextInfo UsaTextInfo = new CultureInfo("en-US", false).TextInfo;
            string capitalized = UsaTextInfo.ToTitleCase(inputString);
            return capitalized;
        }


        /// <summary>
        /// Convert String with seprator to an array of string
        /// </summary>
        /// <param name="strIds">string </param>
        /// <param name="separator">seprator</param>
        /// <returns>String Array</returns>
        public string[] ConvertToArray(string strIds, string separator)
        {
            int size = (strIds != null && strIds.Length > 0) ? 1 : 0;
            //Determine how many semi-colon separated values are in string
            string temp = strIds;
            int pos;
            while (temp.IndexOf(separator) > -1)
            {
                size++;
                pos = temp.IndexOf(separator);
                temp = temp.Substring(pos + 1).Trim();
            }
            string[] strArray = new string[size];
            for (int i = 0; i < size; i++)
            {

                pos = (strIds.Trim().IndexOf(separator) == -1) ? strIds.Trim().Length : strIds.IndexOf(separator);
                //Now get the string within the single quotes; trimming them off
                string val = strIds.Trim().Substring(0, pos).Trim();
                strArray[i] = val;
                if (strIds.Length > (pos + 1))
                {
                    strIds = strIds.Substring(pos + 1).Trim();
                }
            }
            return strArray;
        }

        /// <summary>
        /// Convert array to string
        /// </summary>
        /// <param name="array">array</param>
        /// <param name="Seprator">seprator</param>
        /// <returns>string joined by all array items</returns>
        public string ConvertArrayToString(string[] array, string Seprator)
        {
            string result = string.Join(Seprator, array);
            return result;
        }


        /// <summary>
        ///Function to disable all the controls 
        /// </summary>
        /// <param name="c">page (this.page)</param>
        public void DisableAll(Control c)
        {
            // disable all controls
            if (c is WebControl)
                ((WebControl)c).Enabled = false;
            foreach (Control child in c.Controls)
                DisableAll(child);

        }


        /// <summary>
        /// This function gets the current browser name and its version
        /// </summary>
        /// <returns>Browser name and its version</returns>
        public string GetBrowserName()
        {
            return "Browser Name : " + Request.Browser.Type.ToString() + " (Version : " + Request.Browser.Version.ToString() + ")";
        }

        /// <summary>
        /// Function Checks if the date supplied is a valid date or not
        /// </summary>
        /// <param name="myDate">Date in string format</param>
        /// <returns>True if success,else false</returns>
        public bool IsDate(string myDate)
        {
            try
            {
                DateTime.Parse(myDate);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// Check session state
        /// </summary>
        private void CheckSession()
        {
            //check to see if the Session is null (doesnt exist)
            if (Context.Session != null)
            {
                //check the IsNewSession value, this will tell us if the session has been reset.
                //IsNewSession will also let us know if the users session has timed out
                if (Session.IsNewSession)
                {
                    //now we know it's a new session, so we check to see if a cookie is present
                    string cookie = Request.Headers["Cookie"];
                    //now we determine if there is a cookie does it contains what we're looking for
                    if ((null != cookie) && (cookie.IndexOf("ASP.NET_SessionId") >= 0))
                    {
                        //since it's a new session but a ASP.Net cookie exist we know
                        //the session has expired so we need to redirect them
                        string PageName = ConfigurationManager.AppSettings["ErrorPage"].ToString();
                        Response.Redirect(PageName + "?timeout=yes&success=no", false);
                    }
                }
            }
        }

        #region [FETCHING VALUES FROM SESSION VARIABLE]
        /// <summary>
        /// User ID of logged in user
        /// </summary>
        public Int32 UserID
        {
            get { return Convert.ToInt32(GetValue("UserID")); }
        }

        /// <summary>
        /// User Name of logged in user
        /// </summary>
        public string UserName
        {
            get { return GetValue("UserName"); }
        }
        /// <summary>
        /// Company ID of logged in user
        /// </summary>
        public Int32 CompanyID
        {
            get { return Convert.ToInt32(GetValue("CompanyID")); }
        }

        /// <summary>
        /// Role ID of logged in user
        /// </summary>
        public Int32 RoleID
        {
            get { return Convert.ToInt32(GetValue("RoleID")); }
        }

        /// <summary>
        /// Login time of logged in user
        /// </summary>
        public string LoginTime
        {
            get { return GetValue("LoginTime"); }
        }

        /// <summary>
        /// IP address of logged in user
        /// </summary>
        public string IPAddress
        {
            get { return ClientIPAddress(); }
        }
        /// <summary>
        /// Password of logged in user
        /// </summary>
        public string Password
        {
            get { return GetValue("Password"); }
        }

        /// <summary>
        /// first name of the user
        /// </summary>
        public string FirstName
        {
            get { return GetValue("Firstname"); }
        }

        /// <summary>
        /// last name of the user
        /// </summary>
        public string LastName
        {
            get { return GetValue("Lastname"); }
        }
        /// <summary>
        /// Email of the user
        /// </summary>
        public string Email
        {
            get { return GetValue("Email"); }
        }
        /// <summary>
        /// Image name of the user
        /// </summary>
        public string UserImage
        {
            get { return GetValue("UserImage"); }
        }

        /// <summary>
        /// Sets the value of the properties obove according to the key name in the dictionary (placed in session)
        /// </summary>
        /// <param name="key">Name of the key whose value is to be find</param>
        /// <returns>value from session</returns>
        private String GetValue(string key)
        {
            string Value = "0";
            if (Context.Session != null)
            {
                //check the IsNewSession value, this will tell us if the session has been reset.
                //IsNewSession will also let us know if the users session has timed out
                //if (Session.IsNewSession)
                //{
                if (Session["UserInfo"] != null)
                {
                    Dictionary<string, string> UserInfo = (Dictionary<string, string>)(Session["UserInfo"]);
                    if (UserInfo.ContainsKey(key))
                    {
                        Value = UserInfo[key];
                    }
                }
                // }
            }
            if (Value.Equals("0")) // if Session does not exists or expired then redirect to Timeout page
            {
                HttpContext.Current.Response.Redirect("~/SessionTimeOut.htm", false);
            }
            return Value;
        }

        #endregion

        #region [Handling Errors]
        /// <summary>
        /// This mrthod is used for logging the exception into the database.
        /// </summary>
        /// <param name="ex">Exception object containing all the details of the exception</param>
        /// <param name="FunctionName">NAme of the method in which error raised</param>
        public void HandleError(Exception ex, string FunctionName)
        {
            SqlCommand cmd = new SqlCommand("USP_LogError", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ErrorMessage", ex.Message.ToString());
            cmd.Parameters.AddWithValue("@IPAddress", IPAddress);
            cmd.Parameters.AddWithValue("@FormName", Page.Title);
            cmd.Parameters.AddWithValue("@MethodName", FunctionName);
            cmd.Parameters.AddWithValue("@ErrorDateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@ErrorStack", ex.StackTrace);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        #endregion


    }
}
