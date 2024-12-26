using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_work_01.Models;

public partial class Employee
{
    public string? Name { get; set; }

    public string? Password { get; set; }
    [NotMapped]
    public int? DepartmentId { get; set; }
}
