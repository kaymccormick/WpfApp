using System ;

namespace Common.Model
{
    public class ResourceInfo
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        public ResourceInfo ( Uri source , object key , object value )
        {
            Source = source ;
            Key1   = key ;
            Value  = value ;
        }

        public Uri Source { get ; }

        public object Key1 { get ; }

        public object Value { get ; }
    }
}