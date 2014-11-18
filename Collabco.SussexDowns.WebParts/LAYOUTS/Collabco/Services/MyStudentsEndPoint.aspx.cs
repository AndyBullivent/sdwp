using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI;
using Microsoft.SharePoint.Utilities;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Collabco.SussexDowns.WebParts;

namespace Collabco.SussexDowns.WebParts.Layouts.Collabco.Services
{
    public partial class MyStudentsEndPoint : System.Web.UI.Page
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
            sb.Append("<students>");
            sb.Append("<OpenConcernsTitle>");
            sb.Append("Open Concerns");
            sb.Append("</OpenConcernsTitle>");
            sb.Append("<OpenConcerns>");
            sb.Append(GetOpenConcernsCountByUser() );
            sb.Append("</OpenConcerns>");
            sb.Append("</students>");
            writer.Write(sb.ToString());
        }

        private int GetOpenConcernsCountByUser()
        {
            int openConcerns = 0;
            try
            {
                openConcerns = (int)SQLHelper.GetOpenConcernsByUser(_userName);
            }
            catch (Exception exp)
            {
                DiagnosticLog.Error("GetOpenConcernsCountByUser", exp.Message);
                throw new SPException(exp.Message, exp);
            }
            return openConcerns;
        }
        
    }
}
