using System;
namespace jmpcoon.utils
{
    public static class Objects
    {
        public static TObject RequireNonNull<TObject>(this TObject obj)
        {
            if (obj.Equals(null))
            {
                throw new ArgumentNullException();
            }
            return obj;
        }
    }
}
