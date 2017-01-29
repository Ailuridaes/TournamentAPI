using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.Core.Exceptions
{
    public class EntityHierarchyException : Exception
    {
        public EntityHierarchyException() { }
        public EntityHierarchyException(string childEntity, string parentEntity) : base($"{childEntity} keys do not match the database object under that {parentEntity}") { }
        public EntityHierarchyException(string childEntity, string parentEntity, Exception inner) : base($"{childEntity} keys do not match the database object under that {parentEntity}", inner) { }
        protected EntityHierarchyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}