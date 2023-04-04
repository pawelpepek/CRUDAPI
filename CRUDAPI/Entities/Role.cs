﻿namespace CRUDAPI.Entities;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual List<User> Users { get; set; }
}
