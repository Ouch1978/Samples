using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SL_BindingEnums
{
    public class EnumHelper
    {
        public static List<T> ToList<T>()
        {
            Type enumType = typeof( T );

            if( !enumType.IsEnum )
            {
                throw new ArgumentException( "Type '" + enumType.Name + "' is not an enum" );
            }

            List<T> values = new List<T>();

            var fields = from field in enumType.GetFields()
                         where field.IsLiteral
                         select field;

            foreach( FieldInfo field in fields )
            {
                object value = field.GetValue( enumType );
                values.Add( ( T ) value );
            }

            return values;
        }

    }
}
