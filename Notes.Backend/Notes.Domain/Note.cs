using System;

namespace Notes.Domain
{
    public class Note
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime CreatingDate { get; set; }
        public DateTime? EditDate { get; set; }
        
    }
}