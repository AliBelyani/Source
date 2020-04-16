using System;
using System.Collections.Generic;

namespace DK.Utility
{
    public class ErrorMessageException : Exception
    {
        public ErrorMessageException(string errorMessages)
            : base("Get Messages from Data[\"Messages\"] as List")
        {
            Data["Messages"] = new List<string> { errorMessages };
        }

        public ErrorMessageException(List<string> errorMessages)
            : base("Get Messages from Data[\"Messages\"] as List")
        {
            Data["Messages"] = errorMessages;
        }

        public ErrorMessageException(string errorMessages, Exception innerException)
            : base("Get Messages from Data[\"Messages\"] as List", innerException)
        {
            Data["Messages"] = new List<string> { errorMessages }; ;
        }

        public ErrorMessageException(List<string> errorMessages, Exception innerException)
            : base("Get Messages from Data[\"Messages\"] as List", innerException)
        {
            Data["Messages"] = errorMessages;
        }

    }
}

