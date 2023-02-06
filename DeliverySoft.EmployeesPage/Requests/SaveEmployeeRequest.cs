﻿namespace DeliverySoft.EmployeesPage.Requests;

public class SaveEmployeeRequest
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
}