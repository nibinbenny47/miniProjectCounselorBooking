﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Configuration;
using System.IO;
using System.Data.SqlClient;

public partial class User_Default : System.Web.UI.Page
{
    string connectionString = WebConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection con;
    SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            fillWomenItem();
            fillMenItem();

        }
    }
    public void connection()
    {
        con = new SqlConnection(connectionString);
        con.Open();
    }
    protected void fillMenItem()
    {
        connection();

        cmd = new SqlCommand("sp_Homepage", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@status", 1);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //adp.Fill(dt);
        using (SqlDataReader dr = cmd.ExecuteReader())
        {
            if (dr.HasRows)
            {
                ddlMenItem.DataSource = dr;
                ddlMenItem.DataValueField = "item_id";
                ddlMenItem.DataTextField = "item_name";
                ddlMenItem.DataBind();
                ddlMenItem.Items.Insert(0, "--select Men items--");

            }
        }
    }
    protected void fillWomenItem()
    {
        connection();

        cmd = new SqlCommand("sp_Homepage", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@status", 2);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //adp.Fill(dt);
        using (SqlDataReader dr = cmd.ExecuteReader())
        {
            if (dr.HasRows)
            {
                ddlWomenItem.DataSource = dr;
                ddlWomenItem.DataValueField = "item_id";
                ddlWomenItem.DataTextField = "item_name";
                ddlWomenItem.DataBind();
                ddlWomenItem.Items.Insert(0, "--select Women items--");
            }
        }
    }

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    connection();
    //}
    protected void fillMenDatalist()
    {
        connection();
        string product = ddlMenItem.SelectedValue;
        cmd = new SqlCommand("sp_Homepage", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@status", 3);
        cmd.Parameters.AddWithValue("@selectedProduct", product);
        DataTable dt = new DataTable();
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);
        dtlistMenItems.DataSource = dt;
        dtlistMenItems.DataBind();
    }
    protected void fillWomenDatalist()
    {
        connection();
        string product = ddlWomenItem.SelectedValue;
        cmd = new SqlCommand("sp_Homepage", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@status", 3);
        cmd.Parameters.AddWithValue("@selectedProduct", product);
        DataTable dt = new DataTable();
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);
        dtlistWomenItems.DataSource = dt;
        dtlistWomenItems.DataBind();
    }



    protected void ddlMenItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        fillMenDatalist();
    }

    protected void ddlWomenItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        fillWomenDatalist();
    }
}