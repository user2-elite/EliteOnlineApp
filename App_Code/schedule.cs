using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using INTModel;

/// <summary>
/// Summary description for schedule
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

public class Event
{
    public int EventID { get; set; }
    public string EventName { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
}

//[WebService(Namespace = "http://tempuri.org/")]
//[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//[System.ComponentModel.ToolboxItem(false)]
[ScriptService]
public class schedule : System.Web.Services.WebService
{

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetJSONEvents()
    {
        Event[] events = new Event[2];
        events[0] = new Event();
        events[0].EventID = 1;
        events[0].EventName = "My Event 1";
        events[0].StartDate = "2015-06-02T9:00:00";
        events[0].EndDate = "2015-06-02T10:00:00";

        events[1] = new Event();
        events[1].EventID = 2;
        events[1].EventName = "My Event 2";
        events[1].StartDate = "2015-06-02T11:00:00";
        events[1].EndDate = "2015-06-02T11:30:00";
        //return events;
        return new JavaScriptSerializer().Serialize(events);
    }


    [WebMethod]
    public List<Event> GetXMLEvents()
    {
        List<Event> events = new List<Event>();
        events.Add(new Event()
        {
            EventID = 1,
            EventName = "EventName 1",
            StartDate = "2015-06-02T02:00:00",
            EndDate = "2015-06-02T03:00:00"
        });
        //events.Add(new Event()
        //{
        //    EventID = 2,
        //    EventName = "EventName 2",
        //    StartDate = "2015-06-02T11:00:00",
        //    EndDate = "2015-06-02T11:30:00"
        //});
        //events.Add(new Event()
        //{
        //    EventID = 3,
        //    EventName = "EventName 3",
        //    StartDate = "2015-06-02T16:00:00",
        //    EndDate = "2015-06-02T17:00:00"
        //});
        //events.Add(new Event()
        //{
        //    EventID = 4,
        //    EventName = "EventName 4",
        //    StartDate = DateTime.Now.AddDays(22).ToString("MM-dd-yyyy"),
        //    EndDate = DateTime.Now.AddDays(25).ToString("MM-dd-yyyy")
        //});
        return events;
    }


    [WebMethod]
    public List<INTModel.Conf_GetMyMeetingList_Result> GetXMLEventsFromEntity1()
    {
        string UID = "";
        if (this.Context.Request.QueryString["user"] != null)
        {
            UID = this.Context.Request.QueryString["user"].ToString();
        }

        //List<Event> events = new List<Event>();
        INTModel.INTModelContainer objModel = new INTModel.INTModelContainer();
        List<INTModel.Conf_GetMyMeetingList_Result> objList = objModel.Conf_GetMyMeetingList(UID).ToList();

        //ConfDB objConfDB = new ConfDB();
        //objConfDB.GetMyMeetingList()
        return objList;
    }

    [WebMethod]
    public List<INTModel.Conf_GetMyMeetingListAll_Result> GetXMLEventsFromEntity2()
    {
        string UID = "";
        if (this.Context.Request.QueryString["user"] != null)
        {
            UID = this.Context.Request.QueryString["user"].ToString();
        }

        //List<Event> events = new List<Event>();
        INTModel.INTModelContainer objModel = new INTModel.INTModelContainer();
        List<INTModel.Conf_GetMyMeetingListAll_Result> objList = objModel.Conf_GetMyMeetingListAll(UID).ToList();

        //ConfDB objConfDB = new ConfDB();
        //objConfDB.GetMyMeetingList()
        return objList;
    }

    [WebMethod]
    public List<INTModel.Conf_GetMeetingOrganizedListAll_Result> GetXMLEventsFromEntity3()
    {
        string UID = "";
        if (this.Context.Request.QueryString["user"] != null)
        {
            UID = this.Context.Request.QueryString["user"].ToString();
        }

        //List<Event> events = new List<Event>();
        INTModel.INTModelContainer objModel = new INTModel.INTModelContainer();
        List<INTModel.Conf_GetMeetingOrganizedListAll_Result> objList = objModel.Conf_GetMeetingOrganizedListAll(UID).ToList();

        //ConfDB objConfDB = new ConfDB();
        //objConfDB.GetMyMeetingList()
        return objList;
    }
}
