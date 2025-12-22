using System.Collections.Generic;
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
/// Summary description for Common
/// </summary>
public class ConfDB
{
    public ConfDB()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void BindUnit(DropDownList ddlUnit)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
        SqlCommand cmd = new SqlCommand("Conf_GetFacilities", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        ListItem Listitem0 = new ListItem();
        Listitem0.Value = "0";
        Listitem0.Text = "Choose One";

        if (dr1.HasRows)
        {
            ddlUnit.DataSource = dr1;
            ddlUnit.DataTextField = "UnitName";
            ddlUnit.DataValueField = "UnitID";
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, Listitem0);
        }
        cmd.Parameters.Clear();
        cmd.Dispose();
        con.Close();
    }

    public void BindUsers(ListBox lst1)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
        SqlCommand cmd = new SqlCommand("Conf_Get_AllUsers", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        //ListItem Listitem0 = new ListItem();
        //Listitem0.Value = "0";
        //Listitem0.Text = "---- Participants ---";

        if (dr1.HasRows)
        {
            lst1.DataSource = dr1;
            lst1.DataTextField = "Name";
            lst1.DataValueField = "ListVal";
            lst1.DataBind();
            //lst1.Items.Insert(0, Listitem0);
        }
        cmd.Parameters.Clear();
        cmd.Dispose();
        con.Close();
    }
    

    public string CheckConf_Room(string MeetingID, string UID, string confroom_id, string conf_date, string conf_start_time, string conf_end_time)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("Conf_CheckRoom", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UID", UID);
                cmd.Parameters.AddWithValue("@cr_id", Convert.ToInt32(confroom_id));
                cmd.Parameters.AddWithValue("@conf_date", Convert.ToDateTime(conf_date));
                cmd.Parameters.AddWithValue("@conf_start_time", Convert.ToDateTime(conf_start_time));
                cmd.Parameters.AddWithValue("@conf_end_time", Convert.ToDateTime(conf_end_time));
                if (MeetingID.Length > 0)
                {
                    cmd.Parameters.AddWithValue("@MeetingID", MeetingID);
                }
                conn.Open();
                string Output = cmd.ExecuteScalar().ToString();
                conn.Close();
                return Output;
            }
        }
        catch (Exception ex)
        {
            return "Error";
        }
    }


    public void BindConfRoom(string UnitID, DropDownList ddlConfRoom)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
        SqlCommand cmd = new SqlCommand("Conf_GetConf_RoomList", con);
        cmd.Parameters.AddWithValue("@UnitId", UnitID);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        ListItem Listitem0 = new ListItem();
        Listitem0.Value = "0";
        Listitem0.Text = "Choose One";

        if (dr1.HasRows)
        {
            ddlConfRoom.DataSource = dr1;
            ddlConfRoom.DataTextField = "confRoomName";
            ddlConfRoom.DataValueField = "cr_id";
            ddlConfRoom.DataBind();
            ddlConfRoom.Items.Insert(0, Listitem0);
        }
        cmd.Parameters.Clear();
        cmd.Dispose();
        con.Close();
    }

    public DataSet GetMyMeetingList(string UID)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("Conf_GetMyMeetingList", con);
                cmd.Parameters.AddWithValue("@UID", UID);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dsRooms = new DataSet();
                da.Fill(dsRooms);
                con.Close();
                return dsRooms;
            }
        }
        catch (Exception ex)
        {
            return null;
        }       
    }

    public DataSet GetConf_Rooms(string UnitID)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("Conf_GetConf_RoomList", con);
                cmd.Parameters.AddWithValue("@UnitId", Convert.ToInt32(UnitID));
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dsRooms = new DataSet();
                da.Fill(dsRooms);
                if (dsRooms.Tables.Count > 0)
                    if (dsRooms.Tables[0].Rows.Count > 0)
                        return dsRooms;
                    else
                        return null;
                else
                    return null;

            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataSet GetConf_RoomBYID(string ID)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("Conf_GetMeeting_ByID", con);
                cmd.Parameters.AddWithValue("@cr_Main_id", ID);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dsRooms = new DataSet();
                da.Fill(dsRooms);
                return dsRooms;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    } 
}