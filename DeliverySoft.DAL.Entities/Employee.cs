﻿namespace DeliverySoft.DAL.Entities;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }

    public ICollection<AppointedEmployee> AppointedEmployees { get; set; } = new List<AppointedEmployee>();
}