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
/// Summary description for AppUtility
/// </summary>
public class AppUtility : DAL.BaseClass
{
    string strSql, strAppVersion, strDBVersion;
    public AppUtility()
        : base(ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString)
    {
        strSql = "";

        strAppVersion = "";
        strDBVersion = "";
    }
    public void UpdateApplication()
    {
        strAppVersion = Resources.Resource.AppVersion.ToString();
        strSql = "SELECT * FROM AppUtility";
        DataTable dt = this.ExecuteSQLStringDataTable(strSql);
        if (dt.Rows.Count != 0)
        {
            strDBVersion = dt.Rows[0]["DBVersion"].ToString();
            if (strAppVersion != strDBVersion)
            {
                if (strDBVersion == "1.99.99")
                {
                    version20000("2.00.00");
                    strDBVersion = "2.00.00";
                }
            }
        }
    }
    private void version20000(string strAppVersion)
    {
        object obj = null;
        
        strSql = @"exec dbo.usp_InsertNewMenu 'Genus', 101,'Inventory/Genus.aspx',1,NULL,318,'Inventory';";
        obj = this.ExecuteSQLStringScalar(strSql);
        
        strSql = @"exec dbo.usp_InsertNewMenu 'Sub Type', 101,'Inventory/SubType.aspx',2,NULL,317,'Inventory';";
        obj = this.ExecuteSQLStringScalar(strSql);
        
        strSql = @"exec dbo.usp_InsertNewMenu 'Variety', 101,'Inventory/ItemVariety.aspx',3,NULL,311,'Inventory';";
        obj = this.ExecuteSQLStringScalar(strSql);

        strSql = @"exec dbo.usp_InsertNewMenu 'Quality Grade', 101,'Inventory/Grade.aspx',4,NULL,316,'Inventory';";
        obj = this.ExecuteSQLStringScalar(strSql);
        
        strSql = @"exec dbo.usp_InsertNewMenu 'Unit Type', 101,'Inventory/UnitType.aspx',5,NULL,315,'Inventory';";
        obj = this.ExecuteSQLStringScalar(strSql);

        strSql = @"exec dbo.usp_InsertNewMenu 'Item Quality', 101,'Inventory/ItemQuality.aspx',6,NULL,314,'Inventory';";
        obj = this.ExecuteSQLStringScalar(strSql);
        
        strSql = @"exec dbo.usp_InsertNewMenu 'UOM', 101,'Inventory/UOM.aspx',7,NULL,92,'Inventory';";
        obj = this.ExecuteSQLStringScalar(strSql);
        
        strSql = @"exec dbo.usp_InsertNewMenu 'Item Type', 101,'Inventory/ItemType.aspx',8,NULL,313,'Inventory';";
        obj = this.ExecuteSQLStringScalar(strSql);
        
        strSql = @"exec dbo.usp_InsertNewMenu 'Brand', 101,'Inventory/ItemBrand.aspx',9,NULL,312,'Inventory';";
        obj = this.ExecuteSQLStringScalar(strSql);

        strSql = @"exec dbo.usp_InsertNewMenu 'Stock Item', 101,'Inventory/StockItem.aspx',10,NULL,95,'Inventory';";
        obj = this.ExecuteSQLStringScalar(strSql);

        strSql = @"exec dbo.usp_InsertNewMenu 'Location', 101,'Inventory/Location.aspx',10,NULL,320,'Inventory';";
        obj = this.ExecuteSQLStringScalar(strSql);

        strSql = @"exec dbo.usp_InsertNewMenu 'Manage Customer', 135,'Sales/Customer.aspx',1,NULL,238,'CRM';";
        obj = this.ExecuteSQLStringScalar(strSql);

        strSql = @"exec dbo.usp_InsertNewMenu 'Transaction Config.', 85,'Configuration/VoucherTypeList.aspx',11,NULL,220,'System Tools';";
        obj = this.ExecuteSQLStringScalar(strSql);

        strSql = @"exec dbo.usp_InsertNewMenu 'Sales Order', 136,'Sales/SalesOrderList.aspx',1,NULL,241,'CRM';";
        obj = this.ExecuteSQLStringScalar(strSql);
        
        strSql = "UPDATE AppUtility SET DBVersion='" + strAppVersion + "'";
        obj = this.ExecuteSQLStringScalar(strSql);

    }
    private void versionSample(string strAppVersion)
    {
        object obj = null;

        strSql = @"
        IF EXISTS (SELECT name FROM sysobjects 
              WHERE name = 'xxxx' AND type = 'P')
           DROP PROCEDURE xxxx
        ";
        obj = this.ExecuteSQLStringScalar(strSql);
        
        strSql = @"IF EXISTS (SELECT * FROM sysobjects WHERE name = 'fn_getApprovedLeaveDays' AND type = 'FN')
        BEGIN
            DROP FUNCTION fn_getApprovedLeaveDays
        END";
        obj = this.ExecuteSQLStringScalar(strSql);

        strSql = @"exec dbo.usp_InsertNewMenu 'Mfg. Voucher', 100,'Inventory/ManufacturingVoucher.aspx?typeval=29',13,NULL,266,'Inventory';";
        obj = this.ExecuteSQLStringScalar(strSql);
        
        strSql = "UPDATE AppUtility SET DBVersion='" + strAppVersion + "'";
        obj = this.ExecuteSQLStringScalar(strSql);

    }

  //  --DB
//C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319>aspnet_regsql.exe -S .\empresa -E -d BlumenSoft -ed

//--Table
//C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319>aspnet_regsql.exe -S .\empresa -E -d BlumenSoft -t Inv_SubType -et
    // Windows Authentication //aspnet_regsql.exe -S .\empresa -E -d BlumenSoft -t Inv_PriceList -et
    // NT Authentication //aspnet_regsql.exe -S nightshade.arvixe.com -U BlumenSoftdemo -P encrypted123pass -d demoBlumenSoft -t TB_Executive -et
    //aspnet_regsql.exe -S nightshade.arvixe.com -U BlumenSoftdemo -P encrypted123pass -d demoBlumenSoft -t Inv_BagType -et
    //aspnet_regsql.exe -S nightshade.arvixe.com -U BlumenSoftdemo -P encrypted123pass -d devBlumenSoftMerged -t Inv_BagType -et

//--region [dbo].[usp_InsertInv_Variety]

//------------------------------------------------------------------------------------------------------------------------
//-- Generated By:   Faruk Ahmed
//-- Procedure Name: [dbo].[usp_InsertInv_Variety]
//-- Date Generated: Wednesday, July 11, 2012
//------------------------------------------------------------------------------------------------------------------------

//alter PROCEDURE [dbo].[usp_InsertInv_Variety]
//    @VarietyDescription nvarchar(200),
//    @VarietyNo nvarchar(200),
//    @Genus nvarchar(200),
//    @SubType nvarchar(200),
//    @UOM nvarchar(200),
//    @UOC nvarchar(200),
//    @Color nvarchar(200),
//    @VarietyPhoto image,
//    @ShelfLife1 nvarchar(200),
//    @ShelfLife2 nvarchar(200),
//    @ShelfLife3 nvarchar(200),
//    @Aroma tinyint,
//    @Active tinyint
//AS

//SET NOCOUNT ON

//declare @GenusAutoID int,@SubTypeAutoID int,@UOMAutoID int,@UOCAutoID int,@ItemVarietyAutoID int,@StemSizeAutoID int,@UnitTypeAutoID int
//declare @ItemQualityAutoID int,@ItemTypeAutoID int,@ItemBrandAutoID int

//set @GenusAutoID =(select GenusAutoID from Inv_Genus where GenusDescription=@Genus);
//set @SubTypeAutoID =(select SubTypeAutoID from Inv_SubType where SubTypeDescription=@SubType);
//set @UOMAutoID =(select UOMAutoID from Inv_UOM where UOMDescription=@UOM);
//set @UOCAutoID =(select UOCAutoID from Inv_UOC where UOCDescription=@UOC);

//INSERT INTO [dbo].[Inv_Variety] (
//    VarietyDescription,
//    VarietyNo,
//    [GenusAutoID],
//    [SubTypeAutoID],
//    [UOMAutoID],
//    [UOCAutoID],
//    [Color],
//    [VarietyPhoto],
//    ShelfLife1,
//    ShelfLife2,
//    ShelfLife3,
//    Aroma,
//    [Active]
//) VALUES (
//    @VarietyDescription,
//    @VarietyNo,
//    @GenusAutoID,
//    @SubTypeAutoID,
//    @UOMAutoID,
//    @UOCAutoID,
//    @Color,
//    @VarietyPhoto,
//    @ShelfLife1,
//    @ShelfLife2,
//    @ShelfLife3,
//    @Aroma,
//    @Active
//)	
//declare @NumberingMethod int		
//set @NumberingMethod=1;
//set @NumberingMethod=(select VOUCHER_TYPE_NUMBERING_METHOD from TB_Voucher_Type where COMPANY_ID='0001' and VOUCHER_TYPE_VALUE=1);		        

//        if @NumberingMethod=0 
//        begin
//            update TB_Voucher_Type set VOUCHER_TYPE_TOTAL_VOUCHER=VOUCHER_TYPE_TOTAL_VOUCHER+1 
//            where 
//            COMPANY_ID='0001' and 
//            VOUCHER_TYPE_VALUE=1;
//        end
//--endregion
}