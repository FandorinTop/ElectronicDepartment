using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicDepartment.Common.Exceptions
{
    public class DbNullReferenceException : Exception
    {
        public static void ThrowExceptionIfNull(object entity, string parametName, string paramValue, string exceptionMessage = "")
        {
            if (entity is null)
            {
                if (string.IsNullOrEmpty(exceptionMessage))
                {
                    var message = $"No entity with {parametName}: {paramValue}";

                    throw new DbNullReferenceException(message);
                }

                throw new DbNullReferenceException(exceptionMessage);
            }
        }

        public DbNullReferenceException()
        {
        }

        public DbNullReferenceException(string? message) : base(message)
        {
        }

        public DbNullReferenceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DbNullReferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
