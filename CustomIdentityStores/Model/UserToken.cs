using CustomIdentityStores.Persistence;

namespace CustomIdentityStores.Model
{
    public class UserToken
    {
        public string Id { get; set; } = IdFactory.NewId();
        public string UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
