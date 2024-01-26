using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetManagementApplication.BusinessEntity;

namespace WidgetManagementApplication.Interface
{
    #region IWidgetDataAccess
    public interface IWidgetDataAccess
    {
        void InsertOrUpdateWidgetDetails(Widget widget);
        List<Widget> GetAllWidgetDetails();
        Widget GetWidgetDetailsByWidgetID(int widgetId);
        void DeleteWidgetDetailsByWidgetID(int widgetId);
        int GetWidgetRecordCountFromDatabase();

    }
    #endregion
}
