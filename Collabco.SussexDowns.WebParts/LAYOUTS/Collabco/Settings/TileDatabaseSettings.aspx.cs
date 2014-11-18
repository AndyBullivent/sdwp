using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Collabco.SussexDowns.WebParts.LAYOUTS.Settings
{
    public partial class TileDatabaseSettings : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InternalGetConnectionStringProperties();
            }
        }
        public static DBRec GetConnectionStringProperties()
        {
            DBRec rec = null;
            try
            {
                SPContext ctx = SPContext.Current;
                using (SPWeb web = ctx.Site.OpenWeb())
                {
                    if (web.AllProperties.ContainsKey("DBStr"))
                    {
                        rec = new DBRec((string)web.AllProperties["DBStr"]);
                        return rec;
                    }
                }
                return rec;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        private void InternalGetConnectionStringProperties()
        {
            DBRec rec = new DBRec();
            try
            {
                SPContext ctx = SPContext.Current;
                using(SPWeb web = ctx.Site.OpenWeb())
                {
                    if(web.AllProperties.ContainsKey("DBStr"))
                    {
                        rec = new DBRec((string)web.AllProperties["DBStr"]);
                        StudConnStr.Text = rec.StudentsConnectionString;
                        RegConnStr.Text = rec.RegisterConnectionString;
                        StatsConnStr.Text = rec.StatsConnectionString;
                    }                    
                }
            }
            catch(Exception e)
            {
                throw e;

            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            SaveConnectionStrings();
            SPUtility.TransferToSuccessPage("Connection Strings Saved");
        }

        private void SaveConnectionStrings()
        {
            try
            {
                SPContext ctx = SPContext.Current;
                using(SPWeb web = ctx.Site.OpenWeb())
                {
                    DBRec rec = new DBRec(RegConnStr.Text, StatsConnStr.Text, StudConnStr.Text);
                    if (!web.AllProperties.ContainsKey("DBStr"))
                    {
                        web.AllProperties.Add("DBStr", rec.CreateDBRecString());
                    }
                    else
                    {
                        web.AllProperties["DBStr"] = rec.CreateDBRecString();
                    }
                    web.Update();                    
                }
            }
            catch (Exception e)
            {
                SPUtility.TransferToErrorPage("An Error Occurred: " + e.Message);

                throw e;
            }
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {

            SPUtility.Redirect(Web.Url + "/_layouts/15/settings.aspx", SPRedirectFlags.UseSource, HttpContext.Current);
        }


    }
    public class DBRec
    {
        public DBRec(string dbRecstr)
        {
            string[] p = dbRecstr.Split(SEPARATOR[0]);
            RegisterConnectionString = p[0].ToString();
            StatsConnectionString = p[1].ToString();
            StudentsConnectionString = p[2].ToString();

        }

        public DBRec() { }

        public DBRec(string p1, string p2, string p3)
        {
            RegisterConnectionString= p1;
            StatsConnectionString = p2;
            StudentsConnectionString = p3;
        }

        public string RegisterConnectionString { get; set; }
        public string StatsConnectionString { get; set; }
        public string StudentsConnectionString { get; set; }

        static public readonly string SEPARATOR = "~";
        static public string CreateDBRecString(string registerCS, string statsCS, string StudsCS)
        {
            return registerCS + SEPARATOR + statsCS + SEPARATOR + StudsCS;
        }
        public string CreateDBRecString()
        {
            return RegisterConnectionString + SEPARATOR + StatsConnectionString+ SEPARATOR + StudentsConnectionString;
        }
    }
}
