﻿using System.Net;
using System;

namespace PayPal
{
    /// <summary>
    /// Represents an error that occurred in the PayPal SDK after sending an HTTP request to the PayPal REST API.
    /// </summary>
    public class HttpException : ConnectionException
    {
        /// <summary>
        /// Gets the <see cref="System.Net.HttpStatusCode"/> returned from the server.
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// Gets the <see cref="System.Net.WebHeaderCollection"/> included with the HTTP response.
        /// </summary>
        public WebHeaderCollection Headers { get; private set; }

        /// <summary>
        /// Represents an error occurred when attempting to send an HTTP request.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="response">The response from server.</param>
        /// <param name="statusCode">HTTP status code</param>
        /// <param name="webExceptionStatus">HTTP status code returned from the server.</param>
        /// <param name="headers">HTTP headers included with the server response.</param>
        /// <param name="request">HTTP request sent by this SDK.</param>
        public HttpException(string message, string response, HttpStatusCode statusCode, WebExceptionStatus webExceptionStatus, WebHeaderCollection headers, HttpWebRequest request) : base(message, response, webExceptionStatus, request)
        {
            this.StatusCode = statusCode;
            this.Headers = headers;
        }

        /// <summary>
        /// Copy constructor provided by convenience for derived classes.
        /// </summary>
        /// <param name="ex">The original exception to copy information from.</param>
        protected HttpException(HttpException ex) : base(ex)
        {
            this.StatusCode = ex.StatusCode;
            this.Headers = ex.Headers;
        }

        /// <summary>
        /// Attempts to convert this exception object to another specified exception type.
        /// </summary>
        /// <typeparam name="T">Object type that must derive from HttpException.</typeparam>
        /// <param name="other">Variable that will contain the newly created instance of the derviced class.</param>
        /// <returns>True if the object was successfully created; false otherwise.</returns>
        public bool TryConvertTo<T>(out T other) where T: HttpException
        {
            other = null;
            try
            {
                other = (T)Activator.CreateInstance(typeof(T), new object[] { this });
            }
            catch (System.Exception) { /* Silently ignore any error in converting. */}
            return other != null;
        }

        /// <summary>
        /// Gets the prefix to use when logging the exception information.
        /// </summary>
        protected override string ExceptionMessagePrefix { get { return "HTTP Exception"; } }

        /// <summary>
        /// Override of the default log message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        protected override void LogDefaultMessage(string message) { }
    }
}
