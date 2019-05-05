using System;
namespace jmpcoon
{
    public static class Objects
    {
        public static TObject RequireNonNull<TObject>(this TObject obj) where TObject : class
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }
            return obj;
        }
    }
}
