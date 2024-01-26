using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WidgetManagementApplication.BusinessEntity;
using WidgetManagementApplication.Helpers;
using WidgetManagementApplication.Interface;

namespace WidgetManagementApplication.DataAccess
{
    internal sealed class WidgetDataAccess : IWidgetDataAccess
    {
        #region Variables

        private static readonly ILog Logger = LogManager.GetLogger(typeof(WidgetDataAccess));
        private static readonly string connectionString = ConnectionStringHelper.GetConnectionString();

        #endregion

        #region Methods

        public List<Widget> GetAllWidgetDetails()
        {
            List<Widget> allWidgets = new List<Widget>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("RetrieveWidgetsDetails", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Widget widget = new Widget
                                {
                                    WidgetID = Convert.ToInt32(reader["WidgetID"]),
                                    InventoryCode = Convert.ToString(reader["InventoryCode"]),
                                    Description = Convert.ToString(reader["Description"]),
                                    QuantityOnHand = Convert.ToInt16(reader["QuantityOnHand"]),
                                    ReorderQuantity = Convert.ToInt16(reader["ReorderQuantity"])
                                };
                                allWidgets.Add(widget);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex != null && ex.InnerException != null)
                {
                    Logger.Error("Error in dataaccess while getting all widget details list : " + Environment.NewLine +
                                 "Exception message : " + ex.Message + Environment.NewLine +
                                 "InnerException: " + ex.InnerException.ToString());
                }
                else if (ex != null)
                {
                    Logger.Error("Error in dataaccess while getting all widget details list : " + Environment.NewLine +
                                 "Exception message : " + ex.Message + Environment.NewLine);
                }
            }
            return allWidgets;
        }

        public Widget GetWidgetDetailsByWidgetID(int widgetId)
        {
            Widget widgetDetail = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetWidgetDetailsById", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@WidgetID", widgetId);
                        connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                widgetDetail = new Widget();
                                widgetDetail.InventoryCode = reader["InventoryCode"].ToString();
                                widgetDetail.Description = reader["Description"].ToString();
                                widgetDetail.QuantityOnHand = Convert.ToInt16(reader["QuantityOnHand"]);
                                widgetDetail.ReorderQuantity = Convert.ToInt16(reader["ReorderQuantity"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex != null && ex.InnerException != null)
                {
                    Logger.Error("Error in dataaccess while getting widget details by WidgetId : " + widgetId
                                 + Environment.NewLine + " Exception message : " + ex.Message
                                 + Environment.NewLine + " InnerException: " + ex.InnerException.ToString());
                }
                else if (ex != null)
                {
                    Logger.Error("Error in dataaccess while getting widget details by WidgetId : " + widgetId
                                + Environment.NewLine + " Exception message : " + ex.Message);
                }
            }
            return widgetDetail;
        }

        public void InsertOrUpdateWidgetDetails(Widget widget)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("CreateOrUpdateWidgetDetails", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@WidgetID", widget.WidgetID);
                        cmd.Parameters.AddWithValue("@InventoryCode", widget.InventoryCode);
                        cmd.Parameters.AddWithValue("@Description", widget.Description);
                        cmd.Parameters.AddWithValue("@QuantityOnHand", widget.QuantityOnHand);
                        cmd.Parameters.AddWithValue("@ReorderQuantity", widget.ReorderQuantity);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex != null && ex.InnerException != null)
                {
                    Logger.Error("Error in dataaccess while InsertOrUpdate widget details and WidgetId : " + widget.WidgetID
                                 + Environment.NewLine + " Exception message : " + ex.Message
                                 + Environment.NewLine + " InnerException: " + ex.InnerException.ToString());
                }
                else if (ex != null)
                {
                    Logger.Error("Error in dataaccess while InsertOrUpdate widget details and WidgetId : " + widget.WidgetID
                                + Environment.NewLine + " Exception message : " + ex.Message);
                }
            }

        }

        public void DeleteWidgetDetailsByWidgetID(int widgetId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DeleteWidgetDetails", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@WidgetID", widgetId);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex != null && ex.InnerException != null)
                {
                    Logger.Error("Error in dataaccess while deleting widget details by WidgetId : " + widgetId
                                 + Environment.NewLine + " Exception message : " + ex.Message
                                 + Environment.NewLine + " InnerException: " + ex.InnerException.ToString());
                }
                else if (ex != null)
                {
                    Logger.Error("Error in dataaccess while deleting widget details by WidgetId : " + widgetId
                                + Environment.NewLine + " Exception message : " + ex.Message);
                }
            }
        }

        public int GetWidgetRecordCountFromDatabase()
        {
            int totalWidgetRecordCount = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetWidgetRecordCount", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        totalWidgetRecordCount = Convert.ToInt16(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex != null && ex.InnerException != null)
                {
                    Logger.Error("Error in dataaccess while getting total widget records count : "
                                 + Environment.NewLine + " Exception message : " + ex.Message
                                 + Environment.NewLine + " InnerException: " + ex.InnerException.ToString());
                }
                else if (ex != null)
                {
                    Logger.Error("Error in dataaccess while getting total widget records count : "
                                + Environment.NewLine + " Exception message : " + ex.Message);
                }
            }

            return totalWidgetRecordCount;
        }

        #endregion
    }
}