using CustomIdentityStores.Persistence;

namespace CustomIdentityStores.Model
{
    public class Role
    {
        public string Id { get; set; } = IdFactory.NewId();
        public string RoleName { get; set; }
    }
}
