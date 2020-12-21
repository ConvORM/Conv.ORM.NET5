using System;

namespace Conv.ORM.Exceptions
{
    [Serializable]
    public class ConnectionException : Exception
    {
        public string ErrorCode { get; }
        public string PossiblesSolutions { get; }
        public ConnectionException()
        {

        }

        public ConnectionException(string errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
            PossiblesSolutions = "";
            
        }

        public ConnectionException(string errorCode, string message, string possiblesSolutions)
            : base(message)
        {
            ErrorCode = errorCode;
            PossiblesSolutions = possiblesSolutions;

        }
    }
}
