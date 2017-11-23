using System;

namespace CustomIdentityStores.Persistence
{
    public static class IdFactory
    {
        /// <summary>
        /// Generates a 32 digit Guid.
        /// </summary>
        /// <returns></returns>
        public static string NewId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
