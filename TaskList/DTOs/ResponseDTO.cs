﻿namespace TaskList.DTOs
{
    public class ResponseDTO
    {
        public string? Status { get; set; }
        public string Message { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
