using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepoLayer.Entity
{
    public class NotesEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NoteId { get; set; }

        public string Title { get; set; }
        public string TakeNote { get; set; }
        public DateTime Reminder { get; set; }
        public string Colour { get; set; }
        public string Image { get; set; }
        public bool isArchive { get; set; }
        public bool isPin { get; set; }

        public bool isTrash { get; set; }

        [ForeignKey(nameof(NotesEntity))]
        public long UserId { get; set; }
        public UserEntity User { get; set; }


    }
}
