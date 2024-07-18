using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Entities
{
    public class Note
    {
        public Note()
        {
            ParentNotes = new List<Note>();
        }
        public int NoteId { get; set; }
        [ForeignKey("Notes")]
        public int? ParentNoteId { get; set; }
        public virtual Note? ParentNote { get; set; }
        [Required]
        public required string Text { get; set; }
        [Required]
        public required string PlainText { get; set; }
        public bool Cancelled { get; set; }
        public bool Completed { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CancelledDate { get; set; }
        public DateTime? CompletedDate { get; set; }

        public virtual ICollection<Note> ParentNotes { get; set; }
    }
}
