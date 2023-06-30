﻿namespace Publink.Rest.Models.Responses
{
    public class ErrorBaseResponse<T>
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

        public T Response { get; set; }
    }
}
