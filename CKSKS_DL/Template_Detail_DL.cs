using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORS_RCM_Common;

namespace ORS_RCM_DL
{
    public class Template_Detail_DL
    {
        public Template_Detail_DL()
        { }

        public DataTable SelectByItemCode(string Item_Code)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlDataAdapter da = new SqlDataAdapter("Select * From Template_Detail WITH (NOLOCK) Where Item_Code=N'" + Item_Code + "'", connectionString);
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandTimeout = 0;

                DataTable dt = new DataTable();
                da.SelectCommand.Connection.Open();
                da.Fill(dt);
                da.SelectCommand.Connection.Close();

                return dt;
            }
            catch (Exception ex) { throw ex; }

        }

        public void Update(string item_code, Template_Detail_Entity tde)
        {
            try
            {
                SqlConnection connectionString = new SqlConnection(DataConfig.connectionString);
                SqlCommand cmd = new SqlCommand("SP_Template_Detail_InsertUpdate", connectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Item_Code", item_code);
                cmd.Parameters.AddWithValue("@Template1", tde.Template1);
                cmd.Parameters.AddWithValue("@Template2", tde.Template2);
                cmd.Parameters.AddWithValue("@Template3", tde.Template3);
                cmd.Parameters.AddWithValue("@Template4", tde.Template4);
                cmd.Parameters.AddWithValue("@Template5", tde.Template5);
                cmd.Parameters.AddWithValue("@Template6", tde.Template6);
                cmd.Parameters.AddWithValue("@Template7", tde.Template7);
                cmd.Parameters.AddWithValue("@Template8", tde.Template8);
                cmd.Parameters.AddWithValue("@Template9", tde.Template9);
                cmd.Parameters.AddWithValue("@Template10", tde.Template10);
                cmd.Parameters.AddWithValue("@Template11", tde.Template11);
                cmd.Parameters.AddWithValue("@Template12", tde.Template12);
                cmd.Parameters.AddWithValue("@Template13", tde.Template13);
                cmd.Parameters.AddWithValue("@Template14", tde.Template14);
                cmd.Parameters.AddWithValue("@Template15", tde.Template15);
                cmd.Parameters.AddWithValue("@Template16", tde.Template16);
                cmd.Parameters.AddWithValue("@Template17", tde.Template17);
                cmd.Parameters.AddWithValue("@Template18", tde.Template18);
                cmd.Parameters.AddWithValue("@Template19", tde.Template19);
                cmd.Parameters.AddWithValue("@Template20", tde.Template20);

                cmd.Parameters.AddWithValue("@Template_Content1", tde.Template_Content1);
                cmd.Parameters.AddWithValue("@Template_Content2", tde.Template_Content2);
                cmd.Parameters.AddWithValue("@Template_Content3", tde.Template_Content3);
                cmd.Parameters.AddWithValue("@Template_Content4", tde.Template_Content4);
                cmd.Parameters.AddWithValue("@Template_Content5", tde.Template_Content5);
                cmd.Parameters.AddWithValue("@Template_Content6", tde.Template_Content6);
                cmd.Parameters.AddWithValue("@Template_Content7", tde.Template_Content7);
                cmd.Parameters.AddWithValue("@Template_Content8", tde.Template_Content8);
                cmd.Parameters.AddWithValue("@Template_Content9", tde.Template_Content9);
                cmd.Parameters.AddWithValue("@Template_Content10", tde.Template_Content10);
                cmd.Parameters.AddWithValue("@Template_Content11", tde.Template_Content11);
                cmd.Parameters.AddWithValue("@Template_Content12", tde.Template_Content12);
                cmd.Parameters.AddWithValue("@Template_Content13", tde.Template_Content13);
                cmd.Parameters.AddWithValue("@Template_Content14", tde.Template_Content14);
                cmd.Parameters.AddWithValue("@Template_Content15", tde.Template_Content15);
                cmd.Parameters.AddWithValue("@Template_Content16", tde.Template_Content16);
                cmd.Parameters.AddWithValue("@Template_Content17", tde.Template_Content17);
                cmd.Parameters.AddWithValue("@Template_Content18", tde.Template_Content18);
                cmd.Parameters.AddWithValue("@Template_Content19", tde.Template_Content19);
                cmd.Parameters.AddWithValue("@Template_Content20", tde.Template_Content20);

                cmd.Parameters.AddWithValue("@Detail_Template1", tde.Detail_Template1);
                cmd.Parameters.AddWithValue("@Detail_Template2", tde.Detail_Template2);
                cmd.Parameters.AddWithValue("@Detail_Template3", tde.Detail_Template3);
                cmd.Parameters.AddWithValue("@Detail_Template4", tde.Detail_Template4);

                cmd.Parameters.AddWithValue("@Detail_Template_Content1", tde.Detail_Template_Content1);
                cmd.Parameters.AddWithValue("@Detail_Template_Content2", tde.Detail_Template_Content2);
                cmd.Parameters.AddWithValue("@Detail_Template_Content3", tde.Detail_Template_Content3);
                cmd.Parameters.AddWithValue("@Detail_Template_Content4", tde.Detail_Template_Content4);

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
