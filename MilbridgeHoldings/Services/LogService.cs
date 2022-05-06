namespace ModelLibrary.Services
{
    public class LogService
    {
        /*  public static void Information(LogInformation information)
          {
              string filename = information.FileName.ToString() + ".txt";
              string path = Startup.environment.ContentRootPath + "\\wwwroot\\Logs\\" + information.Location.ToString();
              filename = filename.Replace("/", string.Empty);

              string logText = "TimeStamp:" + "\t" + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss") + Environment.NewLine +
                               "Data:`" + "\t" + information.Data +
                               Environment.NewLine
                               + "===============================================================================================================================================" + Environment.NewLine;
              if (!Directory.Exists(path))
                  Directory.CreateDirectory(path);
              File.AppendAllText(path + "\\" + filename, logText);
          }

          public static void Error(LogError logError)
          {
              string filename = logError.FileName.ToString() + ".txt";
              string path = Startup.environment.ContentRootPath + "\\wwwroot\\Logs\\" + logError.Location.ToString() + "\\" + DateTime.Now.ToString("dd-MMM-yyyy");
              filename = filename.Replace("/", string.Empty);

              string logText = "TimeStamp:" + "\t" + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss") + Environment.NewLine +
                               "Data:" + "\t" + logError.Data + Environment.NewLine +
                               "Error:" + "\t" + ExceptionToString(logError.Exception) +
                               Environment.NewLine +
                               "===============================================================================================================================================" + Environment.NewLine;

              if (!Directory.Exists(path))
                  Directory.CreateDirectory(path);
              File.AppendAllText(path + "\\" + filename, logText);
          }

          private static string ExceptionToString(Exception exception)
          {
              StringBuilder sbExceptionMessage = new StringBuilder();
              sbExceptionMessage.Append("Exception Type" + Environment.NewLine);
              sbExceptionMessage.Append(exception.GetType().Name);
              sbExceptionMessage.Append(Environment.NewLine + Environment.NewLine);
              sbExceptionMessage.Append("Message" + Environment.NewLine);
              sbExceptionMessage.Append(exception.Message + Environment.NewLine + Environment.NewLine);
              sbExceptionMessage.Append("Stack Trace" + Environment.NewLine);
              sbExceptionMessage.Append(exception.StackTrace + Environment.NewLine + Environment.NewLine);
              sbExceptionMessage.Append(Environment.NewLine + Environment.NewLine);
              Exception innerException = exception.InnerException;
              while (innerException != null)
              {
                  sbExceptionMessage.Append("Exception Type" + Environment.NewLine);
                  sbExceptionMessage.Append(innerException.GetType().Name);
                  sbExceptionMessage.Append(Environment.NewLine + Environment.NewLine);
                  sbExceptionMessage.Append("Message" + Environment.NewLine);
                  sbExceptionMessage.Append(innerException.Message + Environment.NewLine + Environment.NewLine);
                  sbExceptionMessage.Append("Stack Trace" + Environment.NewLine);
                  sbExceptionMessage.Append(innerException.StackTrace + Environment.NewLine + Environment.NewLine);
                  innerException = innerException.InnerException;
              }
              return sbExceptionMessage.ToString();
          }
      }*/
    }
}