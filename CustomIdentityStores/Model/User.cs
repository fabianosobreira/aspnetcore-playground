﻿using CustomIdentityStores.Persistence;

namespace CustomIdentityStores.Model
{
    public class User
    {
        public string Id { get; set; } = IdFactory.NewId();
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }

        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }
    }
}
