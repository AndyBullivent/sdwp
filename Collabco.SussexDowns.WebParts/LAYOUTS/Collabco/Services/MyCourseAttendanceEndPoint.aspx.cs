using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI;
using Microsoft.SharePoint.Utilities;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Collabco.SussexDowns.WebParts;
using Microsoft.SharePoint.Administration.Claims;

namespace Collabco.SussexDowns.WebParts.Layouts.collabco.services
{
    [Serializable]
    public class Course
    {
        public string CourseTitle { get; set; }
        public string Group { get; set; }
        public double Attendance { get; set; }
    }
    public partial class MyCourseAttendanceEndPoint : System.Web.UI.Page
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
            List<Course> courses = GetCourseAttendanceForCurrentUser();
            double average = 0;
            if ((courses != null) && (courses.Count > 0))
            {
                average = courses.Average(p => p.Attendance);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<CourseAttendance>");
            sb.Append("<CourseAttendanceAgg>");
            sb.Append(Math.Round((average * 100),1));
            sb.Append("</CourseAttendanceAgg>");
            if (courses.Count > 0)
            {
                sb.Append("<Courses>");
                foreach (Course c in courses)
                {

                    sb.Append("<Course>");
                    sb.Append("<CourseTitle>");
                    sb.Append(c.CourseTitle);
                    sb.Append("</CourseTitle>");
                    sb.Append("<Group>");
                    sb.Append(c.Group);
                    sb.Append("</Group>");
                    sb.Append("<Attendance>");
                    sb.Append(Math.Round((c.Attendance * 100),1));
                    sb.Append("</Attendance>");
                    sb.Append("</Course>");

                }
                sb.Append("</Courses>");
            }
            sb.Append("</CourseAttendance>");
            writer.Write(sb.ToString());
        }
        protected List<Course> GetCourseAttendanceForCurrentUser()
        {
            try
            {
                List<Course> courses = new List<Course>();

                System.Data.IDataReader rdr = SQLHelper.GetCourseAttendanceByUser(_userName);
                while (rdr.Read())
                {
                    Course c = new Course();
                    c.CourseTitle = rdr["Course Title"].ToString();
                    c.Group = rdr["Group"].ToString();
                    c.Attendance = Convert.ToDouble(rdr["Attendance"].ToString());
                    courses.Add(c);
                }
                return courses;
            }
            catch(Exception exp)
            {
                throw exp;
            }
        }
//<attendance>
//    <CurrentMonthString>
//        November 2013
//    </CurrentMonthString>
//    <CurrentMonthAgg>
//        25
//    </CurrentMonthAgg>
//    <YearAgg>
//        35
//    </YearAgg>
//    <Courses>
//        <Course>
//            <CourseName>
//                BTEC Things
//            </CourseName>
//            <Percentage>
//                46
//            </Percentage>
//        </Course>
//        <Course>
//            <CourseName>
//                DipSW Stuff
//            </CourseName>
//            <Percentage>
//                86
//            </Percentage>
//        </Course>
//    </Courses>
//</attendance>

    }
}