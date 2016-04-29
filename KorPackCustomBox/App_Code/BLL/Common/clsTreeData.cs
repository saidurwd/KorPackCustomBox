//*****************************************
// Author : Faruk Ahmed
// Development Date : 31 October 2006
// Modification Date: 1st November 2006
// Module : Service
//*****************************************

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

/// <summary>
/// Summary description for clsTreeData
/// </summary>

namespace Blumen
{
    public class clsTreeData : DAL.BaseClass
    {
        private String strSQL;
        private String strSelect;
        private String strWhere;

        #region Constructor
        public clsTreeData()
            : base(ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString)
        {
            strSQL = "";
            strWhere = "";
            strSelect = "";
        }
        #endregion


        public void FillTree(TreeView tvwTemp, String strTblName, String strFieldID, String strFieldName, String strFieldDesc, String strWhereCond, String strOrderBy, String strOrderAscDesc, string strCompanyID)
        {
            strSelect = "Select " + strFieldID + " ParentID," + strFieldName + " ParentName ," + strFieldDesc + " ParentDesc From " + strTblName;
            tvwTemp.Nodes.Clear();
            if (strWhereCond.Length > 0)
            {
                strWhere = " Where " + strWhereCond;
                strSQL = strSelect + strWhere;
            }
            else { strSQL = strSelect; }
            if (strOrderBy.Length > 0)
            {
                strSQL = strSQL + " Order By " + strOrderBy + " " + strOrderAscDesc;
            }

            string[,] ParentNode = new string[100, 3];
            string[,] ChildNode = new string[40000, 3];
            string[,] ChildNode1 = new string[4000, 3];
            string[,] ChildNode2 = new string[4000, 3];
            string[,] ChildNode3 = new string[4000, 3];
            string[,] ChildNode4 = new string[4000, 3];
            string[,] ChildNode5 = new string[4000, 3];
            string[,] ChildNode6 = new string[4000, 3];
            string[,] ChildNode7 = new string[4000, 3];

            int count = 0;

            #region TempConnection

            SqlConnection sqlcon = new SqlConnection();
            sqlcon.ConnectionString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            sqlcon.Open();

            SqlCommand sqlcmd = new SqlCommand(strSQL, sqlcon);
            SqlDataReader Sdr;

            Sdr = sqlcmd.ExecuteReader();

            while (Sdr.Read())
            {
                ParentNode[count, 0] = Sdr.GetValue(Sdr.GetOrdinal("ParentID")).ToString();
                ParentNode[count, 1] = Sdr.GetValue(Sdr.GetOrdinal("ParentName")).ToString();
                ParentNode[count++, 2] = Sdr.GetValue(Sdr.GetOrdinal("ParentDesc")).ToString();
            }

            Sdr.Close();

            #endregion

            #region ParentLoop

            //ROOT LOOP
            for (int loop = 0; loop < count; loop++)
            {
                TreeNode root = new TreeNode();
                root.Text = ParentNode[loop, 1];
                root.NavigateUrl = "#";
                //root.ImageUrl=("~/App_Themes/SkinFile/images/oPower.ico");
                root.Value = ParentNode[loop, 2];
                root.Expand();

                String strSQLTemp = "";
                strSQLTemp = strSelect + " Where company_id='" + strCompanyID  + "' and ParentID = " + ParentNode[loop, 0];

                SqlCommand Module_SqlCmd = new SqlCommand(strSQLTemp, sqlcon);
                SqlDataReader Module_Sdr = Module_SqlCmd.ExecuteReader();

                int icount = 0;
                while (Module_Sdr.Read())
                {
                    ChildNode[icount, 0] = Module_Sdr.GetValue(Module_Sdr.GetOrdinal("ParentID")).ToString();
                    ChildNode[icount, 1] = Module_Sdr.GetValue(Module_Sdr.GetOrdinal("ParentName")).ToString();
                    ChildNode[icount++, 2] = Module_Sdr.GetValue(Module_Sdr.GetOrdinal("ParentDesc")).ToString();
                }
                Module_Sdr.Close();
                int intChildID;

                #region ChildLoop
                int iloop = 0;
                {
                    if (ChildNode[iloop, 0] == null)
                    {
                        intChildID = 0;
                    }
                    else
                    {
                        intChildID = int.Parse((ChildNode[iloop, 0]));
                    }
                    // intChildID = int.Parse((ChildNode[iloop, 0]));     //Chidl ID

                    int intLoop;
                    #region LeafLoop

                    for (intLoop = 0; intLoop < icount; intLoop++)
                    {
                        TreeNode child = new TreeNode();

                        intChildID = int.Parse(ChildNode[intLoop, 0]);
                        child.Text = ChildNode[intLoop, 1];                //Child Name
                        child.NavigateUrl = "#";
                        //child.ImageUrl = "~/App_Themes/SkinFile/images/add1.gif";
                        child.Value = ChildNode[intLoop, 2];
                        child.Collapse();               //Child Name
                        root.ChildNodes.Add(child);
                        {
                            strSQLTemp = strSelect + " Where  company_id='" + strCompanyID + "' and  ParentID = " + intChildID;

                            SqlCommand Leaf_SqlCmd = new SqlCommand(strSQLTemp, sqlcon);
                            SqlDataReader Leaf_Sdr = Leaf_SqlCmd.ExecuteReader();

                            int jcount = 0;
                            while (Leaf_Sdr.Read())
                            {
                                ChildNode1[jcount, 0] = Leaf_Sdr.GetValue(Leaf_Sdr.GetOrdinal("ParentID")).ToString();
                                ChildNode1[jcount, 1] = Leaf_Sdr.GetValue(Leaf_Sdr.GetOrdinal("ParentName")).ToString();
                                ChildNode1[jcount++, 2] = Leaf_Sdr.GetValue(Leaf_Sdr.GetOrdinal("ParentDesc")).ToString();
                            }
                            Leaf_Sdr.Close();
                            #region For1
                            int intChild = 0; int jloop = 0;
                            if (jcount > 0)
                            {
                                intChild = int.Parse(ChildNode1[jloop, 0]);

                                int kLoop = 0;
                                #region For2
                                for (kLoop = 0; kLoop < jcount; kLoop++)
                                {
                                    TreeNode leaf = new TreeNode();
                                    intChild = int.Parse(ChildNode1[kLoop, 0]);
                                    leaf.Text = ChildNode1[kLoop, 1];
                                    leaf.NavigateUrl = "#";
                                    leaf.ImageUrl = "~/App_Themes/SkinFile/images/Note006.gif";
                                    leaf.Value = ChildNode1[kLoop, 2];
                                    leaf.Collapse();
                                    child.ChildNodes.Add(leaf);
                                    {
                                        strSQLTemp = strSelect + " Where company_id='" + strCompanyID + "' and ParentID = " + intChild;

                                        SqlCommand Leaf_SqlCmd1 = new SqlCommand(strSQLTemp, sqlcon);
                                        SqlDataReader Leaf_Sdr1 = Leaf_SqlCmd1.ExecuteReader();

                                        int kcount = 0;
                                        while (Leaf_Sdr1.Read())
                                        {
                                            ChildNode2[kcount, 0] = Leaf_Sdr1.GetValue(Leaf_Sdr1.GetOrdinal("ParentID")).ToString();
                                            ChildNode2[kcount, 1] = Leaf_Sdr1.GetValue(Leaf_Sdr1.GetOrdinal("ParentName")).ToString();
                                            ChildNode2[kcount++, 2] = Leaf_Sdr1.GetValue(Leaf_Sdr1.GetOrdinal("ParentDesc")).ToString();
                                        }
                                        Leaf_Sdr1.Close();

                                        #region For3

                                        int intLeaf1ID = 0;
                                        //    int lloop = 0;

                                        {
                                            int mLoop = 0;
                                            #region For4
                                            for (mLoop = 0; mLoop < kcount; mLoop++)
                                            {
                                                TreeNode leaf1 = new TreeNode();
                                                intLeaf1ID = int.Parse(ChildNode2[mLoop, 0]);
                                                leaf1.Text = ChildNode2[mLoop, 1];
                                                leaf1.NavigateUrl = "#";
                                                leaf1.ImageUrl = "~/App_Themes/SkinFile/images/edit1.gif";
                                                leaf1.Value = ChildNode2[mLoop, 2];
                                                leaf1.Collapse();
                                                leaf.ChildNodes.Add(leaf1);
                                                {
                                                    strSQLTemp = strSelect + " Where company_id='" + strCompanyID + "' and ParentID = " + intLeaf1ID;

                                                    SqlCommand Leaf_SqlCmd2 = new SqlCommand(strSQLTemp, sqlcon);
                                                    SqlDataReader Leaf_Sdr2 = Leaf_SqlCmd2.ExecuteReader();

                                                    int ncount = 0;

                                                    while (Leaf_Sdr2.Read())
                                                    {
                                                        ChildNode3[ncount, 0] = Leaf_Sdr2.GetValue(Leaf_Sdr2.GetOrdinal("ParentID")).ToString();
                                                        ChildNode3[ncount, 1] = Leaf_Sdr2.GetValue(Leaf_Sdr2.GetOrdinal("ParentName")).ToString();
                                                        ChildNode3[ncount++, 2] = Leaf_Sdr2.GetValue(Leaf_Sdr2.GetOrdinal("ParentDesc")).ToString();
                                                    }
                                                    Leaf_Sdr2.Close();
                                                    #region For5
                                                    int intLeaf2ID = 0;
                                                    for (int nloop = 0; nloop < ncount; nloop++)
                                                    {
                                                        TreeNode leaf2 = new TreeNode();
                                                        intLeaf2ID = int.Parse(ChildNode3[nloop, 0]);
                                                        leaf2.Text = ChildNode3[nloop, 1];
                                                        leaf2.NavigateUrl = "#";
                                                        leaf2.ImageUrl = "~/App_Themes/SkinFile/images/edit1.gif";
                                                        leaf2.Value = ChildNode3[nloop, 2];
                                                        leaf1.ChildNodes.Add(leaf2);


                                                        strSQLTemp = strSelect + " Where company_id='" + strCompanyID + "' and ParentID = " + intLeaf2ID;

                                                        SqlCommand Leaf_SqlCmd3 = new SqlCommand(strSQLTemp, sqlcon);
                                                        SqlDataReader Leaf_Sdr3 = Leaf_SqlCmd3.ExecuteReader();

                                                        int Pcount = 0;
                                                        while (Leaf_Sdr3.Read())
                                                        {
                                                            ChildNode4[Pcount, 0] = Leaf_Sdr3.GetValue(Leaf_Sdr3.GetOrdinal("ParentID")).ToString();
                                                            ChildNode4[Pcount, 1] = Leaf_Sdr3.GetValue(Leaf_Sdr3.GetOrdinal("ParentName")).ToString();
                                                            ChildNode4[Pcount++, 2] = Leaf_Sdr3.GetValue(Leaf_Sdr3.GetOrdinal("ParentDesc")).ToString();
                                                        }
                                                        Leaf_Sdr3.Close();
                                                        #region For6
                                                        int intLeaf3ID = 0;
                                                        for (int ploop = 0; ploop < Pcount; ploop++)
                                                        {
                                                            TreeNode leaf3 = new TreeNode();
                                                            intLeaf3ID = int.Parse(ChildNode4[ploop, 0]);
                                                            leaf3.Text = ChildNode4[ploop, 1];
                                                            leaf3.NavigateUrl = "#";
                                                            leaf3.ImageUrl = "~/App_Themes/SkinFile/images/edit1.gif";
                                                            leaf3.Value = ChildNode4[ploop, 2];
                                                            leaf2.ChildNodes.Add(leaf3);


                                                            //==========================================

                                                            strSQLTemp = strSelect + " Where company_id='" + strCompanyID + "' and ParentID = " + intLeaf3ID;

                                                            SqlCommand Leaf_SqlCmd4 = new SqlCommand(strSQLTemp, sqlcon);
                                                            SqlDataReader Leaf_Sdr4 = Leaf_SqlCmd4.ExecuteReader();

                                                            int Wcount = 0;
                                                            while (Leaf_Sdr4.Read())
                                                            {
                                                                ChildNode5[Wcount, 0] = Leaf_Sdr4.GetValue(Leaf_Sdr4.GetOrdinal("ParentID")).ToString();
                                                                ChildNode5[Wcount, 1] = Leaf_Sdr4.GetValue(Leaf_Sdr4.GetOrdinal("ParentName")).ToString();
                                                                ChildNode5[Wcount++, 2] = Leaf_Sdr4.GetValue(Leaf_Sdr4.GetOrdinal("ParentDesc")).ToString();
                                                            }
                                                            Leaf_Sdr4.Close();
                                                            #region For7
                                                            int intLeaf4ID = 0;
                                                            for (int wloop = 0; wloop < Wcount; wloop++)
                                                            {
                                                                TreeNode leaf4 = new TreeNode();
                                                                intLeaf4ID = int.Parse(ChildNode5[wloop, 0]);
                                                                leaf4.Text = ChildNode5[wloop, 1];
                                                                leaf4.NavigateUrl = "#";
                                                                leaf4.ImageUrl = "~/App_Themes/SkinFile/images/add1.gif";
                                                                leaf4.Value = ChildNode5[wloop, 2];
                                                                leaf3.ChildNodes.Add(leaf4);

                                                                //=========================================================

                                                                strSQLTemp = strSelect + " Where company_id='" + strCompanyID + "' and ParentID = " + intLeaf4ID;

                                                                SqlCommand Leaf_SqlCmd5 = new SqlCommand(strSQLTemp, sqlcon);
                                                                SqlDataReader Leaf_Sdr5 = Leaf_SqlCmd5.ExecuteReader();

                                                                int rowCount = 0;
                                                                while (Leaf_Sdr5.Read())
                                                                {
                                                                    ChildNode6[rowCount, 0] = Leaf_Sdr5.GetValue(Leaf_Sdr5.GetOrdinal("ParentID")).ToString();
                                                                    ChildNode6[rowCount, 1] = Leaf_Sdr5.GetValue(Leaf_Sdr5.GetOrdinal("ParentName")).ToString();
                                                                    ChildNode6[rowCount++, 2] = Leaf_Sdr5.GetValue(Leaf_Sdr5.GetOrdinal("ParentDesc")).ToString();
                                                                }
                                                                Leaf_Sdr5.Close();
                                                                #region For8
                                                                int intLeaf5ID = 0;
                                                                for (int wiloop = 0; wiloop < rowCount; wiloop++)
                                                                {
                                                                    TreeNode leaf5 = new TreeNode();
                                                                    intLeaf5ID = int.Parse(ChildNode6[wiloop, 0]);
                                                                    leaf5.Text = ChildNode6[wiloop, 1];
                                                                    leaf5.NavigateUrl = "#";
                                                                    leaf5.ImageUrl = "~/App_Themes/SkinFile/images/add1.gif";
                                                                    leaf5.Value = ChildNode6[wiloop, 2];
                                                                    leaf4.ChildNodes.Add(leaf5);
                                                                }
                                                                #endregion


                                                            }
                                                            #endregion
                                                        #endregion


                                                            //==========================================



                                                        }
                                                    #endregion

                                                    }
                                            #endregion
                                                    // Leaf_Sdr2.Close();
                                                }//END OF LAST DEPTH 5TH
                                            }//END OF FOR
                                        #endregion
                                        }
                                #endregion
                                        // Leaf_Sdr1.Close();
                                    }//END OF FIRST IF
                                }//END OF FOR
                            #endregion
                            }
                    #endregion

                            Leaf_Sdr.Close();
                        }//END OF FIRST IF
                    }//END OF FOR
                #endregion

                }
            #endregion

                Module_Sdr.Close();
                tvwTemp.Nodes.Add(root);
                tvwTemp.NodeIndent = 30;
            }
            //  #endregion

            sqlcon.Close();
        }
       
    }
}
