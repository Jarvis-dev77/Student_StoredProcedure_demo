﻿using System.ComponentModel.DataAnnotations;

namespace Student_storproc.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string RollNo { get; set; }
        public string Subject { get; set; }

    }
}
