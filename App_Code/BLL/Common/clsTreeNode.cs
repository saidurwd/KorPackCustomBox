using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.SqlClient;


/// <summary>
/// Summary description for clsLogin
/// </summary>
namespace Blumen
{
    public class clsTreeNode : DAL.BaseClass
    {
        
        public string ParentItemImage;
        public  string ChildItemImage;
        public  string AddItemImage;
        public string OtherItemImage;
        public  string PageType;
        private int intPosting;

        public clsTreeNode(): base(ConfigurationManager.AppSettings["Cnn"])
        {
           


        }
      
        public void PopulateRootLevel(string strSQL,TreeView tv)
        {
        SqlConnection sqlcon = new SqlConnection();
        sqlcon.ConnectionString = ConfigurationManager.AppSettings["Cnn"].ToString();
        sqlcon.Open();
            //convert(varchar,ParentID)+'ô'+convert(varchar,CatID)
        //strSQL=  "select "+ID+","+Name+",(select count(*) FROM "+TableName+" "
                 //+ " WHERE " + ParentIDName + "=sc." + ID + ") childnodecount FROM " + TableName + " sc where " + ParentIDName + "=0";
        //strSQL = "select convert(varchar," + ParentIDName + ")+'ô'+convert(varchar," + ID + ")" + ID + " ," + Name + ", (select count(*) FROM " + TableName + " "
         //    + " WHERE " + ParentIDName + "=sc." + ID + ") childnodecount FROM " + TableName + " sc where " + ParentIDName + "=0";

        SqlCommand sqlcmd = new SqlCommand(strSQL, sqlcon);
        SqlDataAdapter sqlda=new SqlDataAdapter(sqlcmd);
        DataTable sqldt=new DataTable();
        sqlda.Fill(sqldt);
        PopulateNodes(sqldt,tv.Nodes);//Generate The node by the Help of ParentID
        //tv.NodeIndent = 10;
        //sqlcon.Close();
     }


        private void PopulateNodes(DataTable dts, TreeNodeCollection nodes)
         {

            foreach (DataRow dr in dts.Rows)
            {

                TreeNode tn = new TreeNode();
                tn.Text = dr["NameDesc"].ToString();
                tn.Value = dr["ID"].ToString();
                //tn.PopulateOnDemand = (Int32.Parse(dr["childnodecount"].ToString()) > 0); //|| (isRootNode==0);
                if ((PageType == "WithAddItem") && (!tn.Value.Contains("ò")))
                {
                    tn.PopulateOnDemand = true;//works for any node other than data node
                }
                else if ((PageType == "WithAddItemCreateFolder") && (!tn.Value.Contains("ò")))
                {
                    tn.PopulateOnDemand = false;//works for any node other than data node
                }

                else if (PageType == "NoCheckBoxNoLink")
                {
                    tn.NavigateUrl = "#";                    
                    tn.PopulateOnDemand = true;//When no "Add Item" or "Add Catagory" Allowed
                }

                else if (PageType == "PostingCheck")
                {
                    string[] arrIDPosting = tn.Value.Split('ô');

                    if (arrIDPosting.Length > 1)
                    {
                        if (arrIDPosting[1].ToString() == "1")
                        {
                            tn.NavigateUrl = "#";
                            tn.PopulateOnDemand = false;//When no "Add Item" or "Add Catagory" Allowed
                            tn.ShowCheckBox = true;
                        }
                        else
                        {
                            tn.NavigateUrl = "#";
                            tn.PopulateOnDemand = true;//When no "Add Item" or "Add Catagory" Allowed
                        }
                    }
                    else
                    {
                        tn.NavigateUrl = "#";
                        tn.PopulateOnDemand = true;//When no "Add Item" or "Add Catagory" Allowed
                    }
                }

                else if (PageType == "MainHeadCheckBox")
                {
                    string[] arrIDPosting = tn.Value.Split('ô');

                    if (arrIDPosting.Length > 1)
                    {
                        if (arrIDPosting[1].ToString() == "0")
                        {
                            tn.NavigateUrl = "#";
                            tn.PopulateOnDemand = true;//When no "Add Item" or "Add Catagory" Allowed
                            tn.ShowCheckBox = true;
                        }
                        else
                        {
                            tn.NavigateUrl = "#";
                            tn.PopulateOnDemand = false;//When no "Add Item" or "Add Catagory" Allowed
                        }
                    }
                    else
                    {
                        tn.NavigateUrl = "#";
                        tn.PopulateOnDemand = true;//When no "Add Item" or "Add Catagory" Allowed
                    }
                }
                else if (PageType == "NoNavigation")
                {
                    string[] arrIDPosting = tn.Value.Split('ò');
                    if (arrIDPosting.Length > 1)
                    {

                        tn.NavigateUrl = "#";
                        tn.ShowCheckBox = true;
                        tn.PopulateOnDemand = false;//When no "Add Item" or "Add Catagory" Allowed
                        tn.ShowCheckBox = true;

                    }
                    else
                    {
                        tn.NavigateUrl = "#";
                        tn.ShowCheckBox = false;
                        tn.PopulateOnDemand = true;//When no "Add Item" or "Add Catagory" Allowed
                    }
                }
                else if ((PageType == "AddNotAllow") && (!tn.Value.Contains("ò")))
                {
                    tn.PopulateOnDemand = true;//When no "Add Item" or "Add Catagory" Allowed
                }
                else
                {
                    tn.PopulateOnDemand = (Int32.Parse(dr["childnodecount"].ToString()) > 0);
                }


                if (Int32.Parse(dr["childnodecount"].ToString()) > 0)
                {
                    tn.ImageUrl = this.ParentItemImage;
                }
                else if ((PageType == "WithAnotherTableVal") || (tn.Value.Contains("ò")))
                {
                    tn.ImageUrl = this.OtherItemImage;
                }

                else
                {
                    tn.ImageUrl = this.ChildItemImage;
                }

                if (PageType == "NoNavigation")
                {
                    string[] arrIDPosting = tn.Value.Split('ò');
                    if (arrIDPosting.Length > 1)
                    {

                        tn.NavigateUrl = "#";
                        tn.ShowCheckBox = true;
                        tn.PopulateOnDemand = false;//When no "Add Item" or "Add Catagory" Allowed
                        tn.ShowCheckBox = true;

                    }
                    else
                    {
                        tn.NavigateUrl = "#";
                        tn.ShowCheckBox = false;
                        tn.PopulateOnDemand = true;//When no "Add Item" or "Add Catagory" Allowed
                    }
                }

                if (PageType == "PostingCheck")
                {
                    string[] arrIDPosting = tn.Value.Split('ô');
                    if (arrIDPosting.Length > 1)
                    {
                        if (arrIDPosting[1].ToString() == "1")
                        {
                            tn.NavigateUrl = "#";
                            tn.PopulateOnDemand = false;//When no "Add Item" or "Add Catagory" Allowed
                            tn.ShowCheckBox = true;
                        }
                        else
                        {
                            tn.NavigateUrl = "#";
                            tn.PopulateOnDemand = true;//When no "Add Item" or "Add Catagory" Allowed
                        }
                    }
                    else
                    {
                        tn.NavigateUrl = "#";
                        tn.PopulateOnDemand = true;//When no "Add Item" or "Add Catagory" Allowed
                    }
                }


                if (PageType == "NoCheckBoxNoLink")
                {
                    tn.NavigateUrl = "#";
                    tn.PopulateOnDemand = true;//When no "Add Item" or "Add Catagory" Allowed
                }

                nodes.Add(tn);

                if (PageType == "AddJavaScriptFunction")
                {
                    //[0]-AccID
                    //[1]-AccNo
                    //[2]-AccHead
                    //[3]-Posting
                    //[4]-OpenBalance
                    //[5]-AccTypeID
                    //[6]-ParentID
                    //[7]-HeadLevel


                    string[] strArray = dr["NameDesc"].ToString().Split('ö');

                    if (strArray.Length > 1)
                    {
                        // LevelType 1 Means "Add Sub" Level
                        tn.Text = strArray[2];
                        tn.Value = dr["NameDesc"].ToString();
                        string url = "javascript:SetSelectedNodeValue('" + dr["NameDesc"].ToString().Replace("'", "`") + "','" + dr["NameDesc"].ToString().Replace("'", "`") + "','" + strArray[2].Replace("'","`") + "',1)";
                        tn.NavigateUrl = url;
                        intPosting = int.Parse(strArray[3]);
                    }
                    if (intPosting == 1)
                    {
                        tn.PopulateOnDemand = false;
                    }
                }



            }


        }
        public void PopulateSubLevel(string strSQL,int ParentID,TreeNode ParentNode,string ParentIDText,TreeView tv)
    {
        SqlConnection sqlcon = new SqlConnection();
        sqlcon.ConnectionString = ConfigurationManager.AppSettings["Cnn"].ToString();
        sqlcon.Open();
        //strSQL = "select convert(varchar,"+ParentIDName+")+'ô'+convert(varchar,"+ID+")"+ID+" ,"+Name+", (select count(*) FROM " + TableName + " "
                 //+ " WHERE " + ParentIDName + "=sc." + ID + ") childnodecount FROM " + TableName + " sc where " + ParentIDName + "=@ParentID";

        //strSQL = "select " + ID + "," + Name + ",(select count(*) FROM " + TableName + " "
                //+ " WHERE " + ParentIDName + "=sc." + ID + ") childnodecount FROM " + TableName + " sc where " + ParentIDName + "=@ParentID";
        
        SqlCommand sqlcmd = new SqlCommand(strSQL, sqlcon);
        //sqlcmd.Parameters.Add("@ParentID",SqlDbType.BigInt).Value=ParentID;
        SqlDataAdapter sqlda=new SqlDataAdapter(sqlcmd);
        DataTable sqldt=new DataTable();      
        sqlda.Fill(sqldt);
       
        if (PageType=="WithAddItem")
        {
          
                TreeNode tn1 = new TreeNode();
                tn1.Text = ParentIDText;
                tn1.Value = ParentID.ToString() + "ô" + "0";
                tn1.Value = ParentID.ToString();
                tn1.PopulateOnDemand = false;
                tn1.ImageUrl = this.AddItemImage;
                ParentNode.ChildNodes.Add(tn1);

                TreeNode tn3 = new TreeNode();
                tn3.Text = "Edit Category";
                tn3.Value = "EditCategory";
                tn3.PopulateOnDemand = false;
                tn3.ImageUrl = this.AddItemImage;
                ParentNode.ChildNodes.Add(tn3);

                TreeNode tn2 = new TreeNode();
                tn2.Text = "Delete Category";
                tn2.Value = "DelCategory";                
                tn2.PopulateOnDemand = false;
                tn2.ImageUrl = this.AddItemImage;
                ParentNode.ChildNodes.Add(tn2);
            
        }

        if (PageType == "WithAddAndDeleteItem")
        {

            TreeNode tn1 = new TreeNode();
            tn1.Text = "Add Sub Category";
            tn1.Value = ParentID.ToString() + "ô" + "0";
            tn1.Value = ParentID.ToString();
            tn1.PopulateOnDemand = false;
            tn1.ImageUrl = this.AddItemImage;
            ParentNode.ChildNodes.Add(tn1);                
            PageType = "WithAddAndDeleteItem";


        }

        if (PageType == "WithAddAndDeleteItem")
        {

            TreeNode tn2 = new TreeNode();
            tn2.Text = "Delete Category Head";         
            tn2.Value = ParentID.ToString();
            tn2.PopulateOnDemand = false;
            tn2.ImageUrl = this.AddItemImage;          
            ParentNode.ChildNodes.Add(tn2);
            //tv.Nodes.Add(tn2);
            PageType = "WithAddItem";

        }

        if (PageType == "AddJavaScriptFunction")
        {
            string[] strArray = ParentNode.Value.Split('ö');
            if (strArray.Length > 1) intPosting = int.Parse(strArray[3]); else intPosting = 0;
            if (ParentNode.Text != "Chart of Account")
            {
                if (intPosting == 0)
                {
                    TreeNode tn1 = new TreeNode();
                    tn1.Text = ParentIDText;
                    // tn1.Value = ParentID.ToString() + "ô" + "0";
                    // LevelType 0 Means not "Add Sub"
                    string url = "javascript:SetSelectedNodeValue('" + ParentNode.Value.Replace("'", "`") + "','" + ParentID.ToString() + "','" + ParentNode.Text.Replace("'", "`") + "',0)";
                    tn1.NavigateUrl = url;

                    tn1.Value = ParentID.ToString();
                    tn1.PopulateOnDemand = false;
                    tn1.ImageUrl = this.AddItemImage;
                    ParentNode.ChildNodes.Add(tn1);
                }
            }
        }
        

        if (sqldt.Rows.Count > 0)
        {
            PopulateNodes(sqldt, ParentNode.ChildNodes);
        }
        sqlcon.Close();

    }


    }
}
