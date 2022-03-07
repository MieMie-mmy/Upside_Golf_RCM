/* 
*
Created By              : Kyaw Thet Paing
Created Date          :
Updated By             :
Updated Date         :

 Tables using           :  
 *                                  - 
 *                                  -
 *                                  -
 *                                  
  * Storedprocedure using:  
 *                                           - 
 *                                           - 
 *                                           -
                                     
    -
    -
    -
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace ORS_RCM.WebForms.Import
{
    public partial class Import_Item_New : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                String Itemmaster = String.Empty;
                String Sku = String.Empty;
                String Inventory = String.Empty;
                String tagID = String.Empty;
                String MonotaroImport = String.Empty;

                if (uplItemImport.HasFile)//check item master file select
                {
                    String filename = Path.GetFileName(uplItemImport.PostedFile.FileName);
                    if (check(filename) == true)
                    {
                        uplItemImport.SaveAs(Server.MapPath("~/Import_CSV/") + filename);//save file
                        Itemmaster = filename;
                    }
                }
                if (uplSKUImport.HasFile)//check sku file select
                {
                    String filename = Path.GetFileName(uplSKUImport.PostedFile.FileName);
                    if (check(filename) == true)
                    {
                        uplSKUImport.SaveAs(Server.MapPath("~/Import_CSV/") + filename);//save file
                        Sku = filename;
                    }
                }
                if (uplInventoryImport.HasFile)//check inventory file exists
                {
                    String filename = Path.GetFileName(uplInventoryImport.PostedFile.FileName);
                    if (check(filename) == true)
                    {
                        uplInventoryImport.SaveAs(Server.MapPath("~/Import_CSV/") + filename);//save file
                        Inventory = filename;
                    }
                }
                if (uplRakutenTagID.HasFile)                // added by ETZ for sks-390 TagID
                {
                    String filename = Path.GetFileName(uplRakutenTagID.PostedFile.FileName);
                    if (check(filename) == true)
                    {
                        uplRakutenTagID.SaveAs(Server.MapPath("~/Import_CSV/") + filename);
                        tagID = filename;
                    }
                }
                if (uplMonotaroImport.HasFile)
                {
                    String filename = Path.GetFileName(uplMonotaroImport.PostedFile.FileName);
                    if (checkfile(filename) == true)
                    {
                        uplMonotaroImport.SaveAs(Server.MapPath("~/Import_CSV/") + filename);
                        MonotaroImport = filename;
                    }
                }

                //call confirm page with file name
                Response.Redirect("~/WebForms/Import/Import_Item_Confirm.aspx?Master=" + Itemmaster + "&Sku=" + Sku + "&Inventory=" + Inventory + "&TagID=" + tagID +"&Monotaro="+ MonotaroImport, false);
                //Response.Redirect("~/WebForms/Import/Import_Item_Confirm.aspx?Master=" + Itemmaster + "&Sku=" + Sku + "&Inventory=" + Inventory, false);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        private bool check(String str)
        {
            try
            {
                if (str.Contains(".csv"))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool checkfile(string str)
        {
            try
            {
                if (str.Contains(".xlsx"))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}