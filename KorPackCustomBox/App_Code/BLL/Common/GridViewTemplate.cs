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
/// Summary description for GridViewTemplate
/// </summary>
public class GridViewTemplate: ITemplate
{
    static public int i=0,j=0,k=0,iIndex=0;

    string[] strAll = null;
    int val, rval;
	public GridViewTemplate()
	{
    }
     //A variable to hold the type of ListItemType.
    ListItemType _templateType;

    //A variable to hold the column name.
    string _columnName;

    //Constructor where we define the template type and column name.
    public GridViewTemplate(ListItemType type, string colname)
    {
        //Stores the template type.
       // i = 0; j = 0; k = 0; iIndex = 0;
        _templateType = type;
        strAll = colname.Split('-');
        //Stores the column name.
        
        _columnName = colname;
    }

 

    void ITemplate.InstantiateIn(System.Web.UI.Control container)
    {
        
        switch (_templateType)
        {
            case ListItemType.Header:
                val = i++;
               
               if (strAll[0] == "StudentName")
                {
                    Label lbl = new Label();            //Allocates the new label object.
                    lbl.Text = _columnName;             //Assigns the name of the column in the lable.
                    
                   //Adds the newly created label control to the container.
                    lbl.ID = "Col" + val.ToString();
                    lbl.Visible = true;
                    container.Controls.Add(lbl);
                }
                else if (strAll[0] == "Passed")
                {
                    Label lbl = new Label();            //Allocates the new label object.
                    lbl.Text = _columnName;             //Assigns the name of the column in the lable.
                    //Adds the newly created label control to the container.
                    lbl.ID = "ChkP" + val.ToString();
                    lbl.Visible = true;
                    container.Controls.Add(lbl);
                }
                else if (strAll[0] == "LabelID")
                {

                    HiddenField hdn2 = new HiddenField();            //Allocates the new label object.
                    //Adds the newly created label control to the container.
                    hdn2.ID = "hlblID" + val.ToString();
                    //iIndex = iIndex + 1;
                    //hdn2.ID = "hlblID" + iIndex.ToString();
                    container.Controls.Add(hdn2);
                }
                else if (strAll[0] != "StudentID")
                {
                    TextBox tb1 = new TextBox();            //Allocates the new TextBox object.
                    tb1.Text = _columnName;             //Assigns the name of the column in the lable.
                    tb1.ID = "Col" + val.ToString();
                    tb1.SkinID = "txtNormalGrd";
                    Unit u = new Unit("98%");
                    tb1.Width = u;
                    tb1.Height = 12;
                    tb1.Font.Size = 1;
                    
                    container.Controls.Add(tb1);        //Adds the newly created label control to the container.
              
                CheckBox chk = new CheckBox();            //Allocates the new label object.
                chk.ID = "Chk" + val.ToString();
                chk.Checked = true;
                container.Controls.Add(chk);        //Adds the newly created label control to the container.
                } break; 
            //case ListItemType.Header:
            //    //Creates a new label control and add it to the container.
            //    Label lbl = new Label();            //Allocates the new label object.
            //    lbl.Text = _columnName;             //Assigns the name of the column in the lable.
            //    container.Controls.Add(lbl);        //Adds the newly created label control to the container.
            //    break;

            case ListItemType.Item:
                //Creates a new text box control and add it to the container.
                rval = j++;
                if (strAll[0] == "StudentName")
                {
                    CheckBox chk1 = new CheckBox();            //Allocates the new label object.
                    chk1.DataBinding += new EventHandler(chk1_DataBinding);
                    chk1.ID = "Col" + rval.ToString();
                    chk1.Visible = false;
                    container.Controls.Add(chk1);
                    
                    Label lbl2 = new Label();            //Allocates the new label object.
                    lbl2.DataBinding += new EventHandler(lbl2_DataBinding); 
                    lbl2.ID = "lbl" + rval.ToString();
                    
                    lbl2.Visible =true;
                    container.Controls.Add(lbl2);
                }
                else if (strAll[0] == "Passed")
                {
                    CheckBox chk2 = new CheckBox();            //Allocates the new label object.
                    chk2.DataBinding += new EventHandler(chk2_DataBinding);
                    chk2.ID = "ChkP" + rval.ToString();
                    container.Controls.Add(chk2);
                }
                else if (strAll[0] == "StudentID")
                {
                    HiddenField hdn1 = new HiddenField();            //Allocates the new label object.
                    hdn1.DataBinding += new EventHandler(hdn1_DataBinding);
                    //Adds the newly created label control to the container.
                    hdn1.ID = "hdn" + rval.ToString();
                    container.Controls.Add(hdn1);
                }
                else if (strAll[0] == "LabelID")
                {
                    HiddenField hdn2 = new HiddenField();            //Allocates the new label object.
                    hdn2.DataBinding += new EventHandler(hdn2_DataBinding);
                    //Adds the newly created label control to the container.
                    hdn2.ID = "hlblID" + rval.ToString();
                    //hdn2.ID = "hlblID" + iIndex.ToString();
                    container.Controls.Add(hdn2);
                }
                else if (strAll[0] != "StudentID")
                {
                    TextBox tb2 = new TextBox();                            //Allocates the new text box object.
                    tb2.DataBinding += new EventHandler(tb2_DataBinding);   //Attaches the data binding event.
                    tb2.ID = "Col" + rval.ToString();
                    tb2.SkinID = "txtNormal";
                    tb2.Columns = 9;                                        //Creates a column with size 4.
                    tb2.Height =12;
                    tb2.Font.Size = 3;
                    //iIndex = iIndex - 1;
                    container.Controls.Add(tb2);                            //Adds the newly created textbox to the container.
                }
                break;

            case ListItemType.EditItem:
                //As, I am not using any EditItem, I didnot added any code here.
                break;
            case ListItemType.Footer:
                CheckBox chkColumn = new CheckBox();
                chkColumn.ID = "Chk" + _columnName;
                container.Controls.Add(chkColumn);
                break;
        }

    }

 

    /// <summary>

    /// This is the event, which will be raised when the binding happens.

    /// </summary>

    /// <param name="sender"></param>

    /// <param name="e"></param>

    void tb1_DataBinding(object sender, EventArgs e)

    {

        TextBox txtdata = (TextBox)sender;

        GridViewRow container = (GridViewRow)txtdata.NamingContainer;

        object dataValue = DataBinder.Eval(container.DataItem, _columnName);

        if (dataValue != DBNull.Value)

        {

            txtdata.Text = dataValue.ToString();

        }

    }


    void chk1_DataBinding(object sender, EventArgs e)
    {

        CheckBox txtdata = (CheckBox)sender;

        GridViewRow container = (GridViewRow)txtdata.NamingContainer;

        object dataValue = DataBinder.Eval(container.DataItem, _columnName);

        if (dataValue != DBNull.Value)
        {


            //txtdata.Checked = dataValue.ToString();

        }

    }

    void chk2_DataBinding(object sender, EventArgs e)
    {

        CheckBox txtdata = (CheckBox)sender;

        GridViewRow container = (GridViewRow)txtdata.NamingContainer;

        object dataValue = DataBinder.Eval(container.DataItem, _columnName);

        if (dataValue != DBNull.Value)
        {
            if (dataValue != null)
            {
                if (dataValue.ToString() == "1")
                {
                    txtdata.Checked = true;
                }
                else txtdata.Checked = false;
            }
        }
    }

    void tb2_DataBinding(object sender, EventArgs e)
    {

        TextBox txtdata = (TextBox)sender;

        GridViewRow container = (GridViewRow)txtdata.NamingContainer;

        object dataValue = DataBinder.Eval(container.DataItem, _columnName);

        if (dataValue != null)
        {
            if (dataValue.ToString() != "0")
            {
                txtdata.Text = dataValue.ToString();
            }
        }

    }

    void lbl2_DataBinding(object sender, EventArgs e)
    {

        Label txtdata = (Label)sender;
        GridViewRow container = (GridViewRow)txtdata.NamingContainer;

        object dataValue = DataBinder.Eval(container.DataItem, _columnName);
        
        if (dataValue != null)
        {
           txtdata.Text = dataValue.ToString();
        }
    }

    void hdn1_DataBinding(object sender, EventArgs e)
    {

        HiddenField txtdata = (HiddenField)sender;
        GridViewRow container = (GridViewRow)txtdata.NamingContainer;

        object dataValue = DataBinder.Eval(container.DataItem, _columnName);

        if (dataValue != null)
        {
            txtdata.Value= dataValue.ToString();
        }
    }

    void hdn2_DataBinding(object sender, EventArgs e)
    {

        HiddenField txtdata = (HiddenField)sender;
        GridViewRow container = (GridViewRow)txtdata.NamingContainer;

        object dataValue = DataBinder.Eval(container.DataItem, _columnName);

        if (dataValue != null)
        {
            txtdata.Value = dataValue.ToString();
        }
    }
}