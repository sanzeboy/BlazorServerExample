﻿using PopulationPortal.Domain.Entities.Common;

namespace PopulationPortal.Domain.Entities.AppUsers
{
    public class AppUser : CommonProperties
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
