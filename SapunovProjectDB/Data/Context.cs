using System.Data.Entity;

namespace SapunovProjectDB.Data
{
    public partial class DBEntities : DbContext
    {
        private static DBEntities context;
        public static DBEntities GetContext()
        {
            if (context == null)
            {
                context = new DBEntities();
            }
            return context;
        }
    }
}
