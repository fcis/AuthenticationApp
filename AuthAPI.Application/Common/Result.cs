using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Application.Common
{
    /// <summary>
    /// Generic result class to standardize operation results across the application
    /// </summary>
    /// <typeparam name="T">The type of data returned in the result</typeparam>
    public class Result<T>
    {
        /// <summary>
        /// Indicates whether the operation was successful
        /// </summary>
        public bool Succeeded { get; private set; }

        /// <summary>
        /// The data returned by the operation (if successful)
        /// </summary>
        public T? Data { get; private set; }

        /// <summary>
        /// Message providing additional information about the result
        /// </summary>
        public string? Message { get; private set; }

        /// <summary>
        /// Creates a successful result with data and optional message
        /// </summary>
        /// <param name="data">The data to include in the result</param>
        /// <param name="message">Optional message</param>
        /// <returns>A success result</returns>
        public static Result<T> Success(T data, string message = "")
        {
            return new Result<T> { Succeeded = true, Data = data, Message = message };
        }

        /// <summary>
        /// Creates a failure result with an error message
        /// </summary>
        /// <param name="message">Error message explaining the failure</param>
        /// <returns>A failure result</returns>
        public static Result<T> Failure(string message)
        {
            return new Result<T> { Succeeded = false, Message = message };
        }
    }

    // Non-generic version for operations that don't return data
    public class Result
    {
        public bool Succeeded { get; private set; }
        public string? Message { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();

        public static Result Success(string message = "")
        {
            return new Result { Succeeded = true, Message = message };
        }

        public static Result Failure(string message, List<string>? errors = null)
        {
            return new Result
            {
                Succeeded = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }
    }
}
