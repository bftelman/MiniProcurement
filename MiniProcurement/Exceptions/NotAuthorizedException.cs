﻿namespace MiniProcurement.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException(string message): base(message) { }
    }
}
