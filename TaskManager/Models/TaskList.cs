using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class TaskList
    {
        public int Id { get; set; }

        [Required()]
        [MaxLength(50)]
        public string Title { get; set; }

        
        [Required()]
        [MaxLength(1000)]
        public string Description { get; set; }

        
        [Required()]        
        public int Priority { get; set; }

        
        public bool IsCompleted { get; set; }
    }    
}
