using System;
using System.ComponentModel.DataAnnotations;

namespace Inter.Models
{
    public class Viagem
    {
        public int Id { get; set; }
        [MaxLength(90)]
        public string BairroPartida { get; set; }
        public string BairroChegada{ get; set; }
        public int HoraPartida { get; set; }
        public int HoraChegada { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public DateTimeOffset? DueAt { get; set; }
    }
}
