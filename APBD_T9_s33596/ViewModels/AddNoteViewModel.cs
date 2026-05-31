using System.ComponentModel.DataAnnotations;

namespace APBD_T9_s33596.ViewModels
{

    public class AddNoteViewModel
    {
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; } = string.Empty;
    }
}
