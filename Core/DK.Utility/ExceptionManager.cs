using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DK.Utility
{
    public static class ExceptionManager
    {
        public static string GetFullMessage(Exception err)
        {
            StringBuilder msg = new StringBuilder(err.Message);
            if ((err.Data != null) && (err.Data.Count > 0))
            {
                msg.AppendLine("Data:");
                foreach (object xCurData in err.Data.Keys)
                    msg.AppendLine(xCurData.ToString() + ": " + (err.Data[xCurData.ToString()] ?? "").ToString());
            }
            msg.AppendLine();
            while (err.InnerException != null)
            {
                msg.AppendLine(err.InnerException.Message);
                if ((err.Data != null) && (err.Data.Count > 0))
                {
                    msg.AppendLine("Data:");
                    foreach (object xCurKey in err.Data.Keys)
                        msg.AppendLine(xCurKey.ToString() + ": " + err.Data[xCurKey].ToString());
                }
                msg.AppendLine();
                err = err.InnerException;
            }

            return msg.ToString().Replace("'", "\"").Replace("\r\n", "<br/>");
        }

        public static string ToFullMessage(this Exception err)
        {
            StringBuilder msg = new StringBuilder(err.Message);
            if ((err.Data != null) && (err.Data.Count > 0))
            {
                msg.AppendLine("Data:");
                foreach (object xCurData in err.Data.Keys)
                    msg.AppendLine(xCurData.ToString() + ": " + (err.Data[xCurData.ToString()] ?? "").ToString());
            }
            msg.AppendLine();
            while (err.InnerException != null)
            {
                msg.AppendLine(err.InnerException.Message);
                if ((err.Data != null) && (err.Data.Count > 0))
                {
                    msg.AppendLine("Data:");
                    foreach (object xCurKey in err.Data.Keys)
                        msg.AppendLine(xCurKey.ToString() + ": " + err.Data[xCurKey].ToString());
                }
                msg.AppendLine();
                err = err.InnerException;
            }

            return msg.ToString().Replace("'", "\"").Replace("\r\n", "<br/>");
        }
    }
}
