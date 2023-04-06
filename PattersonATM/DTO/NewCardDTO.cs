using System;
namespace PattersonATM.DTO
{
    public record NewCardRequest
    {
        public string CardNum { get; set; }
        public string Pin { get; set; }
        public int UserId { get; set; }

    };
}

