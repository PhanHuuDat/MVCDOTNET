﻿namespace MVCMovie.Models
{
    public class Member
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public RoleStatus Role { get; set; }
    }
}
