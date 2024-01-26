using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using WidgetManagementApplication.Helpers;
using log4net;
using WidgetManagementApplication.DataAccess;
using WidgetManagementApplication.Interface;
using WidgetManagementApplication.BusinessEntity;

namespace WidgetManagementApplication
{
    public partial class WidgetEdit : System.Web.UI.Page
    {
        #region Varibales

        private int widgetID;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(WidgetEdit));
        IWidgetDataAccess widgetDataAccess = new WidgetDataAccess();

        #endregion

        #region Methods
        private void LoadDataForEditingWidgetRecords(int widgetID)
        {
            Widget widget = new Widget();
            try
            {
                if (widgetID > 0)
                {
                    widget = widgetDataAccess.GetWidgetDetailsByWidgetID(widgetID);

                    if (widget != null)
                    {
                        txtInventoryCode.Text = widget.InventoryCode ?? "N/A";
                        txtDescription.Text = widget.Description ?? "N/A";
                        txtQuantityOnHand.Text = widget.QuantityOnHand.ToString();
                        txtReorderQuantity.Text = widget.ReorderQuantity.ToString();

                    }
                    else if (Session["IsEditOperation"] != null)
                    {
                        if (Convert.ToBoolean(Session["IsEditOperation"]))
                        {
                            Logger.Info("Widget details not found for widgetID while editing in WidgetEdit :" + widgetID);
                        }
                    }
                    else if (Session["IsDeleteOperation"] != null)
                    {
                        Logger.Info("Widget details not found for widgetID while deleting in WidgetEdit :" + widgetID);
                    }
                }
                else
                {
                    if (Session["IsInsertOperation"] != null)
                    {
                        if (Convert.ToBoolean(Session["IsInsertOperation"]))
                        {
                            Logger.Info("This is new Widget details insertion operation in WidgetEdit");
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                if (ex != null && ex.InnerException != null)
                {
                    Logger.Error("Error in PageLoadEvent for LoadDataForEditingWidgetRecords method while inserting/editing/deleting in WidgetEdit: " + widgetID
                                 + Environment.NewLine + " Exception message : " + ex.Message
                                 + Environment.NewLine + " InnerException: " + ex.InnerException.ToString());
                }
                else if (ex != null)
                {
                    Logger.Error("Error in PageLoadEvent for LoadDataForEditingWidgetRecords method while inserting/editing/deleting in WidgetEdit: " + widgetID
                                 + Environment.NewLine + " Exception message : " + ex.Message);
                }
                throw;//Shows Error page.
            }
        }

        private void InsertOrUpdateWidgetRecords(int widgetID)
        {
            try
            {
                Logger.Info("Started binding Widget properties....");
                Widget widget = new Widget();

                widget.WidgetID = widgetID;
                widget.InventoryCode = txtInventoryCode.Text;
                widget.Description = txtDescription.Text;
                widget.QuantityOnHand = Convert.ToInt16(txtQuantityOnHand.Text);
                widget.ReorderQuantity = Convert.ToInt16(txtReorderQuantity.Text);

                Logger.Info("Completed binding Widget properties....");

                if (widgetID > 0)
                    Logger.Info("Started inserting Widget records.");
                else
                    Logger.Info("Started updating Widget records :" + widgetID);

                widgetDataAccess.InsertOrUpdateWidgetDetails(widget);

                if (widgetID > 0)
                    Logger.Info("Completed inserting Widget records.");
                else
                    Logger.Info("Completed updating Widget records :" + widgetID);

            }
            catch (Exception ex)
            {
                if (ex != null && ex.InnerException != null)
                {
                    Logger.Error("Error in InsertOrUpdateWidgetRecords method for widgetId while insert/update in WidgetEdit: " + widgetID
                                 + Environment.NewLine + " Exception message : " + ex.Message
                                 + Environment.NewLine + " InnerException: " + ex.InnerException.ToString());
                }
                else if (ex != null)
                {
                    Logger.Error("Error in InsertOrUpdateWidgetRecords method for widgetId while insert/update in WidgetEdit: " + widgetID
                                 + Environment.NewLine + " Exception message : " + ex.Message);
                }
                throw;//Shows Error page.
            }
        }

        private void DeleteWidgetRecords(int widgetID)
        {
            try
            {
                if (widgetID > 0)
                {
                    Logger.Info("Started deleting Widget records :" + widgetID);

                    widgetDataAccess.DeleteWidgetDetailsByWidgetID(widgetID);

                    Logger.Info("Completed deleting Widget records :" + widgetID);
                }
                else
                {
                    Logger.Info("Widget details not found for widgetID while deleting in WidgetEdit :" + widgetID);
                }
            }
            catch (Exception ex)
            {
                if (ex != null && ex.InnerException != null)
                {
                    Logger.Error("Error in DeleteWidgetRecords method for widgetId while deleting in WidgetEdit: " + widgetID
                                 + Environment.NewLine + " Exception message : " + ex.Message
                                 + Environment.NewLine + " InnerException: " + ex.InnerException.ToString());
                }
                else if (ex != null)
                {
                    Logger.Error("Error in DeleteWidgetRecords method for widgetId while deleting in WidgetEdit: " + widgetID
                                 + Environment.NewLine + " Exception message : " + ex.Message);
                }
                throw;//Shows Error page.
            }
        }

        protected void cvReorderQuantity_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                int quantityOnHand;
                int reorderQuantity;

                if (int.TryParse(txtQuantityOnHand.Text, out quantityOnHand) && int.TryParse(txtReorderQuantity.Text, out reorderQuantity))
                {
                    args.IsValid = reorderQuantity < quantityOnHand;
                }
                else
                {
                    args.IsValid = false;
                }
            }
            catch (Exception ex)
            {
                if (ex != null && ex.InnerException != null)
                {
                    Logger.Error("Error in ReorderQuantity_ServerValidate :"
                                 + Environment.NewLine + " Exception message : " + ex.Message
                                 + Environment.NewLine + " InnerException: " + ex.InnerException.ToString());
                }
                else if (ex != null)
                {
                    Logger.Error("Error in ReorderQuantity_ServerValidate : "
                                 + Environment.NewLine + " Exception message : " + ex.Message);
                }
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

                    if (int.TryParse(Request.QueryString["WidgetID"], out widgetID))
                    {
                        if (Session["IsDeleteOperation"] != null)
                        {
                            if (Convert.ToBoolean(Session["IsDeleteOperation"]))
                            {
                                btnDelete.Enabled = true;
                                btnSave.Text = "Update";
                                btnSave.Enabled = false;
                                txtInventoryCode.Enabled = false;
                                txtDescription.Enabled = false;
                                txtQuantityOnHand.Enabled = false;
                                txtReorderQuantity.Enabled = false;
                            }
                        }
                        else if (Session["IsEditOperation"] != null)
                        {
                            if (Convert.ToBoolean(Session["IsEditOperation"]))
                            {
                                btnDelete.Enabled = false;
                                btnSave.Text = "Update";
                            }
                        }

                    }
                    else
                    {
                        btnDelete.Enabled = false;
                    }

                    LoadDataForEditingWidgetRecords(widgetID);
                }

            }
            catch (Exception ex)
            {
                if (ex != null && ex.InnerException != null)
                {
                    Logger.Error("Error in Page_Load method in WidgetEdit : "
                                 + Environment.NewLine + " Exception message : " + ex.Message
                                 + Environment.NewLine + " InnerException: " + ex.InnerException.ToString());
                }
                else if (ex != null)
                {
                    Logger.Error("Error in Page_Load method in WidgetEdit: "
                                 + Environment.NewLine + " Exception message : " + ex.Message);
                }
                throw;//Shows Error page.
            }


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                int widgetID;

                if (int.TryParse(Request.QueryString["WidgetID"], out widgetID))
                {
                    btnSave.Text = "Update";
                    btnDelete.Visible = true;
                }
                else
                {
                    btnDelete.Visible = false;
                }

                Logger.Info("Started InsertOrUpdateWidgetRecords for WidgetId :" + widgetID);

                InsertOrUpdateWidgetRecords(widgetID);

                Logger.Info("Completed InsertOrUpdateWidgetRecords for WidgetId :" + widgetID);

                Response.Redirect("WidgetRecordList.aspx", false);
                Context.ApplicationInstance.CompleteRequest();

            }
            catch (Exception ex)
            {
                if (ex != null && ex.InnerException != null)
                {
                    Logger.Error("Error in SaveUpdateEvent for InsertOrUpdateWidgetRecords method :"
                                 + Environment.NewLine + " Exception message : " + ex.Message
                                 + Environment.NewLine + " InnerException: " + ex.InnerException.ToString());
                }
                else if (ex != null)
                {
                    Logger.Error("Error in SaveUpdateEvent for InsertOrUpdateWidgetRecords method :"
                                 + Environment.NewLine + " Exception message : " + ex.Message);
                }
                throw;//Shows Error page.
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["WidgetID"] != null)
                {
                    int widgetID = Convert.ToInt32(Request.QueryString["WidgetID"]);

                    Logger.Info("Started DeleteWidgetRecords for WidgetId :" + widgetID);

                    DeleteWidgetRecords(widgetID);

                    Logger.Info("Completed DeleteWidgetRecords for WidgetId :" + widgetID);

                    Response.Redirect("WidgetRecordList.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();

                }
                else
                {
                    Logger.Info("WidgetID not present in request for deltion operation");
                }
            }
            catch (Exception ex)
            {
                if (ex != null && ex.InnerException != null)
                {
                    Logger.Error("Error in DeleteEvent method for widgetId while deleting in WidgetEdit: " + widgetID
                                 + Environment.NewLine + " Exception message : " + ex.Message
                                 + Environment.NewLine + " InnerException: " + ex.InnerException.ToString());
                }
                else if (ex != null)
                {
                    Logger.Error("Error in DeleteEvent method for widgetId while deleting in WidgetEdit: " + widgetID
                                 + Environment.NewLine + " Exception message : " + ex.Message);
                }
                throw;//Shows Error page.
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("WidgetRecordList.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        #endregion

    }
}
