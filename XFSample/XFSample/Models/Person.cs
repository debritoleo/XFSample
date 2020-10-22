using SQLite;
using System;
using XFSample.Interfaces;

namespace XFSample.Models
{
    public class Person : IBasicEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DtBirth { get; set; }
    }
}
