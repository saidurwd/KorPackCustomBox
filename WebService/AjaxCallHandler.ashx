<%@ WebHandler Language="C#" Class="AjaxCallHandler" %>

using System;
using System.Web;
//using Blumensoft.Business;

public class AjaxCallHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/x-javascript";
        string MethodName = context.Request.Params["method"];
        string Parameter = context.Request.Params["Param"];
        string CallBackMethod = context.Request.Params["callbackMethod"];
        switch (MethodName.ToLower())
        {
            case "getvarietygenus":
                // context.Response.Write(GetVarietyGenus(context));
                break;
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}