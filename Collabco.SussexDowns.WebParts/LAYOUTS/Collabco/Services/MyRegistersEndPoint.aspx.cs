using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI;
using Microsoft.SharePoint.Utilities;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Collabco.SussexDowns.WebParts;

namespace Collabco.SussexDowns.WebParts.Layouts.collabco.services
{
    public partial class MyRegistersEndPoint : System.Web.UI.Page
    {
        private string _userName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Request["UserName"]))
            {
                this._userName = this.Request["UserName"];
            }
            else
            {
                this._userName = Utility.SanitiseLoginName(SPContext.Current.Web.CurrentUser.LoginName);
            }
            this._userName = this._userName.ToLower();
        }
        
        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<register>");
            sb.Append("<MyNonRegisterCount>");
            sb.Append(GetUnMarkedRegistersCountByUser());
            sb.Append("</MyNonRegisterCount>");
            sb.Append("</register>");
            writer.Write(sb.ToString());
        }

        private int GetUnMarkedRegistersCountByUser()
        {
            int unMarkedRegisterCount = 0;
            try
            {
                unMarkedRegisterCount = (int)SQLHelper.GetUnMarkedRegistersCountByUser(_userName);
            }
            catch (Exception exp)
            {
                DiagnosticLog.Error("GetUnMarkedRegistersCountByUser", exp.Message);
                throw new SPException(exp.Message, exp);
            }
            return unMarkedRegisterCount;
        }
       
    }
}