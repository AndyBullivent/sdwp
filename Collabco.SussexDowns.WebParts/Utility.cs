using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Administration.Claims;
using System.Data;
using System.Data.SqlClient;
using Collabco.SussexDowns.WebParts.LAYOUTS.Settings;
namespace Collabco.SussexDowns.WebParts
{
    [System.Runtime.InteropServices.Guid("02420c76-a3b9-4526-9685-2a9e659a838a")]
    public class DiagnosticLog : SPDiagnosticsServiceBase
    {
        private const string ProductName = "Collabco.SussexDowns.WebParts";
        private const string InfoCategory = ProductName + "_Info";
        private const string ErrorCategory = ProductName + "_Error";

        private DiagnosticLog() : base(ProductName + ".Logging", SPFarm.Local) { }

        private static DiagnosticLog _logService;
        public static DiagnosticLog LogService
        {
            get { return _logService ?? (_logService = new DiagnosticLog()); }
        }

        protected override IEnumerable<SPDiagnosticsArea> ProvideAreas()
        {
            var areas = new List<SPDiagnosticsArea> {
              new SPDiagnosticsArea(ProductName, new List<SPDiagnosticsCategory> {
                  new SPDiagnosticsCategory(InfoCategory, TraceSeverity.Verbose, EventSeverity.Information),
                  new SPDiagnosticsCategory(ErrorCategory, TraceSeverity.Unexpected, EventSeverity.Warning),
              })
            };
            return areas;
        }

        public static void Info(string methodName, string errorMessage)
        {
            SPDiagnosticsCategory category = LogService.Areas[ProductName].Categories[InfoCategory];
            LogService.WriteTrace(0, category, TraceSeverity.Verbose, methodName + "::" + errorMessage);
        }

        public static void Error(string methodName, string errorMessage)
        {
            SPDiagnosticsCategory category = LogService.Areas[ProductName].Categories[ErrorCategory];
            LogService.WriteTrace(0, category, TraceSeverity.Unexpected, methodName + "::" + errorMessage);
        }
    }

    public class SQLHelper
    {
        #region private 
        private static string OpenConcerns_SQLCONNECTIONSTRING = @"Data Source=Cloud-spdev2;Initial Catalog=WSS_Content;UID=dataviewreader;PWD=password";
        private static string UnMarkedRegisters_SQLCONNECTIONSTRING = @"Data Source=Cloud-spdev2;Initial Catalog=WSS_Content;UID=myday_user;PWD=Pr0v1der";
        private static string CourseAttendance_SQLCONNECTIONSTRING = @"Data Source=Cloud-spdev2;Initial Catalog=WSS_Content;UID=myday_user;PWD=Pr0v1der";
        static SQLHelper()
        {
            DBRec rec = TileDatabaseSettings.GetConnectionStringProperties();
            if(rec != null)
            {
                OpenConcerns_SQLCONNECTIONSTRING = rec.StudentsConnectionString;
                UnMarkedRegisters_SQLCONNECTIONSTRING = rec.RegisterConnectionString;
                CourseAttendance_SQLCONNECTIONSTRING = rec.StatsConnectionString;
            }


        }

        private static IDataReader SqlExecuteReader(string connectionString, CommandType commandType, string commandText, SqlParameter[] sqlParams)
        {
            SqlConnection connection = new SqlConnection(connectionString);  
            SqlCommand command = connection.CreateCommand();
            command.CommandType = commandType;
            command.CommandText = commandText;

            if (sqlParams != null)
            {
                foreach (SqlParameter parameter in sqlParams)
                {
                    command.Parameters.Add(parameter);
                }
            }

            connection.Open();
            return (IDataReader)command.ExecuteReader(CommandBehavior.CloseConnection);
        }
        private static IDataReader SqlExecuteReader(string ConnectionString, CommandType commandType, string commandText, List<SqlParameter> parameters)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);  
            SqlCommand command = connection.CreateCommand();
            command.CommandType = commandType;
            command.CommandText = commandText;

            if (parameters != null)
            {
                foreach (SqlParameter p in parameters)
                    command.Parameters.Add(p);
            }

            connection.Open();
            return (IDataReader)command.ExecuteReader(CommandBehavior.CloseConnection);
        }
        private static int SqlExecuteNonQuery(string ConnectionString, CommandType commandType, string commandText, List<SqlParameter> parameters)
        {
            int rowCount = 0;
            SqlConnection connection = new SqlConnection(ConnectionString);  
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = commandType;
                command.CommandText = commandText;

                if (parameters != null)
                {
                    foreach (SqlParameter p in parameters)
                        command.Parameters.Add(p);
                }

                connection.Open();
                rowCount = command.ExecuteNonQuery();
            }
            if (connection != null)
                connection.Close();
            return rowCount;
        }
        private static int SqlExecuteNonQuery(string ConnectionString, CommandType commandType, string commandText, int CommandTimeOut, List<SqlParameter> parameters)
        {
            int rowCount = 0;
            SqlConnection connection = new SqlConnection(ConnectionString);  
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = commandType;
                command.CommandText = commandText;
                command.CommandTimeout = CommandTimeOut;

                if (parameters != null)
                {
                    foreach (SqlParameter p in parameters)
                        command.Parameters.Add(p);
                }

                connection.Open();
                rowCount = command.ExecuteNonQuery();
            }
            if (connection != null)
                connection.Close();
            return rowCount;
        }
        private static int SqlExecuteNonQuery(string ConnectionString, CommandType commandType, string commandText, int CommandTimeOut, SqlParameterCollection parameters)
        {
            int rowCount = 0;
            SqlConnection connection = new SqlConnection(ConnectionString);  
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = commandType;
                command.CommandText = commandText;
                command.CommandTimeout = CommandTimeOut;

                if (parameters != null)
                {
                    foreach (SqlParameter p in parameters)
                        command.Parameters.Add(p);
                }

                connection.Open();
                rowCount = command.ExecuteNonQuery();
            }
            if (connection != null)
                connection.Close();
            return rowCount;
        }
        private static int SqlExecuteNonQuery(CommandType commandType, string commandText, SqlParameter[] sqlParams)
        {
            int rowCount = 0;
            SqlConnection connection = new SqlConnection(OpenConcerns_SQLCONNECTIONSTRING);  
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = commandType;
                command.CommandText = commandText;

                if (sqlParams != null)
                {
                    foreach (SqlParameter p in sqlParams)
                        command.Parameters.Add(p);
                }

                connection.Open();
                rowCount = command.ExecuteNonQuery();
            }
            if (connection != null)
                connection.Close();
            return rowCount;
        }
        private static IDataReader SqlExecuteReader(CommandType commandType, string commandText, SqlParameter[] sqlParams)
        {
            SqlConnection connection = new SqlConnection(OpenConcerns_SQLCONNECTIONSTRING);  
            SqlCommand command = connection.CreateCommand();
            command.CommandType = commandType;
            command.CommandText = commandText;
            if (sqlParams != null)
            {
                foreach (SqlParameter parameter in sqlParams)
                {
                    command.Parameters.Add(parameter);
                }
            }

            connection.Open();
            return (IDataReader)command.ExecuteReader(CommandBehavior.CloseConnection);
        }
        #endregion

        public static object GetUnMarkedRegistersCountByUser(string userName)
        {
            DiagnosticLog.Info("GetUnMarkedRegistersCountByUser", string.Format("Get UnMarked Registers Count By User for user {0}", userName));
            SqlConnection sqlConnection1 = new SqlConnection(UnMarkedRegisters_SQLCONNECTIONSTRING);
            SqlCommand cmd = new SqlCommand();
            Object returnValue;

            cmd.CommandText = "dbo.sdc_myday_unmarked_reg";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;
            cmd.Parameters.Add(new SqlParameter("@uname", userName));

            sqlConnection1.Open();

            returnValue = cmd.ExecuteScalar();

            sqlConnection1.Close();
            return returnValue;
        }

        public static object GetOpenConcernsByUser(string userName)
        {
            DiagnosticLog.Info("GetOpenConcernsByUser", string.Format("Getting Open Concerns By User for user {0}", userName));
            SqlConnection sqlConnection1 = new SqlConnection(OpenConcerns_SQLCONNECTIONSTRING);
            SqlCommand cmd = new SqlCommand();
            Object returnValue;

            cmd.CommandText = "dbo.OpenConcerns";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;
            cmd.Parameters.Add(new SqlParameter("@UserName",userName));

            sqlConnection1.Open();

            returnValue = cmd.ExecuteScalar();

            sqlConnection1.Close();
            return returnValue;
        }

        public static IDataReader GetCourseAttendanceByUser(string userName)
        {
            DiagnosticLog.Info("GetCourseAttendanceByUser", string.Format("Getting course attendance data for user {0}",userName));
            return SqlExecuteReader(CourseAttendance_SQLCONNECTIONSTRING, CommandType.StoredProcedure, "dbo.SDC_MYDAY_COURSE_ATT_TUTOR", new SqlParameter[] { new SqlParameter("@uname", userName) });
        }
    }

    public class Utility
    {
        public static string SanitiseLoginName(string value)
        {
            try
            {
                string username = String.Empty;
                SPClaimProviderManager mgr = SPClaimProviderManager.Local;
                if(mgr!=null)
                {
                    username = mgr.DecodeClaim(value).Value;
                }
                if (username.Contains('\\'))
                {
                    return username.Split('\\')[1];
                }
                else if(username.Contains('@'))
                {
                    return username.Split('@')[0];
                }
                else
                {
                    return username;
                }
            }
            catch
            {
                return value;
            }
        }
    }

}
