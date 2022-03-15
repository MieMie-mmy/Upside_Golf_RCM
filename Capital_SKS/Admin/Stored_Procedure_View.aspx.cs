﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Upside_Golf_RCM.Admin
{
    public partial class Stored_Procedure_View : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt = Name_Search();
                GdViewtable.DataSource = dt;
                GdViewtable.DataBind();
            }
       }

        protected void Btnsearch_Click(object sender, EventArgs e)
        {
                DataTable dt = new DataTable();
                dt = Name_Search();
                GdViewtable.DataSource = dt;
                GdViewtable.DataBind();
        }

        public DataTable Name_Search()
        {
            SqlConnection connectionString =con;
            SqlDataAdapter da = new SqlDataAdapter("SP_SP_Name_Search", connectionString);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = 0;
            if (!String.IsNullOrWhiteSpace(Txttablesearch.Text))
            {
                da.SelectCommand.Parameters.AddWithValue("@Name", Txttablesearch.Text.Trim());
            }
            else
            {
                da.SelectCommand.Parameters.AddWithValue("@Name", DBNull.Value);
            }
            da.SelectCommand.Connection.Open();
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.SelectCommand.Connection.Close();
            return dt;
        }
    }
}