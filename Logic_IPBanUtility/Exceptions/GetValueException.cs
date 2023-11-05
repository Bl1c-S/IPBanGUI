using System;

public class GetValueException : Exception
{
     public GetValueException(string message) : base(message)
     {
     }

     public GetValueException(string message, Exception innerException) : base(message, innerException)
     {
     }
}