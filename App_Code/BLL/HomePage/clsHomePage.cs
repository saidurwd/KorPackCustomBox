using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for clsHomePage
/// </summary>
namespace Blumen
{
    public class clsHomePage : DAL.BaseClass
    {
        String strSQL;
        public clsHomePage()
            : base(ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString)
        {
            strSQL = "";
        }
        public DataTable GetSendBox(String UserID)
        {
            strSQL = @"Select distinct Task,DueDate,AssignedBy,
                    AssignedTo,Status,AutoID,AssignedToID,AssignedByID,TaskID,FullTask,PageURL,JobID,Reminder
                    from
                    (                        
                    Select distinct t.task Task,isnull(convert(varchar,ta.DueDate,103),'') DueDate
                    ,'Self' AssignedBy
                    ,'' AssignedTo
                    ,'' Status,0 AutoID,'' AssignedToID
                    ,t.createdby AssignedByID,t.TaskID,t.task as FullTask
                    ,isnull(t.PageURL,'') PageURL,isnull(t.Jobno,'') JobID,isnull(t.Reminder,0) Reminder,'' Done,'' Archive
                    From TB_PrmTask t
                    left join TB_TaskAssign ta on t.taskid=ta.taskid
                    left join TB_PrmUserInfo ui on t.createdby=ui.empautoid
                    left join HRM_TB_EmployeeInfo ei on ui.empautoid=ei.empautoid
                    where t.createdby=" + UserID + @" and t.task is not null and t.Status is null) tb1  order by AutoID Desc";
            return this.ExecuteSQLStringDataTable(strSQL);
        }

        public DataTable GetTask(String UserID, string whereCond, string Reminder, ref string paramWhereCondition, string companyID)
        {
            string strTaskStatus = "";
            string strWhereCon = "";
            string strAssignTo = " ta.assignedto=" + UserID;


            if (whereCond == "All")
            {
                strWhereCon = "" + strAssignTo;
            }
            else if (whereCond == "Archive")
            {
                strWhereCon = " (ta.Archive=1) and " + strAssignTo;
                strTaskStatus = "Archieve";
            }
            else if (whereCond == "AssignByMe")
            {
                strWhereCon = " t.createdby=" + UserID;
                strAssignTo = "";
            }
            else if (whereCond == "Active")
            {
                strWhereCon = " (ta.Archive=0 or ta.Archive is null) and " + strAssignTo;
            }


            //if (Reminder.Length > 0)
            //{
            //    Reminder = " where Reminder='True' and (Done='False' or Archive='False')";
            //}
            //else
            //{
            //    Reminder = " Where Reminder is null";
            //}
            paramWhereCondition = strWhereCon;


            //Ashfaq Comments: 
            //  Why is this strSQL is not inside any condition(if....else) ?If you can make 1 query with just modifying 
            //  where condition it will be very helpfull for my report.For now, report is populating for Inbox only not Archive and Sent Item. 
            //


            if (whereCond == "Archive")
            {
                strSQL = @" SELECT * FROM(
                        Select distinct Task,DueDate,AssignedBy,
                        AssignedTo,Status,AutoID,AssignedToID,AssignedByID,TaskID,FullTask,PageURL,JobID,Reminder
                        from
                        (                        
                        Select distinct t.task Task,isnull(convert(varchar,ta.DueDate,103),'Nil') DueDate
                        ,isnull(ui.UserName,'Nil') AssignedBy
                        ,isnull(ui1.UserName,'Nil') AssignedTo
                        ,Status=CASE isnull(ta.Done,0) WHEN 1 THEN 'Done' WHEN 0 THEN 'Incomplete' END,isnull(ta.AutoID,0) AutoID,ta.AssignedTo as AssignedToID
                        ,isnull(ta.AssignedBy,t.createdby) AssignedByID,t.TaskID,t.task as FullTask
                        ,isnull(t.PageURL,'') PageURL,isnull(t.Jobno,'') JobID,isnull(t.Reminder,0) Reminder,ta.Done,ta.Archive
                        From TB_TaskAssign ta
                        left join TB_PrmTask t on t.taskid=ta.taskid                        
                        left join TB_PrmUserInfo ui on ta.assignedby=ui.userid
                        left join TB_PrmUserInfo ui1 on ta.assignedto=ui1.userid
                        
                        where " + strWhereCon + @") tb1 " + Reminder + @" Union 
                        Select distinct Task,DueDate,AssignedBy,
                        AssignedTo,Status,AutoID,AssignedToID,AssignedByID,TaskID,FullTask,PageURL,JobID,Reminder
                        from
                        (                        
                        Select distinct t.task Task,isnull(convert(varchar,ta.DueDate,103),'') DueDate
                        ,'Self' AssignedBy
                        ,'' AssignedTo
                        ,'' Status,0 AutoID,'' AssignedToID
                        ,t.createdby AssignedByID,t.TaskID,t.task as FullTask
                        ,isnull(t.PageURL,'') PageURL,isnull(t.Jobno,'') JobID,isnull(t.Reminder,0) Reminder,'' Done,'' Archive
                        From TB_PrmTask t
                        left join TB_TaskAssign ta on t.taskid=ta.taskid
                        left join TB_PrmUserInfo ui on t.createdby=ui.empautoid
                        
                        where t.createdby=" + UserID + @" and t.task is not null and t.Status='Archive') tb1  
                        ) tblMain order by AutoID Desc";

            }
            else
            {
                strSQL = @" Select distinct Task,DueDate,AssignedBy,
                        AssignedTo,Status,AutoID,AssignedToID,AssignedByID,TaskID,FullTask,PageURL,JobID,Reminder
                        from
                        (                        
                        Select distinct t.task Task,isnull(convert(varchar,ta.DueDate,103),'Nil') DueDate
                        ,isnull(ui.UserName,'Nil') AssignedBy
                        ,isnull(ui1.UserName,'Nil') AssignedTo
                        ,Status=CASE isnull(ta.Done,0) WHEN 1 THEN 'Done' WHEN 0 THEN 'Incomplete' END,isnull(ta.AutoID,0) AutoID,ta.AssignedTo as AssignedToID
                        ,isnull(ta.AssignedBy,t.createdby) AssignedByID,t.TaskID,t.task as FullTask
                        ,isnull(t.PageURL,'') PageURL,isnull(t.Jobno,'') JobID,isnull(t.Reminder,0) Reminder,ta.Done,ta.Archive
                        From TB_TaskAssign ta
                        left join TB_PrmTask t on t.taskid=ta.taskid                        
                        left join TB_PrmUserInfo ui on ta.assignedby=ui.userid
                        left join TB_PrmUserInfo ui1 on ta.assignedto=ui1.userid
                        
                        where " + strWhereCon + @") tb1 " + Reminder + @" order by tb1.Status Desc,AutoID Desc";
            }

            #region ==============Rough Code=================
            //Select distinct substring(t.task,0,40)+'...' Task,isnull(convert(varchar,ta.DueDate,103),'Nil') DueDate
            //            ,isnull(ei.FirstName+' '+ei.LastName,'Nil') AssignedBy
            //            ,isnull(ei1.FirstName+' '+ei1.LastName,'Nil') as AssignedTo
            //            ,Status=CASE isnull(ta.Done,0) WHEN 1 THEN 'Complete' WHEN 0 THEN 'Incomplete' END,isnull(ta.AutoID,0) AutoID,ta.AssignedTo as AssignedToID
            //            ,isnull(ta.AssignedBy,t.createdby) AssignedByID,t.TaskID,t.task as FullTask
            //            ,isnull(t.PageURL,'') PageURL,isnull(t.Jobno,'') JobID,t.Reminder,ta.Done,ta.Archive
            //            From TB_PrmTask t
            //            left join TB_TaskAssign ta on t.taskid=ta.taskid                        
            //            left join TB_PrmUserInfo ui on ta.assignedby=ui.userid
            //            left join TB_PrmUserInfo ui1 on ta.assignedto=ui1.userid
            //            left join HRM_TB_EmployeeInfo ei on ui.userid=ei.empautoid
            //            left join HRM_TB_EmployeeInfo ei1 on ui1.userid=ei1.empautoid
            //            where " + strWhereCon+@" t.createdby=" + UserID +
            //            @"union


            //                        @"union
            //                        Select distinct substring(t.task,0,40)+'...' Task,isnull(convert(varchar,ta.DueDate,103),'Nil') DueDate
            //                        ,isnull(ei.FirstName+' '+ei.LastName,'Nil') AssignedBy
            //                        ,isnull(ei1.FirstName+' '+ei1.LastName,'Nil') AssignedTo
            //                        ,Status=CASE isnull(ta.Done,0) WHEN 1 THEN 'Complete' WHEN 0 THEN 'Incomplete' END,isnull(ta.AutoID,0) AutoID,ta.AssignedTo as AssignedToID
            //                        ,isnull(ta.AssignedBy,t.createdby) AssignedByID,t.TaskID,t.task as FullTask
            //                        ,isnull(t.PageURL,'') PageURL,isnull(t.Jobno,'') JobID,t.Reminder,ta.Done,ta.Archive
            //                        From TB_PrmTask t
            //                        left join TB_TaskAssign ta on t.taskid=ta.taskid                        
            //                        left join TB_PrmUserInfo ui on ta.assignedby=ui.userid
            //                        left join TB_PrmUserInfo ui1 on ta.assignedto=ui1.userid
            //                        left join HRM_TB_EmployeeInfo ei on ui.userid=ei.empautoid
            //                        left join HRM_TB_EmployeeInfo ei1 on ui1.userid=ei1.empautoid
            //                        where " + strWhereCon + @"  ta.assignedby=" + UserID +
            //                        @") tb1 "+Reminder+@" order by AutoID Desc";

            #endregion


            return this.ExecuteSQLStringDataTable(strSQL);
        }


        public static void SetPagerButtonStates(GridView gridView, GridViewRow gvPagerRow, Page page)
        {



            int pageIndex = gridView.PageIndex;

            int pageCount = gridView.PageCount;



            Button btnFirst = (Button)gvPagerRow.FindControl("btnFirst");

            Button btnPrevious = (Button)gvPagerRow.FindControl("btnPrevious");

            Button btnNext = (Button)gvPagerRow.FindControl("btnNext");

            Button btnLast = (Button)gvPagerRow.FindControl("btnLast");



            btnFirst.Enabled = btnPrevious.Enabled = (pageIndex != 0);

            btnNext.Enabled = btnLast.Enabled = (pageIndex < (pageCount - 1));



            DropDownList ddlPageSelector = (DropDownList)gvPagerRow.FindControl("ddlPageSelector");

            ddlPageSelector.Items.Clear();

            for (int i = 1; i <= gridView.PageCount; i++)
            {

                ddlPageSelector.Items.Add(i.ToString());

            }



            ddlPageSelector.SelectedIndex = pageIndex;



            //Anonymous method (see another way to do this at the bottom)

            //ddlPageSelector.SelectedIndexChanged += delegate
            //{

            //    gridView.PageIndex = ddlPageSelector.SelectedIndex;

            //    gridView.DataBind();

            //};



        }

        public DataTable GetTaskCount(String UserID, string whereCond, string Reminder, ref string paramWhereCondition, string companyID)
        {
            string strTaskStatus = "";
            string strWhereCon = "";
            string strAssignTo = " ta.assignedto=" + UserID;


            if (whereCond == "All")
            {
                strWhereCon = "" + strAssignTo;
            }
            else if (whereCond == "Archive")
            {
                strWhereCon = " (ta.Archive=1) and " + strAssignTo;
                strTaskStatus = "Archieve";
            }
            else if (whereCond == "AssignByMe")
            {
                strWhereCon = " t.createdby=" + UserID;
                strAssignTo = "";
            }
            else if (whereCond == "Active")
            {
                strWhereCon = " (ta.Archive=0 or ta.Archive is null) and " + strAssignTo;
            }

            paramWhereCondition = strWhereCon;


            


            if (whereCond == "Archive")
            {
                strSQL = @" SELECT COUNT(Task) Task FROM(
                        Select distinct Task,DueDate,AssignedBy,
                        AssignedTo,Status,AutoID,AssignedToID,AssignedByID,TaskID,FullTask,PageURL,JobID,Reminder
                        from
                        (                        
                        Select distinct t.task Task,isnull(convert(varchar,ta.DueDate,103),'Nil') DueDate
                        ,isnull(ui.UserName,'Nil') AssignedBy
                        ,isnull(ui1.UserName,'Nil') AssignedTo
                        ,Status=CASE isnull(ta.Done,0) WHEN 1 THEN 'Done' WHEN 0 THEN 'Incomplete' END,isnull(ta.AutoID,0) AutoID,ta.AssignedTo as AssignedToID
                        ,isnull(ta.AssignedBy,t.createdby) AssignedByID,t.TaskID,t.task as FullTask
                        ,isnull(t.PageURL,'') PageURL,isnull(t.Jobno,'') JobID,isnull(t.Reminder,0) Reminder,ta.Done,ta.Archive
                        From TB_TaskAssign ta
                        left join TB_PrmTask t on t.taskid=ta.taskid                        
                        left join TB_PrmUserInfo ui on ta.assignedby=ui.userid
                        left join TB_PrmUserInfo ui1 on ta.assignedto=ui1.userid
                        
                        where " + strWhereCon + @") tb1 " + Reminder + @" Union 
                        Select distinct Task,DueDate,AssignedBy,
                        AssignedTo,Status,AutoID,AssignedToID,AssignedByID,TaskID,FullTask,PageURL,JobID,Reminder
                        from
                        (                        
                        Select distinct t.task Task,isnull(convert(varchar,ta.DueDate,103),'') DueDate
                        ,'Self' AssignedBy
                        ,'' AssignedTo
                        ,'' Status,0 AutoID,'' AssignedToID
                        ,t.createdby AssignedByID,t.TaskID,t.task as FullTask
                        ,isnull(t.PageURL,'') PageURL,isnull(t.Jobno,'') JobID,isnull(t.Reminder,0) Reminder,'' Done,'' Archive
                        From TB_PrmTask t
                        left join TB_TaskAssign ta on t.taskid=ta.taskid
                        left join TB_PrmUserInfo ui on t.createdby=ui.empautoid
                        
                        where t.createdby=" + UserID + @" and t.task is not null and t.Status='Archive') tb1  
                        ) tblMain where tblMain.Status<>'Done'
                        and tblMain.Reminder=1";

            }
            else
            {
                strSQL = @" Select COUNT(Task) Task 
                        from
                        (                        
                        Select distinct t.task Task,isnull(convert(varchar,ta.DueDate,103),'Nil') DueDate
                        ,isnull(ui.UserName,'Nil') AssignedBy
                        ,isnull(ui1.UserName,'Nil') AssignedTo
                        ,Status=CASE isnull(ta.Done,0) WHEN 1 THEN 'Done' WHEN 0 THEN 'Incomplete' END,isnull(ta.AutoID,0) AutoID,ta.AssignedTo as AssignedToID
                        ,isnull(ta.AssignedBy,t.createdby) AssignedByID,t.TaskID,t.task as FullTask
                        ,isnull(t.PageURL,'') PageURL,isnull(t.Jobno,'') JobID,isnull(t.Reminder,0) Reminder,ta.Done,ta.Archive
                        From TB_TaskAssign ta
                        left join TB_PrmTask t on t.taskid=ta.taskid                        
                        left join TB_PrmUserInfo ui on ta.assignedby=ui.userid
                        left join TB_PrmUserInfo ui1 on ta.assignedto=ui1.userid
                        
                        where " + strWhereCon + @") tb1 " + Reminder + @" 
                        where tb1.Status<>'Done'
                        and tb1.Reminder=1";
            }

            #region ==============Rough Code=================
            

            #endregion


            return this.ExecuteSQLStringDataTable(strSQL);
        }

    }
}