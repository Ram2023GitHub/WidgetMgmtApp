using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using WidgetManagementApplication.Helpers;
using log4net;
using WidgetManagementApplication.BusinessEntity;
using WidgetManagementApplication.Interface;
using WidgetManagementApplication.DataAccess;
using System.Collections.Generic;
using WebGrease;
using System.Web;

namespace WidgetManagementApplication
{
    public partial class WidgetRecordList : System.Web.UI.Page
    {
        #region Variables

        private static readonly ILog Logger = log4net.LogManager.GetLogger(typeof(WidgetRecordList));
        IWidgetDataAccess widgetDataAccess = new WidgetDataAccess();
        private CacheManagerHelper<Widget> cacheManagerHelper;

        #endregion

        #region Methods

        private void GetAllWidgetDetailsAndBind()
        {
            try
            {
                Logger.Info("Started executing GetAllWidgetDetailsAndBind Method....");

                Session.Remove("CacheManager");

                int totalWidgetRecordsCOunt = widgetDataAccess.GetWidgetRecordCountFromDatabase();

                cacheManagerHelper = Session["CacheManager"] as CacheManagerHelper<Widget>;

                if (cacheManagerHelper == null)
                {
                    cacheManagerHelper = new CacheManagerHelper<Widget>(totalWidgetRecordsCOunt);

                    Session["CacheManager"] = cacheManagerHelper;
                }

                List<Widget> widgetData = cacheManagerHelper.GetData();

                if (widgetData == null)
                {
                    widgetData = widgetDataAccess.GetAllWidgetDetails();

                    if (widgetData != null)
                        cacheManagerHelper.CacheData(widgetData);

                }

                GridView1.DataSource = widgetData;
                GridView1.DataBind();


                Logger.Info("Completed executing GetAllWidgetDetailsAndBind Method.");
            }
            catch (Exception ex)
            {
                if (ex != null && ex.InnerException != null)
                {
                    Logger.Error("Error in GetAllWidgetDetailsAndBind while getting all widget details : "
                                 + Environment.NewLine + " Exception message : " + ex.Message
                                 + Environment.NewLine + " InnerException: " + ex.InnerException.ToString());
                }
                else if (ex != null)
                {
                    Logger.Error("Error in GetAllWidgetDetailsAndBind while getting all widget details : "
                                 + Environment.NewLine + " Exception message : " + ex.Message);
                }
                throw;//Shows Error page.
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    int currentPageIndex = Session["CurrentPageIndex"] != null ? Convert.ToInt32(Session["CurrentPageIndex"]) : 0;

                    if (currentPageIndex > 0)
                        GridView1.PageIndex = currentPageIndex - 1;

                    GetAllWidgetDetailsAndBind();
                }
            }
            catch (Exception ex)
            {
                if (ex != null && ex.InnerException != null)
                {
                    Logger.Error("Error in Page_Load method in WidgetRecordList : "
                                 + Environment.NewLine + " Exception message : " + ex.Message
                                 + Environment.NewLine + " InnerException: " + ex.InnerException.ToString());
                }
                else if (ex != null)
                {
                    Logger.Error("Error in Page_Load method in WidgetRecordList: "
                                 + Environment.NewLine + " Exception message : " + ex.Message);
                }

                throw;//Shows Error page.

            }

        }

        protected void GridView1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            try
            {
                int widgetID = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Values["WidgetID"]);

                Session["CurrentPageIndex"] = GridView1.PageIndex + 1;
                Session["IsEditOperation"] = true;
                Session["IsDeleteOperation"] = null;
                Session["IsInsertOperation"] = null;

                CacheManagerHelper<Widget> cacheManagerHelper = Session["CacheManager"] as CacheManagerHelper<Widget>;

                if (cacheManagerHelper != null)
                {
                    cacheManagerHelper.InvalidateCache();

                    Session.Remove("CacheManager");
                }


                Response.Redirect("WidgetEdit.aspx?WidgetID=" + widgetID, false);
                Context.ApplicationInstance.CompleteRequest();

            }
            catch (Exception ex)
            {
                if (ex != null && ex.InnerException != null)
                {
                    Logger.Error("Error while RowEditingEvent method in WidgetRecordList : "
                                 + Environment.NewLine + " Exception message : " + ex.Message
                                 + Environment.NewLine + " InnerException: " + ex.InnerException.ToString());
                }
                else if (ex != null)
                {
                    Logger.Error("Error while RowEditingEvent method in WidgetRecordList : "
                                 + Environment.NewLine + " Exception message : " + ex.Message);
                }
                throw;//Shows Error page.
            }

        }

        protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            try
            {
                int widgetID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

                Session["IsDeleteOperation"] = true;
                Session["IsEditOperation"] = null;
                Session["IsInsertOperation"] = null;

                CacheManagerHelper<Widget> cacheManagerHelper = Session["CacheManager"] as CacheManagerHelper<Widget>;

                if (cacheManagerHelper != null)
                {
                    cacheManagerHelper.InvalidateCache();
                    Session.Remove("CacheManager");
                }

                Response.Redirect("WidgetEdit.aspx?WidgetID=" + widgetID, false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                if (ex != null && ex.InnerException != null)
                {
                    Logger.Error("Error while RowDeletingEvent method in WidgetRecordList : "
                                 + Environment.NewLine + " Exception message : " + ex.Message
                                 + Environment.NewLine + " InnerException: " + ex.InnerException.ToString());
                }
                else if (ex != null)
                {
                    Logger.Error("Error while RowDeletingEvent method in WidgetRecordList : "
                                 + Environment.NewLine + " Exception message : " + ex.Message);
                }
                throw;//Shows Error page.
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridView1.PageIndex = e.NewPageIndex;
                GetAllWidgetDetailsAndBind();
            }
            catch (Exception ex)
            {
                if (ex != null && ex.InnerException != null)
                {
                    Logger.Error("Error while PageIndexChangingEvent method in WidgetRecordList : "
                                 + Environment.NewLine + " Exception message : " + ex.Message
                                 + Environment.NewLine + " InnerException: " + ex.InnerException.ToString());
                }
                else if (ex != null)
                {
                    Logger.Error("Error while PageIndexChangingEvent method in WidgetRecordList : "
                                 + Environment.NewLine + " Exception message : " + ex.Message);
                }
                throw;//Shows Error page.
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Session["IsDeleteOperation"] = null;
                Session["IsEditOperation"] = null;
                Session["IsInsertOperation"] = true;

                CacheManagerHelper<Widget> cacheManagerHelper = Session["CacheManager"] as CacheManagerHelper<Widget>;

                if (cacheManagerHelper != null)
                {
                    cacheManagerHelper.InvalidateCache();
                    Session.Remove("CacheManager");
                }

                Response.Redirect("WidgetEdit.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                if (ex != null && ex.InnerException != null)
                {
                    Logger.Error("Error while AddEvent method in WidgetRecordList : "
                                 + Environment.NewLine + " Exception message : " + ex.Message
                                 + Environment.NewLine + " InnerException: " + ex.InnerException.ToString());
                }
                else if (ex != null)
                {
                    Logger.Error("Error while AddEvent method in WidgetRecordList : "
                                 + Environment.NewLine + " Exception message : " + ex.Message);
                }
                throw;//Shows Error page.
            }
        }

        #endregion
    }
}
