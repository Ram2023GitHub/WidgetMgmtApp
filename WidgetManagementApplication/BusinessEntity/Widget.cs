using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WidgetManagementApplication.BusinessEntity
{
    public class Widget
    {
        #region Properties

        public int WidgetID { get; set; }
        public string InventoryCode { get; set; }
        public string Description { get; set; }
        public int QuantityOnHand { get; set; }
        public int ReorderQuantity { get; set; }

        #endregion
    }
}