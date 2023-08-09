using CoreLibrary.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoreLibrary.Models
{
    public class QueueBorrowBook : IQueueBorrowBook
    {
        [Key()]
        public int Id { get; set; }
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public bool IsFinished { get; set; }
    }
}
