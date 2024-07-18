using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Notes.Data;
using Notes.Entities;
using System.ComponentModel.DataAnnotations;

namespace Notes.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext applicationDbContext;

        public IndexModel(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [MinLength(1)]
        [Required]
        [BindProperty]
        public string Text { get; set; } = "";

        [MinLength(1)]
        [Required]
        [BindProperty]
        public string PlainText { get; set; } = "";

        [BindProperty]
        public int? NewNoteParentId { get; set; }

        public List<Note> Notes { get; set; } = new List<Note>();

        public async Task OnGetAsync()
        {
            await GetNotes();
        }


        public async Task<IActionResult> OnPostCreateNote()
        {
            await SaveNote(Text, PlainText, NewNoteParentId);

            return Redirect("/");
        }
        [BindProperty]
        public int NoteId { get; set; }

        public async Task<IActionResult> OnPostCancelNote()
        {
            await CancelNote(NoteId);

            return Redirect("/");
        }




        async Task GetNotes()
        {
            Notes = await applicationDbContext.Notes.Include(x => x.ParentNotes).Where(x => !x.Cancelled && !x.Completed && x.ParentNoteId == null).OrderByDescending(x => x.NoteId).ToListAsync();
        }

        async Task SaveNote(string note, string plainText, int? parentId)
        {
            applicationDbContext.Notes.Add(new Note
            {
                Text = note,
                PlainText = plainText,
                ParentNoteId = parentId,
                CreatedDate = DateTime.Now,               
            });

            await applicationDbContext.SaveChangesAsync();
        }


        async Task CancelNote(int noteId)
        {
            var note = await applicationDbContext.Notes.FindAsync(noteId);
            if (note != null)
            {
                note.Cancelled = true;
                note.CancelledDate = DateTime.Now;
                await applicationDbContext.SaveChangesAsync();
            }
        }

        async Task CompleteNote(int noteId)
        {
            var note = await applicationDbContext.Notes.FindAsync(noteId);
            if (note != null)
            {
                note.Completed = true;
                note.CompletedDate = DateTime.Now;
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
