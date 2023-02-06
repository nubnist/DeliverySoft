using System;
using System.Linq;
using System.Text;

namespace DeliverySoft.Core
{
    public static class SwaggerHelper
    {
        /// <summary>
        /// Реализует кастомную схему данных swagger
        /// </summary>
        public static StringBuilder GetNameType(Type type)
        {
            var typeName = new StringBuilder();
            typeName.Append(type.Namespace);
            typeName.Append('.');
            typeName.Append(type.Name.Split('`')[0]);
            var genericArguments = type.GetGenericArguments().Select(generic => GetNameType(generic)).ToArray();
            if (genericArguments.Length > 0)
            {
                typeName.Append('<');
                for (int i = 0; i < genericArguments.Length; i++)
                {
                    typeName.Append(genericArguments[i]);
                    if (i < genericArguments.Length - 1)
                    {
                        typeName.Append(',');
                    }
                }
                typeName.Append('>');
            }
            return typeName;
        }
    }
}
