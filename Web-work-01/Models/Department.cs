using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_work_01.Models;

public class Department
{
    [NotMapped]
    public int DepartmentId { get; set; }
    [NotMapped]
    public int? ParentID { get; set; }
    [NotMapped]
    public string? department { get; set; }
    [NotMapped]
    public int Level { get; set; }
}
